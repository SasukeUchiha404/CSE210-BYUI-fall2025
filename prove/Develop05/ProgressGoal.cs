// ============================================================================
// ProgressGoal Class
// ----------------------------------------------------------------------------
// Purpose: Represents a goal where 'progress' accumulates to a fixed total.
// Points are proportional to progress events. Example: Running 100 miles in total.
// ============================================================================

using System;

class ProgressGoal : Goal
{
    private int _totalRequired;     // Total amount of progress to reach
    private int _currentProgress;   // Progress made so far

    public ProgressGoal(string name, string desc, int pts, int totalRequired)
        : base(name, desc, pts)
    {
        _totalRequired = totalRequired;
        _currentProgress = 0;
    }

    // Getters and setters for encapsulated access
    public int GetTotalRequired() { return _totalRequired; }
    public int GetCurrentProgress() { return _currentProgress; }
    public void SetCurrentProgress(int value) { _currentProgress = value; }

    // Each event adds to progress and grants proportional points
    public override int RecordEvent(int progress = 1)
    {
        if (_currentProgress < _totalRequired)
        {
            _currentProgress += progress;
            int earned = Math.Min(progress * GetPoints(), (_totalRequired - _currentProgress + progress) * GetPoints());

            if (_currentProgress >= _totalRequired)
                SetCompleted(true);

            return earned;
        }
        return 0;
    }

    // Status presents current progress out of final required amount
    public override string ShowStatus()
    {
        return (GetCompleted()
            ? "[X] "
            : $"[ ] Progress {_currentProgress}/{_totalRequired} ")
            + GetName() + " -- " + GetDescription();
    }
}
