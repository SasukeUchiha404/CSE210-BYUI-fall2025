// ============================================================================
// NegativeGoal Class
// ----------------------------------------------------------------------------
// Purpose: Special goal that tracks negative behavior (bad habits). Recording
// an event for this goal type subtracts points from the user.
// ============================================================================

using System;

class NegativeGoal : Goal
{
    public NegativeGoal(string name, string desc, int pts)
        : base(name, desc, pts) { }

    // Each record subtracts points (makes them negative)
    public override int RecordEvent(int progress = 1)
    {
        return -GetPoints();
    }

    // Shows its distinct '[!]' status for clarity
    public override string ShowStatus()
    {
        return "[!] " + GetName() + " -- " + GetDescription();
    }
}
