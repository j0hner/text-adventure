class Item(string name, string desc, string[]? aliases = null) : GameObject(name, desc, aliases)
{
    public override string ToString()
    {
        return Name;
    }
}
