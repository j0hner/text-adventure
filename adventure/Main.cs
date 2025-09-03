using System.Text.Json;

internal class Adventure
{
    private static void Main()
    {
        Console.Clear();
        Player player = WorldBuilder.MakeWorld("World.json");
        while (true)
        {
            Console.Write("-----------------------------------------------\n=> ");
            string[] cmd = Console.ReadLine()!.Split(' ');
            string verb = cmd[0].ToLower();
            string response = "";

            if (new[] { "north", "n" }.Contains(verb)) response = player.North();
            else if (new[] { "south", "s" }.Contains(verb)) response = player.South();
            else if (new[] { "east", "e" }.Contains(verb)) response = player.East();
            else if (new[] { "west", "w" }.Contains(verb)) response = player.West();
            else if (new[] { "up", "u" }.Contains(verb)) response = player.Up();
            else if (new[] { "down", "d" }.Contains(verb)) response = player.Down();
            else if (new[] { "bye", "quit" }.Contains(verb)) return;

            Console.WriteLine(response);
        }

        
    }
}