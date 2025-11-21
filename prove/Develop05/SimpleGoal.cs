// ============================================================================
// SimpleGoal Class
// ----------------------------------------------------------------------------
// Purpose: Represents a one-time goal that is completed and grants points
// in a single recording event (e.g., finish a marathon).
// ============================================================================

using System;

class SimpleGoal : Goal
{
    // Constructor calls base class to set values
    public SimpleGoal(string name, string desc, int pts)
        : base(name, desc, pts) { }

    // Records the event (marks complete & returns points once, or 0 if already done)
    public override int RecordEvent(int progress = 1)
    {
        if (!GetCompleted())
        {
            SetCompleted(true); // Mark as completed
            return GetPoints(); // Award points once
        }
        return 0; // No more points after the first completion
    }

    // Shows goal status using [ ] or [X] for incomplete/complete
    public override string ShowStatus()
    {
        return (GetCompleted() ? "[X] " : "[ ] ") + GetName() + " -- " + GetDescription();
    }
}
