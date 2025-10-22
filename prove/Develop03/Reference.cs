namespace ScriptureMemorizer
{
    // Represents a scripture reference (e.g., "John 3:16" or "Proverbs 3:5–6")
    // Supports both single verses and verse ranges.
    class Reference
    {
        public string Book { get; }
        public int Chapter { get; }
        public int StartVerse { get; }
        public int EndVerse { get; }

        // Constructor for single-verse references
        public Reference(string book, int chapter, int verse)
        {
            Book = book;
            Chapter = chapter;
            StartVerse = verse;
            EndVerse = verse;
        }

        // Constructor for verse ranges
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            Book = book;
            Chapter = chapter;
            StartVerse = startVerse;
            EndVerse = endVerse;
        }

        // Returns formatted reference text (e.g., "John 3:16" or "Proverbs 3:5–6")
        public string ToDisplayString() =>
            StartVerse == EndVerse
                ? $"{Book} {Chapter}:{StartVerse}"
                : $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
    }
}
