using System;
using System.Collections.Generic;

// Class responsible for providing random prompts from a predefined list
class PromptGenerator
{
    // List of prompts from which to randomly select
    private List<string> _listPrompt = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        "What did I learn today?",
        "What made me smile today?"
    };

    // Returns a random prompt string from the list
    public string GetPrompt()
    {
        Random rnd = new Random();                // Create new random number generator
        int index = rnd.Next(_listPrompt.Count); // Generate a random index within list bounds
        return _listPrompt[index];                // Return the randomly selected prompt
    }
}

