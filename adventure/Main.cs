internal class Adventure
{
    private static void Main()
    {
        Console.Clear();
        Player player = WorldBuilder.MakeWorld("World.json");
        while (true)
        {
            Console.Write("\r=> ");
            string command = Console.ReadLine()!;
            string? response = CommandProcessor.Process(player, command);

            if (response == null)
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                continue;
            }
            Console.WriteLine(response);
            Console.WriteLine();
        }
    }
}