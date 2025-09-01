class Feature(string name, string desc, string unlockText, string require, string[]? aliases = null, Feature? newFeature = null, Item? newItem = null, Dictionary<Direction, string>? newExit = null)
: GameObject(name, desc, aliases)
{
    public string UnlockText { get; } = unlockText;
    public string Require { get; } = require;

    public Feature? NewFeature { get; } = newFeature;
    public Item? NewItem { get; } = newItem;
    public Dictionary<Direction, string>? NewExit { get; } = newExit;
}