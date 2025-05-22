using System.Text.RegularExpressions;

namespace Lab8CSharp
{
    public partial class BinWordsProcessor
    {
        private static readonly int defaultNumber = 1;

        public static void Task()
        {
            Console.Write("Enter the file: ");
            string? filePath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Error: Empty input.");
                return;
            }

            if (!File.Exists(filePath))
            {
                string directory = Path.GetDirectoryName(filePath) ?? filePath;
                string fallbackPath = Path.Combine(directory, "input.txt");
                if (!File.Exists(fallbackPath))
                {
                    Console.WriteLine("Error: File does not exist.");
                    return;
                }
                filePath = fallbackPath;
            }

            string text;
            try
            {
                text = File.ReadAllText(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return;
            }

            Console.Write("Enter number to remove words with same amount of letters: ");
            int number = defaultNumber;
            try
            {
                string? input = Console.ReadLine() ?? throw new ArgumentNullException("Error: Empty input.");
                number = int.Parse(input);
            }
            catch
            {
                Console.WriteLine($"Wrong number format, defaulting with {defaultNumber}");
            }

            string regexLine = $@"\b\p{{L}}{{{number}}}\b";
            MatchCollection matches = Regex.Matches(text, regexLine);

            Console.WriteLine($"Words with {number} letters:");
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    Console.WriteLine(match.Value);
                }
                Console.WriteLine($"Total found: {matches.Count} words");
            }
            else
            {
                Console.WriteLine($"No words with {number} letters found.");
            }
        }
    }
}
