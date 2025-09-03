using System.Text.Json;
using System.Text.Json.Serialization;

static class WorldBuilder
{
    static Dictionary<string, Direction> dirLookup = new()
    {
        ["north"] = Direction.North,
        ["south"] = Direction.South,
        ["west"] = Direction.West,
        ["east"] = Direction.East,
        ["up"] = Direction.Up,
        ["down"] = Direction.Down,
    };

    static Dictionary<string, Item> ItemsDict = new Dictionary<string, Item>();
    static Dictionary<string, Room> RoomsDict = new Dictionary<string, Room>();
    static Dictionary<string, Feature> FeatureDict = new Dictionary<string, Feature>();
    static Dictionary<string, Dictionary<string, string>> RoomExitDict = new Dictionary<string, Dictionary<string, string>>();
    static Dictionary<string, Dictionary<string, string>> FeatureExitDict = new Dictionary<string, Dictionary<string, string>>();
    static List<Feature> FeatureList = new List<Feature>();

    public static Player MakeWorld(string jsonPath)
    {
        string json = File.ReadAllText(jsonPath);

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var root = JsonSerializer.Deserialize<Root>(json, options);
        if (root == null) throw new InvalidDataException($"{jsonPath} file is empty or failed to read.");


        // step 1: create game objects, catalog items and rooms
        List<RawRoom> rawRooms = root.Rooms ?? throw new InvalidDataException("Mandatory JSON object \"rooms\" in root is missing.");
        foreach (var rr in rawRooms)
        {
           Room room = rr.ToRoom();

            RoomExitDict[rr.Name!.ToLower()] = rr.Exits!;  // normalized key
            RoomsDict[room.Name.ToLower()] = room;

            if (rr.Items != null)
            {
                foreach (var rawItem in rr.Items)
                {
                    Item item = rawItem.ToItem();

                    room.Items.Add(item);
                    ItemsDict[item.Name.ToLower()] = item;
                }
            }

            if (rr.Features != null)
            {
                foreach (var fr in rr.Features)
                {
                    Feature feature = fr.ToFeature();
                    FeatureList.Add(feature);

                    if (fr.NewExits != null)
                        FeatureExitDict[feature.Name.ToLower()] = fr.NewExits;  // normalized key

                    room.Features.Add(feature);
                    FeatureDict[feature.Name.ToLower()] = feature;
                }
            }
        }

        // step 2: create player object, catalog player items
        RawPlayer rawPlayer = root.Player ?? throw new InvalidDataException("Mandatory JSON object \"player\" in root is missing.");
        string playerLocation = rawPlayer.StartLocation ?? throw new InvalidDataException("Mandatory JSON object \"start_location\" in \"player\" is missing.");
        RawItem[] rawInventory = rawPlayer.StartInventory ?? Array.Empty<RawItem>();
        List<Item> inventory = new List<Item>();

        foreach (RawItem rawItem in rawInventory)
        {
            Item item = rawItem.ToItem();
            inventory.Add(item);
            ItemsDict[item.Name] = item;
        }

        if (!RoomsDict.ContainsKey(playerLocation.ToLower())) throw new InvalidDataException($"Room reference \"{playerLocation}\" not found.");

        // step 3: connect room exits
        foreach (Room room in RoomsDict.Values)
        {
            string name = room.Name;
            Dictionary<string, string> rawExits = RoomExitDict[name.ToLower()];

            foreach (string key in rawExits.Keys)
            {
                string exitReferenceStr = rawExits[key].ToLower();

                if (!RoomsDict.ContainsKey(exitReferenceStr)) throw new InvalidDataException($"Room reference \"{exitReferenceStr}\" not found. Error in key 'exits' of room: \"{name}\".");
                Room roomReference = RoomsDict[exitReferenceStr];

                if (!dirLookup.ContainsKey(key)) throw new InvalidDataException($"Invalid direction key \"{key}\" in key 'exits' of room: \"{name}\"");
                Direction keyDirection = dirLookup[key];
                if (room.Exits.ContainsKey(keyDirection)) throw new InvalidDataException($"Duplicate direction in key 'exits' of room: \"{name}\"");

                room.Exits[keyDirection] = roomReference;
            }
        }

        // step 4 resolve exit refeneces in features
        foreach (Feature feature in FeatureList)
        {
            if (feature.NewExits.Count() == 0) continue;
            string name = feature.Name;
            Dictionary<string, string> rawExits = FeatureExitDict[name.ToLower()];

            foreach (string key in rawExits.Keys)
            {
                string exitReferenceStr = rawExits[key].ToLower();

                if (!RoomsDict.ContainsKey(exitReferenceStr)) throw new InvalidDataException($"Room reference \"{exitReferenceStr}\" not found. Error in key 'new_exits' of feature: \"{name}\".");
                Room roomReference = RoomsDict[exitReferenceStr];

                if (!dirLookup.ContainsKey(key)) throw new InvalidDataException($"Invalid direction key \"{key}\" in key 'new_exits' of feature: \"{name}\"");
                Direction keyDirection = dirLookup[key];
                if (feature.NewExits.ContainsKey(keyDirection)) throw new InvalidDataException($"Duplicate direction in key 'new_exits' of feature: \"{name}\"");

                feature.NewExits[keyDirection] = roomReference;
            }
        }
        return new Player(RoomsDict[playerLocation], inventory);
    }

