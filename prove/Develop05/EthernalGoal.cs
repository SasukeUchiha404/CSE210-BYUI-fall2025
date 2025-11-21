// ============================================================================
// EternalGoal Class
// ----------------------------------------------------------------------------
// Purpose: Represents a repeatable goal that is never 'finished' (e.g., daily scripture study);
// each recording event always gives points.
// ============================================================================

using System;

class EternalGoal : Goal
{
    public EternalGoal(string name, string desc, int pts)
        : base(name, desc, pts) { }

    // Recording always awards points, but never marks complete
    public override int RecordEvent(int progress = 1)
    {
        return GetPoints();
    }

    // Status always shows as 'in progress'
    public override string ShowStatus()
    {
        return "[~] " + GetName() + " -- " + GetDescription();
    }
}
