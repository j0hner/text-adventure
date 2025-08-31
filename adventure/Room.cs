class Room(string name, string desc, Dictionary<Direction, string> exits)
{
    public string Name { get; } = name;
    public string Desc { get; } = desc;
    public Dictionary<Direction, string> Exits { get; } = exits;
}