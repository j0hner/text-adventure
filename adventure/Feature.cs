class Feature(
    string name,
    string desc,
    string[] require,

    string unlockText,
    string[] aliases,
    List<Feature> newFeatures,
    List<Item> newItems,
    Dictionary<Direction, Room> newExits
) : GameObject(name, desc, aliases)
{
    public string UnlockText { get; } = unlockText;
    public string[] Require { get; } = require;
    public int SequenceIdx = 0;

    public List<Feature> NewFeatures { get; } = newFeatures;
    public List<Item> NewItems { get; } = newItems;
    public Dictionary<Direction, Room> NewExits { get; set; } = newExits;

    public override string ToString()
    {
        return Name;
    }
}