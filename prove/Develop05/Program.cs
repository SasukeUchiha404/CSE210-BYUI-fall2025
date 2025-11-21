// ============================================================================
// Eternal Quest Program
// ----------------------------------------------------------------------------
// Description:
//     Console app for tracking diverse personal goals using gamification.
//     Supports: one-time, eternal, checklist, progress and negative goals,
//     earning points, bonus, experience and leveling up. Data can be saved/
//     loaded. Shows creativity via progress+negative goals, experience & levels.
//
// Standards Applied:
//     - C# language (PascalCase classes/methods; _camelCase private fields)
//     - Encapsulated, abstracted, polymorphic class structure
//     - Full top block documentation
//     - Each class in its own .cs file
//     - Consistent formatting and whitespace
//
// Creativity/Exceeding Requirements: Experience and level, progress & negative goals
// Author: Christian Chan
// Date: Nov 19, 2025
// ============================================================================

using System;

class Program
{
    static void Main(string[] args)
    {
        // Create a User object to hold all goals, score, experience, etc.
        User user = new User();

        // Begin the main menu loop
        while (true)
        {
            Console.Clear();

            // Display app title and current user stats at the top of each menu view
            Console.WriteLine("=== Eternal Quest ===");
            Console.WriteLine("Score: " + user.GetScore() + " | Level: " + user.GetLevel() + " | Experience: " + user.GetExperience());

            // Show the main menu options
            Console.WriteLine("Menu:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. Record Event");
            Console.WriteLine("  3. Show Goals");
            Console.WriteLine("  4. Save Progress");
            Console.WriteLine("  5. Load Progress");
            Console.WriteLine("  6. Quit");
            Console.Write("Select an option (1-6): ");

            // Get user menu choice (as a trimmed string)
            string choice = Console.ReadLine()?.Trim();

            // Main menu logic: call User methods for each feature
            switch (choice)
            {
                case "1":
                    // User chooses to add a new goal ("Create New Goal")
                    user.AddGoalInteractively();
                    break;
                case "2":
                    // User chooses to record progress on a goal ("Record Event")
                    user.RecordGoalInteractively();
                    break;
                case "3":
                    // User chooses to see current goals and their statuses
                    user.ShowGoals();
                    break;
                case "4":
                    // User saves their current progress to a file
                    user.SaveProgress();
                    break;
                case "5":
                    // User loads progress from a file
                    user.LoadProgress();
                    break;
                case "6":
                    // User chooses to quit - thank them and exit the loop
                    Console.WriteLine("Thanks for using Eternal Quest! (Press Enter to exit)");
                    Console.ReadLine();
                    return;
                default:
                    // Defensive: If input is not recognized (not 1-6), show error and continue loop
                    Console.WriteLine("Invalid choice. Press Enter to try again.");
                    Console.ReadLine();
                    break;
            }
        }
    }
}

