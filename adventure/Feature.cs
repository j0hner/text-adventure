class Feature(
    string name,
    string desc,
    string[] require,

    bool isSequential,
    string unlockText,
    string[] aliases,
    List<Feature> newFeatures,
    List<Item> newItems,
    Dictionary<Direction, Room> newExits
) : GameObject(name, desc, aliases)
{
    public string UnlockText { get; } = unlockText;
    public string[] Require { get; } = require;
    public bool IsSequential { get; } = isSequential;
    private int SequenceIdx = 0;

    public List<Feature> NewFeatures { get; } = newFeatures;
    public List<Item> NewItems { get; } = newItems;
    public Dictionary<Direction, Room> NewExits { get; set; } = newExits;
}