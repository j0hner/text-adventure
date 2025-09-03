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
        
            foreach (Item item in Location.Items) if (item.Matches(target)) return item.Desc;
            foreach (Feature feature in Location.Features) if (feature.Matches(target)) return feature.Desc;
        }

        return "Look at what?";
    }
}