using System;
using System.Collections.Generic;
using System.IO;

// Represents the journal and manages file interaction and entries.
class Journal
{
    private List<Entry> _entries = new List<Entry>();

    // Adds a new entry to the journal.
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
        Console.WriteLine("Entry added successfully.\n");
    }

    // Displays all entries in the journal.
    public void Display()
    {
        Console.Clear();
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries available.\n");
            return;
        }

        Console.WriteLine("=== Journal Entries ===\n");
        foreach (Entry e in _entries)
        {
            Console.WriteLine($"Date: {e.GetDateText()} - Prompt: {e.GetPrompt()}");
            Console.WriteLine(e.GetEntryText());
            Console.WriteLine();
        }
    }

    // Saves current entries to a file, optionally appending.
    public void SaveToFile(string filename, bool append = false)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Invalid filename. Save cancelled.\n");
            return;
        }

        try
        {
            using (StreamWriter writer = new StreamWriter(filename, append))
            {
                foreach (Entry e in _entries)
                    writer.WriteLine($"{e.GetDateText()}|{e.GetPrompt()}|{e.GetEntryText()}");
            }

            Console.WriteLine("Journal saved successfully.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}\n");
        }
    }

    // Loads journal entries from a specified text file.
    public void LoadFile(string filename)
    {
        if (string.IsNullOrWhiteSpace(filename))
        {
            Console.WriteLine("Filename cannot be empty.\n");
            return;
        }

        _entries.Clear();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found. Please check the name and try again.\n");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);

            if (lines.Length == 0)
            {
                Console.WriteLine("File found but contains no entries. Add new ones to begin.\n");
                return;
            }

            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    Entry e = new Entry(parts[0], parts[1], parts[2]);
                    _entries.Add(e);
                }
            }

            Console.WriteLine("Journal loaded successfully.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}\n");
        }
    }
}
