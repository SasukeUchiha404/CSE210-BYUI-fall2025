using System;
using System.Collections.Generic;
using System.Threading;

// The ListingActivity class helps users generate lists of items related to various positive prompts.
public class ListingActivity : Activity
{
    // Lists for possible prompts and items entered by the user.
    private List<string> _listingPrompts;
    private List<string> _userItems;

    // Static variable tracks the last used prompt between sessions to avoid immediate repeats.
    private static string _lastPromptUsed = "";

    // Constructor sets activity name, description, and initializes prompts.
    public ListingActivity()
    {
        _name = "Listing";
        _description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";

        _listingPrompts = new List<string>()
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        _userItems = new List<string>();
    }

    // Runs the listing activity loop.
    public override void Run()
    {
        StartMessage();
        string prompt = DisplayRandomPrompt();

        Console.WriteLine("You may begin in: ");
        CountDown(5);

        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        _userItems.Clear();

        // Allows user to submit as many items as time allows.
        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            string item = Console.ReadLine();
            _userItems.Add(item);
        }

        Console.WriteLine($"\nYou listed {_userItems.Count} items!");
        EndMessage();
    }

    // Displays one random listing prompt that is not the same as the previous one.
    private string DisplayRandomPrompt()
    {
        // Create a list of possible prompts except the one used last time.
        List<string> availablePrompts = new List<string>(_listingPrompts);
        if (!string.IsNullOrEmpty(_lastPromptUsed))
        {
            availablePrompts.Remove(_lastPromptUsed);
        }

        Random rand = new Random();
        string selectedPrompt = availablePrompts[rand.Next(availablePrompts.Count)];

        Console.WriteLine($"\nList as many responses as you can to the following prompt:\n--- {selectedPrompt} ---");

        // Store the currently used prompt to avoid immediate repeat next time.
        _lastPromptUsed = selectedPrompt;

        return selectedPrompt;
    }

    // Displays countdown before the user starts listing responses.
    private void CountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($" {i}");
            Thread.Sleep(1000);
            Console.Write("\b\b  \b\b");
        }
        Console.WriteLine();
    }
}
