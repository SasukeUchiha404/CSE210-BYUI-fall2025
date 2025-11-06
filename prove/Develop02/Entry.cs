using System;

// Represents a single journal entry containing date, prompt, and response.
class Entry
{
    private string _dateText;
    private string _prompt;
    private string _entryText;

    public Entry(string date, string prompt, string entryText)
    {
        _dateText = date;
        _prompt = prompt;
        _entryText = entryText;
    }

    public string GetDateText() { return _dateText; }
    public string GetPrompt() { return _prompt; }
    public string GetEntryText() { return _entryText; }
}
