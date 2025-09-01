class GameObject(string name, string desc, string[]? aliases = null)
{
    public string Name { get; } = name;
    public string Desc { get; } = desc;
    public string[] Aliases = aliases ?? [];

    bool Matches(string Name) => Name == this.Name || Aliases.Contains(Name);
}