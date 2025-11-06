// ============================================================================
// Scripture Memorizer Console Application
// ----------------------------------------------------------------------------
// Description:
//     Interactive console app that helps users memorize scriptures easily.
//     - Loads and saves scriptures to scripture.txt
//     - Prompts user gracefully when file missing or invalid
//     - In memorization mode, words disappear progressively
//
// Updated: November 6, 2025
// Standards applied:
//     - PascalCase for class/method names
//     - _camelCase for fields
//     - Explicit getter/setter methods (no properties)
//     - Added input validation and safe file handling
// ============================================================================

using System;
using System.Collections.Generic;

namespace ScriptureMemorizer
{
    class Program
    {
        private const string _filePath = "scriptures.txt"; // Scripture storage file path

        static void Main()
        {
            // Attempt to load existing scriptures
            List<Scripture> scriptures = ScriptureFileHelper.LoadScriptures(_filePath);

            // If no file or empty, notify user to add manually
            if (scriptures.Count == 0)
            {
                Console.WriteLine("No existing scriptures found.");
                Console.WriteLine("Please add new scriptures manually through the menu.\n");
            }

            // Main menu loop
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Scripture Memorizer ===");
                Console.WriteLine("1. Start Memorizing (Random)");
                Console.WriteLine("2. Choose a Scripture to Memorize");
                Console.WriteLine("3. View All Scriptures");
                Console.WriteLine("4. Add a New Scripture");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option (1–5): ");

                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        StartRandomMemorizer(scriptures);
                        break;
                    case "2":
                        ChooseAndMemorize(scriptures);
                        break;
                    case "3":
                        ViewAllScriptures(scriptures);
                        break;
                    case "4":
                        AddNewScripture(scriptures);
                        break;
                    case "5":
                        return; // Exit application
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again...");
                        Console.ReadLine();
                        break;
                }
            }
        }

        // Starts memorization with a random scripture.
        private static void StartRandomMemorizer(List<Scripture> scriptures)
        {
            if (scriptures.Count == 0)
            {
                Console.WriteLine("\nNo scriptures available. Please add one first.");
                Console.ReadLine();
                return;
            }

            Random random = new Random();
            Scripture scripture = scriptures[random.Next(scriptures.Count)];
            scripture.Reset();
            RunMemorizationLoop(scripture);
        }

        // Allows user to choose a scripture to memorize.
        private static void ChooseAndMemorize(List<Scripture> scriptures)
        {
            if (scriptures.Count == 0)
            {
                Console.WriteLine("\nNo scriptures found. Add one first!");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Choose a Scripture ===");
            for (int i = 0; i < scriptures.Count; i++)
            {
                string reference = scriptures[i].GetReference().ToDisplayString();
                string preview = scriptures[i].GetOriginalText().Split(' ')[0];
                Console.WriteLine($"{i + 1}. {reference} - {preview}...");
            }

            Console.Write("\nSelect the number of a scripture: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= scriptures.Count)
            {
                Scripture scripture = scriptures[choice - 1];
                scripture.Reset();
                RunMemorizationLoop(scripture);
            }
            else
            {
                Console.WriteLine("Invalid choice. Press Enter to return.");
                Console.ReadLine();
            }
        }

        // Performs word hiding until user quits or text is fully hidden.
        private static void RunMemorizationLoop(Scripture scripture)
        {
            while (true)
            {
                Console.Clear();
                scripture.Display();
                Console.WriteLine("\nPress Enter to hide more words or type 'quit' to stop.");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "quit")
                    break;

                scripture.HideRandomWords();

                if (scripture.IsCompletelyHidden())
                {
                    Console.Clear();
                    scripture.Display();
                    Console.WriteLine("\nAll words hidden. Great job!");
                    Console.WriteLine("Press Enter to return...");
                    Console.ReadLine();
                    break;
                }
            }
        }

        // Displays all currently stored scriptures.
        private static void ViewAllScriptures(List<Scripture> scriptures)
        {
            Console.Clear();
            if (scriptures.Count == 0)
            {
                Console.WriteLine("No scriptures stored.");
            }
            else
            {
                Console.WriteLine("=== All Scriptures ===\n");
                foreach (Scripture s in scriptures)
                {
                    Console.WriteLine($"{s.GetReference().ToDisplayString()} — {s.GetOriginalText()}\n");
                }
            }
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
        }

        // Adds a user-provided scripture.
        private static void AddNewScripture(List<Scripture> scriptures)
        {
            Console.Clear();
            Console.WriteLine("=== Add a New Scripture ===");

            Console.Write("Book: ");
            string book = Console.ReadLine()?.Trim();

            int chapter = SafeIntInput("Chapter: ");
            int startVerse = SafeIntInput("Start verse: ");
            int endVerse = SafeIntInput("End verse (same as start if single verse): ", startVerse);

            Console.Write("Enter the full scripture text: ");
            string text = Console.ReadLine()?.Trim();

            Reference reference = new Reference(book, chapter, startVerse, endVerse);
            Scripture scripture = new Scripture(reference, text);
            scriptures.Add(scripture);
            ScriptureFileHelper.SaveScriptures(_filePath, scriptures);

            Console.WriteLine("\nScripture added successfully! Press Enter to return...");
            Console.ReadLine();
        }

        // Helper method for safe integer input.
        private static int SafeIntInput(string prompt, int defaultValue = 1)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && result > 0)
                    return result;

                if (string.IsNullOrWhiteSpace(input))
                    return defaultValue;

                Console.WriteLine("Please enter a valid positive number.");
            }
        }
    }
}
