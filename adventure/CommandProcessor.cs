using System.Text.RegularExpressions;

static class CommandProcessor
{
    public static Dictionary<string, Func<Player, string[], string?>> Commands { get; } = new Dictionary<string, Func<Player, string[], string?>>()
    {
        ["exit"]        = (player, _) => player.Exit(),
        ["inventory"]   = (player, _) => player.ListInventory(),

        ["look"]        = (player, args) => player.Look(args),
        ["take"]        = (player, args) => player.Take(args),
        ["drop"]        = (player, args) => player.Drop(args),
        ["use"]         = (player, args) => player.Use(args),

        ["north"]       = (player, _) => player.North(),
        ["south"]       = (player, _) => player.South(),
        ["east"]        = (player, _) => player.East(),
        ["west"]        = (player, _) => player.West(),
        ["up"]          = (player, _) => player.Up(),
        ["down"]        = (player, _) => player.Down(),
        ["tutorial"]    = (player, _) => player.Tutorial(),
        ["help"]        = (player, _) => Help()
    };

    public static Dictionary<string, string> CommandAliases { get; } = new Dictionary<string, string>()
    {
        ["n"]       = "north",
        ["s"]       = "south",
        ["e"]       = "east",
        ["w"]       = "west",
        ["u"]       = "up",
        ["d"]       = "down",

        ["inv"]     = "inventory",
        ["i"]       = "inventory",

        ["examine"] = "look",
        ["x"]       = "look",
        ["look"]    = "look",
        ["l"]       = "look",
        ["insp"]    = "look",

        ["t"]       = "take",
        ["tk"]      = "take",
        ["get"]     = "take",
        ["grab"]    = "take",

        ["dr"]      = "drop",
        ["rm"]      = "drop",

        ["tutor"]   = "tutorial",

        ["quit"]    = "exit",
        ["bye"]     = "exit"
    };

    public static string? Process(Player player, string input)
    {
        var tokens = Regex.Matches(input, @"[\""].+?[\""]|\S+") // regex separate by space, but keep terms in quotes together
                          .Select(m => m.Value.Trim('"'))
                          .ToArray();

        if (tokens.Length == 0) return null;

        var command = tokens[0].ToLower();
        var args = tokens.Skip(1).ToArray();



        if (CommandAliases.TryGetValue(command, out var deAliased))
            command = deAliased; // de-alias command

        if (Commands.TryGetValue(command, out var action))
            return action(player, args); // look for actual command
        
        return $"Do what?"; // command not found
        
    }

    static string? Help()
    {
        Console.Clear();
        Console.WriteLine("Available commands:\n");

        foreach (var cmd in Commands.Keys.OrderBy(k => k))
        {
            var aliases = CommandAliases
                .Where(kvp => kvp.Value == cmd)
                .Select(kvp => kvp.Key)
                .OrderBy(a => a)
                .ToList();

            Console.Write($"{cmd,-12}");
            if (aliases.Any())
                Console.WriteLine($"(aliases: {string.Join(", ", aliases)})");
            else
                Console.WriteLine();
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
        return null;
    }
}