class Room(string name, string desc, List<Item>? items = null, List<Feature>? features = null)
: GameObject(name, desc, null)
{
    public Dictionary<Direction, Room> Exits { get; set;  } = [];
    public List<Item> Items { get; set; } = items ?? [];
    public List<Feature> Features { get; set; } = features ?? [];
}