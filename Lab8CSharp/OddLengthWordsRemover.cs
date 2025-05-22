using System.Text.RegularExpressions;

namespace Lab8CSharp
{
    public partial class OddLengthWordsRemover
    {
        [GeneratedRegex(@"\b\p{L}(\p{L}{2})*\b")]
        private static partial Regex RemoveOddLengthWords();

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

            string result = RemoveOddLengthWords().Replace(text, "");

            result = RemoveMultipleCommas().Replace(result, ",");
            result = RemoveLeadingTrailingCommas().Replace(result, "");
            result = CollapseMultipleSpaces().Replace(result, " ").Trim();

            string outputFile = "output.txt";
            string outputDir = Path.GetDirectoryName(filePath) ?? "";
            string outputPath = Path.Combine(outputDir, outputFile);

            try
            {
                File.WriteAllText(outputPath, result);
                Console.WriteLine($"Result written to: {outputFile}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing result: {ex.Message}");
            }
        }
    }
}
