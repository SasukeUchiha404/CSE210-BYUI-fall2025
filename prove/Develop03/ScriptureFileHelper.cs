using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureMemorizer
{
    // Handles reading and writing scripture data from/to scripture.txt.
    static class ScriptureFileHelper
    {
        public static List<Scripture> LoadScriptures(string filePath)
        {
            List<Scripture> scriptures = new List<Scripture>();

            try
            {
                if (!File.Exists(filePath))
                    return scriptures;

                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;

                    string[] parts = line.Split('|');
                    if (parts.Length < 5) continue;

                    string book = parts[0];
                    int chapter = int.Parse(parts[1]);
                    int startVerse = int.Parse(parts[2]);
                    int endVerse = int.Parse(parts[3]);
                    string text = parts[4];

                    Reference reference = new Reference(book, chapter, startVerse, endVerse);
                    scriptures.Add(new Scripture(reference, text));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading scriptures: {ex.Message}");
            }

            return scriptures;
        }

        public static void SaveScriptures(string filePath, List<Scripture> scriptures)
        {
            try
            {
                List<string> lines = scriptures.Select(s =>
                {
                    Reference r = s.GetReference();
                    return $"{r.GetBook()}|{r.GetChapter()}|{r.GetStartVerse()}|{r.GetEndVerse()}|{s.GetOriginalText()}";
                }).ToList();

                File.WriteAllLines(filePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving scriptures: {ex.Message}");
            }
        }
    }
}
