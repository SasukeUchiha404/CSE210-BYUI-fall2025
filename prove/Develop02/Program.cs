using System;

// Main program class containing the entry point of the application
class Program
{
    static void Main()
    {
        Journal journal = new Journal();                 // Create a new Journal instance to manage entries
        PromptGenerator promptGenerator = new PromptGenerator();  // Create a PromptGenerator with a list of prompts

        Console.WriteLine("Welcome to the Journal Program!");    // Show welcome message once at program start
        bool firstRun = true;                                     // Flag to control initial welcome message display

        while (true)                                             // Main program loop for menu-driven interface
        {
            if (firstRun)                                        // On first loop iteration,
            {
                DisplayMenu(showWelcome: true);                  // Display menu with welcome message
                firstRun = false;                                 // Reset flag to prevent repeated welcome
            }
            else
            {
                DisplayMenu(showWelcome: false);                 // Display menu without welcome message on later runs
            }

            string input = Console.ReadLine().Trim();             // Read user input and trim whitespace
            string choice = input.ToLower();                       // Case-fold input for flexible matching

            if (choice == "1" || choice == "write")               // Option 1 or text "write": create new entry
            {
                string prompt = promptGenerator.GetPrompt();     // Get a random prompt
                Console.WriteLine($"\n{prompt}");                 // Display prompt with newline before it
                Console.Write("> ");                               // Show input prompt symbol
                string response = Console.ReadLine();             // Read user's journal response

                // Get current system date formatted as dd/MM/yyyy
                string date = DateTime.Now.ToString("dd/MM/yyyy");

                Entry entry = new Entry(date, prompt, response);  // Create an Entry object with date, prompt, and response
                journal.AddEntry(entry);                           // Add this entry to the journal
            }
            else if (choice == "2" || choice == "display")         // Option 2 or "display": show all entries
            {
                journal.Display();                                  // Call journal to print out all stored entries
            }
            else if (choice == "3" || choice == "load")            // Option 3 or "load": load journal from file
            {
                Console.WriteLine("What is the filename?");        // Prompt for filename
                Console.Write("> ");
                string filename = Console.ReadLine();              // Read filename input
                journal.LoadFile(filename);                          // Load entries from specified file, replacing current entries
            }
            else if (choice == "4" || choice == "save")            // Option 4 or "save": save journal to file
            {
                Console.WriteLine("What is the filename?");        // Prompt for filename
                Console.Write("> ");
                string filename = Console.ReadLine();              // Read filename input
                journal.SaveToFile(filename, append: true);        // Save or append entries to the specified file
            }
            else if (choice == "5" || choice == "quit")            // Option 5 or "quit": exit program
            {
                break;                                             // Exit the while loop and end program
            }
            else                                                   // Invalid input not matching options
            {
                Console.WriteLine("Invalid choice. Please try again.");  // Show error message, then loop again
            }
        }
    }

    // Displays the menu; optionally shows welcome message on first display
    static void DisplayMenu(bool showWelcome)
    {
        if (showWelcome)
        {
            Console.WriteLine("Please select one of the following choices");
        }

        // Menu options always displayed
        Console.WriteLine("1. Write");
        Console.WriteLine("2. Display");
        Console.WriteLine("3. Load");
        Console.WriteLine("4. Save");
        Console.WriteLine("5. Quit");
        Console.Write("What would you like to do? ");  // Prompt user for choice
    }
}
