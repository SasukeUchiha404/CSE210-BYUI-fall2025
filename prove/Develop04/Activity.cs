using System;
using System.Threading;

// The base class for all mindfulness activities.
// It defines shared attributes and common methods for timing, messages, and animations.
public class Activity
{
    // Private member variables (encapsulated attributes)
    protected string _name;
    protected string _description;
    protected int _duration;

    // Displays the activity's welcome message and asks user for duration.
    public void StartMessage()
    {
        Console.WriteLine($"Welcome to the {_name} Activity.\n");
        Console.WriteLine(_description);
        Console.Write("\nHow long, in seconds, would you like for your session? ");
        _duration = int.Parse(Console.ReadLine());
        Console.WriteLine("\nGet ready...");
        PauseWithAnimation(3);
        Console.Clear();
    }

    // Displays the activity's closing message and waits a few seconds.
    public void EndMessage()
    {
        Console.WriteLine("\nWell done!!");
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name} Activity.");
        PauseWithAnimation(3);
    }

    // Simple animated spinner for timed pauses.
    public void PauseWithAnimation(int seconds)
    {
        string[] spinner = new string[] { "-", "\\", "|", "/" };
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(spinner[i % 4]);
            Thread.Sleep(250);      // Waits for a short time between spinner frames
            Console.Write("\b");    // Deletes last spinner character
            i++;
        }
        Console.WriteLine();
    }

    // Placeholder Run method that subclasses override.
    public virtual void Run()
    {
        StartMessage();
        EndMessage();
    }
}
