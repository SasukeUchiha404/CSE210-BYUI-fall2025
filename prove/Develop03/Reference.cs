namespace ScriptureMemorizer
{
    // Represents a scripture reference (e.g. John 3:16 or Proverbs 3:5–6).
    class Reference
    {
        private string _book;
        private int _chapter;
        private int _startVerse;
        private int _endVerse;

        public Reference(string book, int chapter, int verse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = verse;
            _endVerse = verse;
        }

        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        public string GetBook() { return _book; }
        public int GetChapter() { return _chapter; }
        public int GetStartVerse() { return _startVerse; }
        public int GetEndVerse() { return _endVerse; }

        public string ToDisplayString()
        {
            if (_startVerse == _endVerse)
                return $"{_book} {_chapter}:{_startVerse}";
            return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}
