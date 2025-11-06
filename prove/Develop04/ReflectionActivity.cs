using System;
using System.Collections.Generic;
using System.Threading;

// The ReflectionActivity class guides the user through reflective thinking prompts and questions.
public class ReflectionActivity : Activity
{
    // Lists store random prompts and reflection questions.
    private List<string> _reflectionPrompts;
    private List<string> _reflectionQuestions;

    // Static variable keeps track of the last used prompt across sessions to avoid repeats.
    private static string _lastPromptUsed = "";

    // Constructor initializes name, description, and all possible prompts/questions.
    public ReflectionActivity()
    {
        _name = "Reflecting";
        _description = "This activity will help you reflect on times in your life when you have shown strength and resilience.\nThis will help you recognize the power you have and how you can use it in other aspects of your life.";

        _reflectionPrompts = new List<string>()
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        _reflectionQuestions = new List<string>()
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different?",
            "What is your favorite thing about this experience?",
            "What did you learn about yourself?"
        };
    }

    // Runs the reflection session using random order and cycling for longer sessions.
    public override void Run()
    {
        StartMessage();
        string currentPrompt = DisplayUniquePrompt();

        Console.WriteLine("\nWhen you have something in mind, press enter to continue.");
        Console.ReadLine();

        Console.WriteLine("\nNow ponder on each of the following questions as they relate to this experience.");
        Console.Write("You may start in: ");
        CountDown(5);

        DateTime endTime = DateTime.Now.AddSeconds(_duration);
        List<string> questionsForSession = ShuffleQuestions(new List<string>(_reflectionQuestions));
        int index = 0;

        // Loop while time remains, reshuffling after all questions are used.
        while (DateTime.Now < endTime)
        {
            if (index >= questionsForSession.Count)
            {
                questionsForSession = ShuffleQuestions(new List<string>(_reflectionQuestions));
                index = 0;
            }

            string question = questionsForSession[index];
            Console.WriteLine($"> {question}");
            PauseWithAnimation(5);
            index++;
        }

        EndMessage();
    }

    // Displays one random reflection prompt that is not the same as the last one used.
    private string DisplayUniquePrompt()
    {
        List<string> availablePrompts = new List<string>(_reflectionPrompts);

        if (!string.IsNullOrEmpty(_lastPromptUsed))
        {
            availablePrompts.Remove(_lastPromptUsed);
        }

        Random rand = new Random();
        string selectedPrompt = availablePrompts[rand.Next(availablePrompts.Count)];

        Console.WriteLine($"\nConsider the following prompt:\n--- {selectedPrompt} ---");

        // Store the selected prompt to avoid immediate repetition next time.
        _lastPromptUsed = selectedPrompt;
        return selectedPrompt;
    }

    // Simple countdown prior to starting the reflection questions.
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

    // Randomizes the order of questions using the Fisher–Yates shuffle.
    private List<string> ShuffleQuestions(List<string> list)
    {
        Random rand = new Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);
            string temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }
}
