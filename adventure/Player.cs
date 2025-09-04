class Player
{
    List<Item> Inventory;
    Room Location;

    public Player(Room startLocation, List<Item>? startItems = null)
    {
        Location = startLocation;
        Inventory = startItems ?? new List<Item>();

        Console.WriteLine($"{Location}\n");
    }

    string Move(Direction direction)
    {
        if (Location.Exits.ContainsKey(direction))
        {
            Location = Location.Exits[direction];
            return $"You went {direction}.\n{Location}";
        }
        return "You can't go that way";
    }

    public string North() => Move(Direction.North);
    public string South() => Move(Direction.South);
    public string East() => Move(Direction.East);
    public string West() => Move(Direction.West);
    public string Up() => Move(Direction.Up);
    public string Down() => Move(Direction.Down);
    public string Exit()
    {
        Environment.Exit(0);
        return ""; // this has to behere since ProcessCommand expects a string returning method
    }

    public string Look(string[] args)
    {
        if (args.Length > 0)
        {
            string target = args[0];

            foreach (Item item in Location.Items.Concat(Inventory)) if (item.Matches(target)) return item.Desc; // look for items in room an inventory
            foreach (Feature feature in Location.Features) if (feature.Matches(target)) return feature.Desc; // look for features
        }

        return "Look at what?";
    }

    public string Take(string[] args)
    {
        if (args.Length > 0)
        {
            string target = args[0];

            foreach (Item item in Location.Items)
                if (item.Matches(target))
                {
                    Location.Items.Remove(item);
                    Inventory.Add(item);
                    return $"Took {item}.";
                }
        }

        return "Take what?";
    }

    public string Drop(string[] args)
    {
        if (args.Length > 0)
        {
            string target = args[0];

            foreach (Item item in Inventory)
                if (item.Matches(target))
                {
                    Inventory.Remove(item);
                    Location.Items.Add(item);
                    return $"Dropped {item}.";
                }
        }

        return "Drop what?";
    }
    
    
    Item? FindItem(string name, IEnumerable<Item> collection)
    {
        foreach (Item item in collection)
            if (item.Matches(name.ToLower())) return item;

        return null;
    }

    Feature? FindFeature(string name, IEnumerable<Feature> collection)
    {
        foreach (Feature feature in collection)
            if (feature.Matches(name.ToLower())) return feature;

        return null;
    }

    public string Use(string[] args)
    {
        string returnStr = "Use what on what?";

        // these are pointless, since all null scenarios return, but the error correction would not shut up.
        Item? item = new Item("", "");
        Feature? target = new Feature("", "", Array.Empty<string>(), "", Array.Empty<string>(), new List<Feature>(), new List<Item>(), new Dictionary<Direction, Room>());

        if (args.Length == 0) return returnStr;

        if (args.Length == 1 || (args.Length == 2 && args[1] == "on"))
        {
            item = FindItem(args[0], Inventory);
            string itemName = (item == null) ? "what" : item.Name;
            return $"Use {itemName} on what?";
        }

        if (args.Length == 2 || (args.Length == 3 && args[1] == "on"))
        {
            item = FindItem(args[0], Inventory);
            target = FindFeature(args[(args[1] == "on") ? 2 : 1], Location.Features);

            string itemName = (item == null) ? "what" : item.Name;
            string featureName = (target == null) ? "what" : target.Name;

            if (item == null || target == null) return $"Use {itemName} on {featureName}";
        }

        if (item.Matches(target.Require[target.SequenceIdx]))
        {
            Location.Features.AddRange(target.NewFeatures);
            Location.Items.AddRange(target.NewItems);

            foreach (var kvp in target.NewExits)
                Location.Exits[kvp.Key] = kvp.Value;

            if (target.SequenceIdx == target.Require.Length - 1)
            {
                Location.Features.Remove(target);
            }
            returnStr = target.UnlockText;

            target.SequenceIdx++;
        }

        return returnStr;
    }
}