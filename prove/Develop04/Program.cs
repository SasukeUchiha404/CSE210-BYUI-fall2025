// ============================================================================
// Mindfulness Program
// ----------------------------------------------------------------------------
// Description:
//     This console-based application guides users through three mindfulness
//     activities to promote focus and relaxation:
//       1. Breathing Activity
//       2. Reflection Activity
//       3. Listing Activity
//     Each activity tracks its own duration and uses simple console animations.
//
// Menu Options:
//     === Mindfulness Program ===
//     1. Start Breathing Activity
//     2. Start Reflecting Activity
//     3. Start Listing Activity
//     4. Quit
//
// Standards Applied:
//     - C# language with PascalCase class/method names
//     - _camelCase for private fields
//     - Explicit getter and setter methods where needed
//     - Each class in its own file named accordingly (e.g., Activity.cs)
//     - Consistent formatting and whitespace for readability
//
// Updated: November 6, 2025
// Author:  Christian Chan
// ============================================================================

using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Mindfulness Program ===");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflecting Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. Quit");
            Console.Write("Select an option (1–4): ");

            string input = Console.ReadLine()?.Trim();

            switch (input)
            {
                case "1":
                    BreathingActivity breathing = new BreathingActivity();
                    breathing.Run();
                    break;

                case "2":
                    ReflectionActivity reflection = new ReflectionActivity();
                    reflection.Run();
                    break;

                case "3":
                    ListingActivity listing = new ListingActivity();
                    listing.Run();
                    break;

                case "4":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Invalid selection. Press Enter to try again...");
                    Console.ReadLine();
                    break;
            }
        }
    }
}
