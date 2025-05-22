using System.Text.RegularExpressions;

namespace Lab8CSharp
{
    public partial class IpAddressProcessor
    {
        private const string Octet = @"(25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)";

        [GeneratedRegex($@"{Octet}\.{Octet}\.{Octet}\.{Octet}")]
        private static partial Regex IpAddressRegex();

        [GeneratedRegex(@"\b")]
        private static partial Regex WordBoundaryRegex();

        private readonly Regex _ipRegex;
        private readonly string _filePath;
        private readonly List<string> _matchedLines;

        public IpAddressProcessor(string filePath)
        {
            _ipRegex = IpAddressRegex();
            _filePath = ValidateAndGetFilePath(filePath);
            _matchedLines = [];
        }

        public void ProcessFile()
        {
            if (string.IsNullOrEmpty(_filePath))
            {
                Console.WriteLine("Error: Invalid file path.");
                return;
            }

            if (!ReadAndExtractIpAddresses())
                return;

            if (!WriteInitialResults())
                return;

            ProcessTextModifications();
            WriteResults();
        }

        private string ValidateAndGetFilePath(string? filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                Console.WriteLine("Error: Empty input.");
                return string.Empty;
            }

            if (File.Exists(filePath))
                return filePath;

            string directory = Path.GetDirectoryName(filePath) ?? filePath;
            string defaultFileName = "input.txt";
            string fallbackPath = Path.Combine(directory, defaultFileName);

            if (File.Exists(fallbackPath))
                return fallbackPath;

            Console.WriteLine("Error: File does not exist.");
            return string.Empty;
        }

        private bool ReadAndExtractIpAddresses()
        {
            string[] lines;

            try
            {
                lines = File.ReadAllLines(_filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return false;
            }

            _matchedLines.Clear();

            foreach (string line in lines)
            {
                MatchCollection matches = _ipRegex.Matches(line);
                foreach (Match match in matches)
                {
                    _matchedLines.Add(match.Value);
                }
            }

            return true;
        }

        private bool WriteInitialResults()
        {
            string outputPath = GetOutputPath();

            try
            {
                File.WriteAllLines(outputPath, _matchedLines);
                Console.WriteLine($"Found {_matchedLines.Count} IP addresses.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing file: {ex.Message}");
                return false;
            }
        }

        private void ProcessTextModifications()
        {
            ProcessTextRemoval();
            ProcessTextReplacement();
        }

        private void ProcessTextRemoval()
        {
            Console.Write("Octate to remove: ");
            string? removeWord = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(removeWord))
            {
                Console.WriteLine("Error: Empty input.");
                return;
            }

            string pattern = $@"\b{Regex.Escape(removeWord)}\b";
            Regex removeRegex = new(pattern);

            for (int i = 0; i < _matchedLines.Count; i++)
            {
                _matchedLines[i] = removeRegex.Replace(_matchedLines[i], "0");
            }
        }

        private void ProcessTextReplacement()
        {
            Console.Write("Octate to replace: ");
            string? targetWord = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(targetWord))
            {
                Console.WriteLine("Error: Empty input.");
                return;
            }

            Console.Write("Octate to replace with: ");
            string? replaceWord = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(replaceWord))
            {
                Console.WriteLine("Error: Empty input.");
                return;
            }

            string pattern = $@"\b{Regex.Escape(targetWord)}\b";
            Regex replaceRegex = new(pattern);

            for (int i = 0; i < _matchedLines.Count; i++)
            {
                _matchedLines[i] = replaceRegex.Replace(_matchedLines[i], replaceWord);
            }
        }

        private void WriteResults()
        {
            string outputPath = GetOutputPath();

            try
            {
                File.WriteAllLines(outputPath, _matchedLines);
                Console.WriteLine($"Result written to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing result: {ex.Message}");
            }
        }

        private string GetOutputPath()
        {
            string outputFile = "output.txt";
            string outputDir = Path.GetDirectoryName(_filePath) ?? "";
            return Path.Combine(outputDir, outputFile);
        }

        public static void Task()
        {
            Console.Write("Enter the file: ");
            string? filePath = Console.ReadLine();

            var processor = new IpAddressProcessor(filePath ?? string.Empty);
            processor.ProcessFile();
        }
    }
}
