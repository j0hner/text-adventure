class GameObject(string name, string desc, string[]? aliases = null)
{
    public string Name { get; set; } = name;
    public string Desc { get; set; } = desc;
    public string[] Aliases = aliases ?? [];

    public bool Matches(string Name) => Name.ToLower() == this.Name.ToLower() || Aliases.Contains(Name.ToLower());
}