using System;

// Represents a single journal entry with date, prompt shown, and user's response
class Entry
{
    public string DateText { get; }           // Date string for the entry
    public string Prompt { get; }             // Prompt text shown when entry was created
    public string EntryText { get; }          // User's journal response for this entry

    // Constructor initializes all properties
    public Entry(string date, string prompt, string entryText)
    {
        DateText = date;
        Prompt = prompt;
        EntryText = entryText;
    }
}
