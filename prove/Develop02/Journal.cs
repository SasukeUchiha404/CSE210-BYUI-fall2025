using System;
using System.Collections.Generic;
using System.IO;

// Class representing the journal, manages multiple entries and file operations
class Journal
{
    private List<Entry> _listEntry = new List<Entry>();  // Private list holding currently loaded journal entries

    // Adds a new journal entry to the list
    public void AddEntry(Entry entry)
    {
        _listEntry.Add(entry);
    }

    // Displays all journal entries with formatting for readability
    public void Display()
    {
        Console.WriteLine();                              // Blank line before display output
        foreach (Entry entry in _listEntry)
        {
            Console.WriteLine($"Date: {entry.DateText} - Prompt: {entry.Prompt}");  // Show date and prompt for each entry
            Console.WriteLine(entry.EntryText);           // Show the user's journal response
            Console.WriteLine();                          // Blank line after each entry for separation
        }
    }

    // Saves all current journal entries to a file; appends if specified
    public void SaveToFile(string filename, bool append = false)
    {
        using (StreamWriter writer = new StreamWriter(filename, append))  // Open file for write with append option
        {
            // Write each entry as one line with fields separated by '|'
            foreach (Entry entry in _listEntry)
            {
                writer.WriteLine($"{entry.DateText}|{entry.Prompt}|{entry.EntryText}");
            }
        }
        Console.WriteLine("Journal saved.");               // Inform user save was successful
    }

    // Loads journal entries from a specified file (replaces current entries)
    public void LoadFile(string filename)
    {
        _listEntry.Clear();                                // Clear all existing entries before loading

        if (File.Exists(filename))                         // Check if the file exists
        {
            using (StreamReader reader = new StreamReader(filename)) // Open file for reading
            {
                string line;
                while ((line = reader.ReadLine()) != null)  // Read each line until end of file
                {
                    string[] parts = line.Split('|');      // Each line expected to have 3 parts by '|'
                    if (parts.Length == 3)                 // Validate correct format before creating entry
                    {
                        Entry entry = new Entry(parts[0], parts[1], parts[2]);  // Create new Entry from line parts
                        _listEntry.Add(entry);            // Add entry to journal list
                    }
                }
            }
            Console.WriteLine("Journal loaded.");           // Inform user loading was successful
        }
        else
        {
            Console.WriteLine("File not found.");           // Error message if file does not exist
        }
    }
}


