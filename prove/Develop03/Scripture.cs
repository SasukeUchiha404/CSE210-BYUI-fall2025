using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    // Represents a complete scripture, including the reference and all words.
    // Handles word hiding, reset, and display logic.
    class Scripture
    {
        private readonly Reference reference; // Scripture reference (e.g., John 3:16)
        private readonly List<Word> words;    // List of words in the scripture
        private readonly Random random;       // Random generator for hiding words

        public Scripture(Reference reference, string text)
        {
            this.reference = reference;
            this.random = new Random();
            this.words = text.Split(' ')
                             .Select(word => new Word(word))
                             .ToList();
        }

        // Displays scripture reference and current visible words
        public void Display()
        {
            Console.WriteLine(reference.ToDisplayString());
            Console.WriteLine(GetVisibleText());
        }

        // Randomly hides a few words that are still visible
        public void HideRandomWords()
        {
            int wordsToHide = random.Next(2, 5); // randomly hide 2–4 words 
            var visible = words.Where(w => !w.IsHidden()).ToList();

            for (int i = 0; i < wordsToHide && visible.Count > 0; i++)
            {
                int index = random.Next(visible.Count);
                visible[index].Hide();
                visible.RemoveAt(index);
            }
        }

        // Returns true if all words are hidden
        public bool IsCompletelyHidden() =>
            words.All(w => w.IsHidden());

        // Returns the scripture text with hidden words replaced by underscores
        public string GetVisibleText() =>
            string.Join(" ", words.Select(w => w.GetDisplayText()));

        // Returns original scripture text (for saving)
        public string GetOriginalText() =>
            string.Join(" ", words.Select(w => w.GetText()));

        // Returns the scripture reference
        public Reference GetReference() => reference;

        // Resets visibility state of all words to visible
        public void Reset()
        {
            foreach (var word in words)
            {
                word.Unhide(); // make word visible again
            }
        }
    }
}
