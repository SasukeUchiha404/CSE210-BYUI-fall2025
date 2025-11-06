using System;

// The Menu class manages user navigation between different mindfulness activities.
public class Menu
{
    // Displays the main options for the program.
    public void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("Menu Options:");
        Console.WriteLine("  1. Start Breathing Activity");
        Console.WriteLine("  2. Start Reflecting Activity");
        Console.WriteLine("  3. Start Listing Activity");
        Console.WriteLine("  4. Quit");
        Console.Write("Select a choice from the menu: ");
    }

    // Returns a new instance of the selected activity, or null if user chose to quit.
    public Activity SelectActivity()
    {
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                return new BreathingActivity();
            case "2":
                return new ReflectionActivity();
            case "3":
                return new ListingActivity();
            case "4":
                return null; // Indicates user wants to exit the program
            default:
                Console.WriteLine("\nInvalid choice. Please select a valid option.");
                PauseBeforeMenu();
                return null;
        }
    }

    // Runs the chosen activity or terminates if option 4 is selected.
    public bool RunSelectedActivity()
    {
        Activity activity = SelectActivity();

        if (activity != null)
        {
            Console.Clear();
            activity.Run();
            return true; // Continue running the main program loop
        }
        else
        {
            Console.WriteLine("\nGoodbye! Exiting program...");
            return false; // Return false to terminate the program
        }
    }

    // Small pause when invalid input is given before returning to the menu.
    private void PauseBeforeMenu()
    {
        Console.WriteLine("\nPress any key to return to the menu...");
        Console.ReadKey();
    }
}
