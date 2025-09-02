using System.Text.Json;

internal class Adventure
{
    private static void Main()
    {
        // hall.Exits = new Dictionary<Direction, Room> { [Direction.North] = kitchen };
        // kitchen.Exits = new Dictionary<Direction, Room> { [Direction.South] = hall };

        // while (true)
        // {
        //     Console.Write("> ");
        //     string[] cmd = Console.ReadLine()!.Split(' ');
        //     string verb = cmd[0].ToLower();
        //     string response = "";

        //     if (new[] { "north", "n" }.Contains(verb)) response = player.North();
        //     else if (new[] { "south", "s" }.Contains(verb)) response = player.South();
        //     else if (new[] { "east", "e" }.Contains(verb)) response = player.East();
        //     else if (new[] { "west", "w" }.Contains(verb)) response = player.West();
        //     else if (new[] { "up", "u" }.Contains(verb)) response = player.Up();
        //     else if (new[] { "down", "d" }.Contains(verb)) response = player.Down();

        //     Console.WriteLine(response);
        // }

        WorldBuilder.MakeWorld("World.json");
    }
}