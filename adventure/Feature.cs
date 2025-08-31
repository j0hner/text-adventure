class Feature(string name, string desc, string unlockText, string require, Feature? newFeature = null, Item? newItem = null, Dictionary<Direction, string>? newExit = null)
{
    public string Name { get; } = name;
    public string Desc { get; } = desc;

    public string UnlockText { get; } = unlockText;
    public string Require { get; } = require;

    public Feature? NewFeature { get; } = newFeature;
    public Item? NewItem { get; } = newItem;
    public Dictionary<Direction, string>? NewExit { get; } = newExit;
}