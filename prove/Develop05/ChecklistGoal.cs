// ============================================================================
// ChecklistGoal Class
// ----------------------------------------------------------------------------
// Purpose: Represents a goal with a number of required repetitions. Each
// event gives points, bonus awarded after last one, then marks complete.
// Ex: "Go to the temple 5 times" awards points per, plus bonus at finish.
// ============================================================================

using System;

class ChecklistGoal : Goal
{
    // Private fields unique to ChecklistGoal
    private int _targetCount;   // The total number of times needed for completion
    private int _currentCount;  // The number of times this goal has been recorded so far
    private int _bonus;         // Bonus points granted on final completion

    // Constructor initializes all checklist-relevant values
    public ChecklistGoal(string name, string desc, int pts, int target, int bonus)
        : base(name, desc, pts)
    {
        _targetCount = target;
        _currentCount = 0;
        _bonus = bonus;
    }

    // Getters and setters for encapsulated access
    public int GetTargetCount() { return _targetCount; }
    public int GetCurrentCount() { return _currentCount; }
    public int GetBonus() { return _bonus; }
    public void SetCurrentCount(int value) { _currentCount = value; }

    // Records a completed instance, returns points and bonus as appropriate
    public override int RecordEvent(int progress = 1)
    {
        if (!GetCompleted())
        {
            _currentCount += progress;
            if (_currentCount >= _targetCount)
            {
                SetCompleted(true); // Finished after enough completions
                return GetPoints() + _bonus; // Final points and bonus
            }
            else
            {
                return GetPoints(); // Just a normal event
            }
        }
        return 0; // Cannot progress already-completed goal
    }

    // Status includes tracker of completions out of the total required
    public override string ShowStatus()
    {
        return (GetCompleted()
            ? "[X] "
            : $"[ ] Completed {_currentCount}/{_targetCount} ")
            + GetName() + " -- " + GetDescription();
    }
}
