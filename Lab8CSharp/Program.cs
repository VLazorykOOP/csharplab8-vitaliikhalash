namespace Lab8CSharp
{
    internal class Program
    {
        private static void Main()
        {
            Console.Write("Enter option 1-5: ");
            bool isValid = int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= 5;

            while (!isValid)
            {
                Console.Write("Please enter a valid option. Enter option 1-5: ");
                isValid = int.TryParse(Console.ReadLine(), out option) && option >= 1 && option <= 5;
            }

            switch (option)
            {
                case 1: IpAddressProcessor.Task(); break;
                case 2: WordLengthProcessor.Task(); break;
                case 3: OddLengthWordsRemover.Task(); break;
                case 4: BinWordsProcessor.Task(); break;
                case 5: FileStructureBuilder.Task(); break;
            }
        }
    }
}
