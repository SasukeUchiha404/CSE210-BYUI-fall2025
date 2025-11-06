using System;
using System.Threading;

// The BreathingActivity class guides the user through a breathing exercise.
public class BreathingActivity : Activity
{
    // Constructor sets specific name and description for this activity.
    public BreathingActivity()
    {
        _name = "Breathing";
        _description = "This activity will help you relax by walking you through breathing in and out slowly.\nClear your mind and focus on your breathing.";
    }

    // Overrides the Run method to execute breathing sequences.
    public override void Run()
    {
        StartMessage();
        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        // Runs during the specified time duration.
        while (DateTime.Now < endTime)
        {
            Console.Write("Breathe in...");
            CountDown(3);

            Console.Write("Now breathe out...");
            CountDown(3);
        }

        EndMessage();
    }

    // Displays a numeric countdown to help users pace their breathing.
    private void CountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write($" {i}");
            Thread.Sleep(1000);
            Console.Write("\b\b  \b\b");  // Erases the number after displaying
        }
        Console.WriteLine();
    }
}
