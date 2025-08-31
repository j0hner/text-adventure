internal class Program
{
    Dictionary<string, Room> rooms = new Dictionary<string, Room>
    {
        ["hall"] = new Room(
            "Hall",
            "You are in a grand hall with marble floors.",
            new Dictionary<Direction, string>
            {
                [Direction.North] = "kitchen"
            }
        ),
        ["kitchen"] = new Room(
            "Kitchen",
            "A dark, dusty kitchen. The smell is awful.",
            new Dictionary<Direction, string>
            {
                [Direction.South] = "hall"
            }
        )
    };

    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}