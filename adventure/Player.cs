class Player
{
    List<Item> Inventory;
    Room Location;

    public Player(Room startLocation, List<Item>? startItems = null)
    {
        Location = startLocation;
        Inventory = startItems ?? new List<Item>();

        Console.WriteLine(Location);
    }

    string Move(Direction direction)
    {
        if (Location.Exits.ContainsKey(direction))
        {
            Location = Location.Exits[direction];
            return $"You went {direction}.\n{Location}";
        }
        return "I can't go that way";
    }

    public string North() => Move(Direction.North);
    public string South() => Move(Direction.South);
    public string East() => Move(Direction.East);
    public string West() => Move(Direction.West);
    public string Up() => Move(Direction.Up);
    public string Down() => Move(Direction.Down);
}