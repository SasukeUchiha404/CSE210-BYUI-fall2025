// ============================================================================
// Journal Console Application
// ----------------------------------------------------------------------------
// Description:
//    A menu-driven journaling program that allows users to write, display,
//    load, and save journal entries with random prompts.
//    - Prompts inspire each entry.
//    - Saves entries to text files with '|' separators.
//    - Handles missing or empty files gracefully.
// 
// Updated: November 6, 2025
// Standards:
//    - PascalCase for class/method names
//    - _camelCase for private fields
//    - Explicit getter/setter methods (no properties)
//    - Enhanced error handling and validation.
// ============================================================================

using System;

class Program
{
    static void Main()
    {
        Journal journal = new Journal();
        PromptGenerator promptGenerator = new PromptGenerator();

        Console.WriteLine("Welcome to the Journal Program!");
        bool firstRun = true;

        while (true)
        {
            DisplayMenu(firstRun);
            firstRun = false;

            string input = Console.ReadLine()?.Trim().ToLower();

            switch (input)
            {
                case "1":
                case "write":
                    string prompt = promptGenerator.GetPrompt();
                    Console.WriteLine($"\n{prompt}");
                    Console.Write("> ");
                    string response = Console.ReadLine()?.Trim();

                    string date = DateTime.Now.ToString("dd/MM/yyyy");
                    Entry entry = new Entry(date, prompt, response);
                    journal.AddEntry(entry);
                    break;

                case "2":
                case "display":
                    journal.Display();
                    break;

                case "3":
                case "load":
                    Console.WriteLine("Enter the filename to load:");
                    Console.Write("> ");
                    string loadFile = Console.ReadLine()?.Trim();
                    journal.LoadFile(loadFile);
                    break;

                case "4":
                case "save":
                    Console.WriteLine("Enter the filename to save:");
                    Console.Write("> ");
                    string saveFile = Console.ReadLine()?.Trim();
                    journal.SaveToFile(saveFile, append: true);
                    break;

                case "5":
                case "quit":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    break;
            }
        }
    }

    private static void DisplayMenu(bool showWelcome)
    {
        if (showWelcome)
            Console.WriteLine("Please select one of the following choices:");

        Console.WriteLine("1. Write");
        Console.WriteLine("2. Display");
        Console.WriteLine("3. Load");
        Console.WriteLine("4. Save");
        Console.WriteLine("5. Quit");
        Console.Write("What would you like to do? ");
    }
}
