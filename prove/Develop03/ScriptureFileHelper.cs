using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureMemorizer
{
    // Handles all file input/output for saving and loading scriptures.
    static class ScriptureFileHelper
    {
        // Loads all scriptures from a text file
        public static List<Scripture> LoadScriptures(string filePath)
        {
            var scriptures = new List<Scripture>();

            try
            {
                if (!File.Exists(filePath))
                    return scriptures;

                foreach (var line in File.ReadAllLines(filePath))
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    // Expected format: Book|Chapter|StartVerse|EndVerse|Text
                    string[] parts = line.Split('|');
                    if (parts.Length < 5) continue;

                    string book = parts[0];
                    int chapter = int.Parse(parts[1]);
                    int startVerse = int.Parse(parts[2]);
                    int endVerse = int.Parse(parts[3]);
                    string text = parts[4];

                    var reference = new Reference(book, chapter, startVerse, endVerse);
                    scriptures.Add(new Scripture(reference, text));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading scriptures: {ex.Message}");
            }

            return scriptures;
        }

        // Saves all scriptures back to the file
        public static void SaveScriptures(string filePath, List<Scripture> scriptures)
        {
            try
            {
                var lines = scriptures.Select(s =>
                {
                    var r = s.GetReference();
                    return $"{r.Book}|{r.Chapter}|{r.StartVerse}|{r.EndVerse}|{s.GetOriginalText()}";
                });
                File.WriteAllLines(filePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving scriptures: {ex.Message}");
            }
        }
    }
}
