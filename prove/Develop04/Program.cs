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
        Menu menu = new Menu();

        bool continueRunning = true;
        while (continueRunning)
        {
            menu.DisplayMenu();
            continueRunning = menu.RunSelectedActivity();
        }
    }
}
