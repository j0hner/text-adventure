using System.Text.RegularExpressions;

static class CommandProcessor
{
    static Dictionary<string, Func<Player, string[], string>> Commands = new Dictionary<string, Func<Player, string[], string>>()
    {
        // ["look"] = (player, args) => player.Look(args),
        // ["take"]  = (player, args) => player.Take(args),
        // ["drop"]  = (player, args) => player.Drop(args),
        ["north"] = (player, args) => player.North(),
        ["south"] = (player, args) => player.South(),
        ["east"] = (player, args) => player.East(),
        ["west"] = (player, args) => player.West(),
        ["up"] = (player, args) => player.Down(),
        ["down"] = (player, args) => player.Down(),
        ["exit"] = (player, args) => player.Exit(),
    };

    static Dictionary<string, string> CommandAliases = new Dictionary<string, string>()
    {
        ["n"] = "north",
        ["s"] = "south",
        ["e"] = "east",
        ["w"] = "west",
        ["u"] = "up",
        ["d"] = "down",

        ["inv"] = "inventory",
        ["i"] = "inventory",

        ["examine"] = "look",
        ["x"] = "look",
        ["look"] = "look",
        ["l"] = "look",
        ["insp"] = "look",

        ["get"] = "take",
        ["grab"] = "take",

        ["dr"] = "drop",
        ["rm"] = "drop",

        ["inv"] = "inventory",

        ["quit"] = "exit",
        ["bye"] = "exit"
    };

    public static string? Process(Player player, string input)
    {
        var tokens = Regex.Matches(input, @"[\""].+?[\""]|\S+")
                          .Select(m => m.Value.Trim('"'))
                          .ToArray();

        if (tokens.Length == 0) return null;

        var command = tokens[0].ToLower();
        var args = tokens.Skip(1).ToArray();

        if (Commands.TryGetValue(command, out var action))
            return action(player, args); // look for actual command
        else if (CommandAliases.TryGetValue(command, out var deAliased))
            return Commands[deAliased](player, args); // look for alias
        
        
        return $"Unknown command: {command}"; // command not found
        
    }
}