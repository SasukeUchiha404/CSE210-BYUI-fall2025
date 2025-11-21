// ============================================================================
// ProgressGoal Class
// ----------------------------------------------------------------------------
// Purpose: Represents a goal where 'progress' accumulates to a fixed total.
// Points are proportional to progress events. Example: Running 100 miles in total.
// Fix: Progress cannot exceed total required, and points are capped accordingly.
// Author: Christian Chan
// Date: Nov 20, 2025
// ============================================================================

using System;

class ProgressGoal : Goal
{
    // Total amount of progress required to complete the goal
    private int _totalRequired;
    // The user's current progress toward that total (never exceeds _totalRequired)
    private int _currentProgress;

    // Constructor initializes the fields using the parent Goal constructor
    public ProgressGoal(string name, string desc, int pts, int totalRequired)
        : base(name, desc, pts)
    {
        _totalRequired = totalRequired;
        _currentProgress = 0;
    }

    // Getter for total progress required
    public int GetTotalRequired() { return _totalRequired; }
    // Getter for current progress
    public int GetCurrentProgress() { return _currentProgress; }
    // Setter for current progress (rarely used)
    public void SetCurrentProgress(int value) { _currentProgress = value; }

    // Each event adds to progress and grants proportional points,
    // but will NEVER let progress or points exceed the total target
    public override int RecordEvent(int progress = 1)
    {
        // Calculate how much progress remains
        int remaining = _totalRequired - _currentProgress;

        // Only award as much progress as is still needed
        int toAdd = Math.Min(progress, remaining);

        // Add capped progress
        _currentProgress += toAdd;

        // Mark as complete if reached the required amount
        if (_currentProgress >= _totalRequired)
        {
            SetCompleted(true);
        }

        // Points earned reflect only what was actually added
        int earned = toAdd * GetPoints();

        return earned;
    }

    // Status presents capped current progress out of the final required amount
    public override string ShowStatus()
    {
        // Shows e.g. [ ] Progress 10/100 GoalName -- Description
        return (GetCompleted()
            ? "[X] "
            : $"[ ] Progress {_currentProgress}/{_totalRequired} ")
            + GetName() + " -- " + GetDescription();
    }
}
