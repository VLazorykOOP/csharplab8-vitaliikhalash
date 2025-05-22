namespace Lab8CSharp
{
    public class FileStructureBuilder
    {
        public static void Task()
        {
            Console.Write("Enter the temporary folder path: ");
            string? tempFolderPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(tempFolderPath))
            {
                Console.WriteLine("Error: Empty input.");
                return;
            }

            if (!Directory.Exists(tempFolderPath))
            {
                string directory = Path.GetDirectoryName(tempFolderPath) ?? tempFolderPath;
                string fallbackPath = Path.Combine(directory, "temp");

                if (!Directory.Exists(fallbackPath))
                {
                    Console.WriteLine("Error: Directory does not exist.");
                    return;
                }

                tempFolderPath = fallbackPath;
            }

            string sampleData1Path = Path.Combine(tempFolderPath, "sample_data1");
            string sampleData2Path = Path.Combine(tempFolderPath, "sample_data2");

            Directory.CreateDirectory(sampleData1Path);
            Directory.CreateDirectory(sampleData2Path);

            string sampleData1File = Path.Combine(sampleData1Path, "sample_data1.txt");
            string sampleData2File = Path.Combine(sampleData1Path, "sample_data2.txt");

            File.WriteAllText(sampleData1File, "Шевченко Степан Іванович, 2001> року народження, місце проживання <м. Суми");
            File.WriteAllText(sampleData2File, "Комар Сергій Федорович, 2000 > року народження, місце проживання <м. Київ");

            string sampleData3File = Path.Combine(sampleData2Path, "sample_data3.txt");
            string combinedContent = File.ReadAllText(sampleData1File) + Environment.NewLine + File.ReadAllText(sampleData2File);
            File.WriteAllText(sampleData3File, combinedContent);

            Console.WriteLine("\nCreated files:");
            PrintFileInfo(sampleData1File);
            PrintFileInfo(sampleData2File);
            PrintFileInfo(sampleData3File);

            string movedSampleData2File = Path.Combine(sampleData2Path, "sample_data2.txt");
            File.Move(sampleData2File, movedSampleData2File, overwrite: true);

            string copiedSampleData1File = Path.Combine(sampleData2Path, "sample_data1.txt");
            File.Copy(sampleData1File, copiedSampleData1File, overwrite: true);

            string finalSampleDataPath = Path.Combine(tempFolderPath, "sample_data");
            if (Directory.Exists(finalSampleDataPath)) Directory.Delete(finalSampleDataPath, true);
            Directory.Move(sampleData2Path, finalSampleDataPath);
            Directory.Delete(sampleData1Path, true);

            Console.WriteLine("\nFinal file information in sample_data:");
            foreach (var file in Directory.GetFiles(finalSampleDataPath))
            {
                PrintFileInfo(file);
            }
        }

        private static void PrintFileInfo(string filePath)
        {
            FileInfo fi = new(filePath);
            Console.WriteLine($"File: {fi.Name}");
            Console.WriteLine($"  Path: {fi.FullName}");
            Console.WriteLine($"  Size: {fi.Length} bytes");
            Console.WriteLine($"  Created: {fi.CreationTime}");
            Console.WriteLine($"  Modified: {fi.LastWriteTime}");
            Console.WriteLine();
        }
    }
}
