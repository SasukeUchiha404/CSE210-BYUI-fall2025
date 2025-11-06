using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorizer
{
    // Manages a scripture verse or passage text for memorization.
    class Scripture
    {
        private Reference _reference;
        private List<Word> _words;
        private Random _random;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _random = new Random();
            _words = text.Split(' ').Select(w => new Word(w)).ToList();
        }

        public Reference GetReference() { return _reference; }

        public void Display()
        {
            Console.WriteLine(_reference.ToDisplayString());
            Console.WriteLine(GetVisibleText());
        }

        public void HideRandomWords()
        {
            int toHide = _random.Next(2, 5);
            List<Word> visible = _words.Where(w => !w.IsHidden()).ToList();

            for (int i = 0; i < toHide && visible.Count > 0; i++)
            {
                int index = _random.Next(visible.Count);
                visible[index].Hide();
                visible.RemoveAt(index);
            }
        }

        public bool IsCompletelyHidden() { return _words.All(w => w.IsHidden()); }

        public string GetVisibleText() { return string.Join(" ", _words.Select(w => w.GetDisplayText())); }

        public string GetOriginalText() { return string.Join(" ", _words.Select(w => w.GetText())); }

        public void Reset()
        {
            foreach (Word word in _words)
                word.Unhide();
        }
    }
}
