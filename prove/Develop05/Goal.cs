// ============================================================================
// Abstract Goal Base Class
// ----------------------------------------------------------------------------
// Purpose: Serves as the base class for all goal types in the program.
// Contains all shared fields, core behaviors, and standard access methods.
// Only fields are used for data, with explicit getter/setter methods.
// ============================================================================

using System;

abstract class Goal
{
    // Private fields to store goal details
    private string _name;          // Goal name/title
    private string _description;   // Description or summary of the goal
    private int _points;           // Points awarded each time the goal is recorded
    private bool _completed;       // True if the goal is 'finished'

    // Constructor to initialize the shared fields
    protected Goal(string name, string desc, int pts)
    {
        _name = name;
        _description = desc;
        _points = pts;
        _completed = false; // all goals start unfinished unless specified
    }

    // Getter methods for encapsulated access
    public string GetName() { return _name; }
    public string GetDescription() { return _description; }
    public int GetPoints() { return _points; }
    public bool GetCompleted() { return _completed; }
    public void SetCompleted(bool value) { _completed = value; }

    // Abstract methods for polymorphism.
    // Derived classes must implement their own logic for recording a goal event and displaying status.
    public abstract int RecordEvent(int progress = 1);
    public abstract string ShowStatus();
}
