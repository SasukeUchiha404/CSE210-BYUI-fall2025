using System;
using System.Collections.Generic;

namespace ScriptureMemorizer
{
    // Main program class for interacting with the user.
    class Program
    {
        private const string FilePath = "scriptures.txt"; // Scripture data file

        static void Main()
        {
            // Load existing scriptures from file
            List<Scripture> scriptures = ScriptureFileHelper.LoadScriptures(FilePath);

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

        // Start memorization with a random scripture from the list
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
            scripture.Reset();  // Reset word visibility so scripture is fully visible
            RunMemorizationLoop(scripture);
        }

        // Allows user to pick a scripture and memorize it
        private static void ChooseAndMemorize(List<Scripture> scriptures)
        {
            if (scriptures.Count == 0)
            {
                Console.WriteLine("\nNo scriptures available. Add one first!");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== Select a Scripture ===");
            for (int i = 0; i < scriptures.Count; i++)
            {
                var referenceText = scriptures[i].GetReference().ToDisplayString();
                var previewWords = scriptures[i].GetOriginalText().Split(' ');
                string preview = string.Join(" ", previewWords[..Math.Min(5, previewWords.Length)]);
                Console.WriteLine($"{i + 1}. {referenceText} - {preview}...");
            }

            Console.Write("\nEnter the number of the scripture: ");
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice > 0 && choice <= scriptures.Count)
            {
                var scripture = scriptures[choice - 1];
                scripture.Reset();  // Reset visibility before memorizing
                RunMemorizationLoop(scripture);
            }
            else
            {
                Console.WriteLine("Invalid selection. Press Enter to return.");
                Console.ReadLine();
            }
        }

        // Loop showing scripture and hiding words progressively until finished or quit
        private static void RunMemorizationLoop(Scripture scripture)
        {
            while (true)
            {
                Console.Clear();
                scripture.Display();

                Console.WriteLine("\nPress Enter to hide more words or type 'quit' to stop.");
                string input = Console.ReadLine();

                if (input?.Trim().ToLower() == "quit")
                    break;

                scripture.HideRandomWords();

                if (scripture.IsCompletelyHidden())
                {
                    Console.Clear();
                    scripture.Display();
                    Console.WriteLine("\nAll words hidden. Great work!");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    break;
                }
            }
        }

        // Displays all scriptures currently stored
        private static void ViewAllScriptures(List<Scripture> scriptures)
        {
            Console.Clear();
            if (scriptures.Count == 0)
            {
                Console.WriteLine("No scriptures available.");
            }
            else
            {
                Console.WriteLine("=== All Stored Scriptures ===\n");
                foreach (var s in scriptures)
                {
                    Console.WriteLine($"{s.GetReference().ToDisplayString()} — {s.GetOriginalText()}");
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
        }

        // Adds a new scripture entry and saves it
        private static void AddNewScripture(List<Scripture> scriptures)
        {
            Console.Clear();
            Console.WriteLine("=== Add New Scripture ===");

            Console.Write("Book: ");
            string book = Console.ReadLine()?.Trim();

            Console.Write("Chapter: ");
            int chapter = int.Parse(Console.ReadLine() ?? "1");

            Console.Write("Start verse: ");
            int startVerse = int.Parse(Console.ReadLine() ?? "1");

            Console.Write("End verse (same as start if single verse): ");
            int endVerse = int.Parse(Console.ReadLine() ?? startVerse.ToString());

            Console.Write("Enter the full scripture text: ");
            string text = Console.ReadLine()?.Trim();

            var reference = new Reference(book, chapter, startVerse, endVerse);
            var scripture = new Scripture(reference, text);
            scriptures.Add(scripture);
            ScriptureFileHelper.SaveScriptures(FilePath, scriptures);

            Console.WriteLine("\nScripture added successfully! Press Enter to return...");
            Console.ReadLine();
        }
    }
}
