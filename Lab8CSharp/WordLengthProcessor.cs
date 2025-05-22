using System.Text.RegularExpressions;

namespace Lab8CSharp
{
    public partial class WordLengthProcessor
    {
        [GeneratedRegex(@"\s{2,}")]
        private static partial Regex CollapseMultipleSpaces();

        [GeneratedRegex(@"\s*,\s*,+")]
        private static partial Regex RemoveMultipleCommas();

        [GeneratedRegex(@"^\s*,\s*|,\s*$")]
        private static partial Regex RemoveLeadingTrailingCommas();

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
                string defaultFileName = "input.txt";
                string fallbackPath = Path.Combine(directory, defaultFileName);
                if (!File.Exists(fallbackPath))
                {
                    Console.WriteLine("Error: File does not exist.");
                    return;
                }
                filePath = fallbackPath;
            }

            Console.Write("Enter the word length to remove: ");
            string? lengthInput = Console.ReadLine();
            if (!int.TryParse(lengthInput, out int targetLength) || targetLength <= 0)
            {
                Console.WriteLine("Error: Please enter a valid positive number for word length.");
                return;
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

            string pattern = $@"\b[A-Za-z]{{{targetLength}}}\b";
            Regex removeWordsRegex = new(pattern, RegexOptions.Multiline);

            string resultText = removeWordsRegex.Replace(text, "");

            resultText = RemoveMultipleCommas().Replace(resultText, ",");
            resultText = RemoveLeadingTrailingCommas().Replace(resultText, "");
            resultText = CollapseMultipleSpaces().Replace(resultText, " ");
            resultText = resultText.Trim();

            string outputFile = "output.txt";
            string outputDir = Path.GetDirectoryName(filePath) ?? "";
            string outputPath = Path.Combine(outputDir, outputFile);

            try
            {
                File.WriteAllText(outputPath, resultText);
                Console.WriteLine($"Removed all words of length {targetLength}.");
                Console.WriteLine($"Result written to: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing result: {ex.Message}");
            }
        }
    }
}
