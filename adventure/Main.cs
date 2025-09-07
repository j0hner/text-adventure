using System.Diagnostics;
using System.Text;

internal class Adventure
{
    const int width = 50;
    private static void Main()
    {
        Console.Clear();
        // Player player = WorldBuilder.MakeWorld("World.json");
        // while (true)
        // {
        //     Console.Write("\r=> ");
        //     string command = Console.ReadLine()!;
        //     string? response = CommandProcessor.Process(player, command);

        //     if (response == null)
        //     {
        //         Console.SetCursorPosition(0, Console.CursorTop - 1);
        //         continue;
        //     }
        //     Console.WriteLine(response);
        //     Console.WriteLine();
        // }

        Console.WriteLine(MakeLines("Idk quite a long test string. It has to be over 50 characters long to do something. Watch this peace of bs throw an exception.\nalso there is a newline just to see what that does", "idk sum command response. It also gotta be pretty long so lets just extend this bs to some serious length."));
    }

    static string MakeLines(string WorldDesc, string CommandResponse)
    {
        string top       = "╔" + new string('═', width - 2) + "╗";
        string middle    = "╟" + new string('─', width - 2) + "╢";
        string bottom    = "╚" + new string('═', width - 2) + "╝";
        string commandLine = "║ =>" + new string(' ', width - 5) + "║";
        string lineStart = "║ ";
        string lineEnd   = " ║";

        List<string> AddBounds(List<string> lines)
        {
            List<string> ret = new List<string>();

            foreach (string line in lines)
                ret.Add(lineStart + line + new string(' ', width - 4 - line.Length) + lineEnd); // add bounds to the lines and make them all the same length

            return ret;
        }

        List<string> ProcessedLines = new List<string>
        {
            top,
            //world desc goes here
            middle,
            //command response goes here
            middle,
            commandLine,
            bottom
        };

        List<string> worldDescLines = AddBounds(Wrap(WorldDesc, width - 2));

        ProcessedLines.InsertRange(1, worldDescLines);
        ProcessedLines.InsertRange(worldDescLines.Count + 2, AddBounds(Wrap(CommandResponse, width - 2)));

        return string.Join('\n', ProcessedLines);
    }

    static List<string> Wrap(string text, int maxLen)
    {
        var paragraphs = text.Split('\n');
        var lines = new List<string>();

        foreach (var para in paragraphs)
        {
            var words = para.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var current = new StringBuilder();

            foreach (var word in words)
            {
                if (current.Length + word.Length + 1 > maxLen - 4)
                {
                    lines.Add(current.ToString().TrimEnd());
                    current.Clear();
                }
                current.Append(word).Append(' ');
            }

            if (current.Length > 0)
                lines.Add(current.ToString().TrimEnd());
        }

        return lines;
    }
}