// Program.cs
// Entry point for the Mindfulness Program.
// Exceeding Requirements: Includes spinner animations, structured inheritance, and clean termination.

using System;

// Main class controlling user interaction and application flow.
public class Program
{
    public static void Main(string[] args)
    {
        Menu menu = new Menu();
        bool running = true; // Controls the main loop

        // Repeat menu system until user selects Quit
        while (running)
        {
            menu.DisplayMenu();
            running = menu.RunSelectedActivity(); // Returns false if the user quits
        }

        // Once loop ends, terminate program gracefully.
        Environment.Exit(0);
    }
}