    class Root
    {
        public List<RawRoom>? Rooms { get; set; }
        public RawPlayer? Player { get; set; }
    }

    class RawRoom
    {
        public string? Name { get; set; }
        public string? Desc { get; set; }
        public Dictionary<string, string>? Exits { get; set; }
        public List<RawItem>? Items { get; set; }
        public List<RawFeature>? Features { get; set; }

        
        public Room ToRoom()
        {
            if (Name == null) throw new InvalidDataException("Mandatory JSON key 'name' in room is missing.");
            if (Desc == null) throw new InvalidDataException($"Mandatory JSON key 'desc' in {Name} is missing.");
            if (Exits == null) throw new InvalidDataException($"Mandatory JSON key 'exits' in {Name} is missing.");

            return new Room(Name, Desc);
        }
    }

    class RawFeature
    {
        public string? Name { get; set; }
        public string? Desc { get; set; }

        [property: JsonPropertyName("unlock_text")]
        public string? UnlockText { get; set; }

        public string[]? Require { get; set; }
        public bool? Sequential { get; set; }
        public string[]? Aliases { get; set; }

        [property: JsonPropertyName("new_features")]
        public List<RawFeature>? NewFeatures { get; set; }

        [property: JsonPropertyName("new_items")]
        public List<RawItem>? NewItems { get; set; }

        [property: JsonPropertyName("new_exits")]
        public Dictionary<string, string>? NewExits { get; set; }

        public Feature ToFeature()
        {
            List<Feature> features = new List<Feature>();
            if (NewFeatures != null) // convert all child FeatureRaw instances to Features
                foreach (RawFeature rawFeature in NewFeatures)
                    features.Add(rawFeature.ToFeature());

            List<Item> items = new List<Item>();
            if (NewItems != null) // convert all child ItemRaw instances to Items
                foreach (RawItem rawItem in NewItems)
                    items.Add(rawItem.ToItem());

            return new Feature(
                Name ?? throw new InvalidDataException($"Mandatory JSON key 'name' in feature is missing."),
                Desc ?? throw new InvalidDataException($"Mandatory JSON key 'desc' in {Name} is missing."),
                Require ?? throw new InvalidDataException($"Mandatory JSON key 'require' in {Name} is missing."),
                Sequential ?? false, 

                UnlockText ?? "",
                Aliases ?? Array.Empty<string>(),
                features ?? new List<Feature>(),
                items ?? new List<Item>(),
                new Dictionary<Direction, Room>()
            );
        }
    };

    class RawItem
    {
        public string? Name { get; set; }
        public string? Desc { get; set; }
        public string[]? Aliases { get; set; }

        public Item ToItem()
        {
            return new Item(
                Name ?? throw new InvalidDataException($"Mandatory JSON key 'name' in item is missing."),
                Desc ?? throw new InvalidDataException($"Mandatory JSON key 'desc' in {Name} is missing."),
                Aliases ?? Array.Empty<string>()
            );
        }
    }

    class RawPlayer
    {
        [property: JsonPropertyName("start_location")]
        public string? StartLocation { get; set; }

        [property: JsonPropertyName("start_inventory")]
        public RawItem[]? StartInventory { get; set; }
    }
}

