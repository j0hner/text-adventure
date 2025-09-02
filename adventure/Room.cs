class Room(
    string name, string desc,
    List<Item>? items = null,
    List<Feature>? features = null)
: GameObject(name, desc, null)
{
    public Dictionary<Direction, Room> Exits { get; set; } = [];
    public List<Item> Items { get; set; } = items ?? [];
    public List<Feature> Features { get; set; } = features ?? [];

    public override string ToString()
    {
        List<string> strings = [Desc];
        if (Items.Count > 0) strings.Add($"You see: {string.Join(", ", Items)}");
        if (Features.Count > 0) strings.AddRange($"You notice: {string.Join(", ", Features)}");
        strings.Add($"You can go: {string.Join(", ", Exits.Keys)}");

        return string.Join("\n", strings);
    }
}