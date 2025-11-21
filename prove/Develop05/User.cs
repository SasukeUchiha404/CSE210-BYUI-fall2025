// ============================================================================
// User Class
// ----------------------------------------------------------------------------
// Purpose: Manages a list of goals and all user stats (score, XP, level).
// Handles all menu-driven interactions, file I/O for saving/loading, and
// provides getter methods for encapsulated fields. Now asks for negative or
// progress goal type before standard goal creation.
// Author: Christian Chan
// Date: Nov 19, 2025
// ============================================================================

using System;
using System.Collections.Generic;
using System.IO;

class User
{
    // Private fields for encapsulating user data
    private List<Goal> _goals = new List<Goal>(); // User's list of all goal objects
    private int _score;       // Total points earned (grows from all positive/negative goals)
    private int _experience;  // Experience earned, used for leveling
    private int _level;       // User's level, determined by XP

    // Constructor: Start at level 1 with zero score and XP
    public User()
    {
        _score = 0;
        _experience = 0;
        _level = 1;
    }

    // Getter methods for encapsulated access
    public int GetScore() { return _score; }
    public int GetExperience() { return _experience; }
    public int GetLevel() { return _level; }
    public List<Goal> GetGoals() { return _goals; }

    // Adds a new goal to the list
    public void AddGoal(Goal g) { _goals.Add(g); }

    // Adds experience points and checks if the user should level up
    public void AddExperience(int points)
    {
        _experience += points;
        CheckLevelUp();
    }

    // Level up check (every 1000*level XP, advance a level)
    public void CheckLevelUp()
    {
        while (_experience >= _level * 1000)
        {
            _experience -= _level * 1000;
            _level++;
            Console.WriteLine($"Congratulations! Level up to {GetLevel()}!");
            Console.ReadLine();
        }
    }

    // Shows all user's goals and their statuses on the screen
    public void ShowGoals()
    {
        Console.Clear();
        Console.WriteLine("=== Your Goals ===");
        if (_goals.Count == 0)
            Console.WriteLine("No goals yet.");
        else
            for (int i = 0; i < _goals.Count; i++)
                Console.WriteLine((i + 1) + ". " + _goals[i].ShowStatus());
        Console.WriteLine("\nPress Enter to return to menu.");
        Console.ReadLine();
    }

    // Interactive method to let user choose and create a goal
    // Prompts first for negative/progress, then standard types
    public void AddGoalInteractively()
    {
        Console.Clear();
        Console.WriteLine("Create a New Goal!");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Goal Types (standard):");
        Console.WriteLine("  1. Simple");
        Console.WriteLine("  2. Eternal");
        Console.WriteLine("  3. Checklist");
        Console.WriteLine();
        Console.WriteLine("You can also create a Negative goal (subtracts points) or Progress goal (tracks progress toward a total).");

        // Prompt if this goal should be Negative type
        Console.Write("Is this goal a negative goal (subtracts points for bad habits)? (y/n): ");
        string isNegative = Console.ReadLine()?.Trim().ToLower();

        if (isNegative == "y" || isNegative == "yes")
        {
            // Gather all required fields for a NegativeGoal and add it
            Console.Write("Goal name: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string desc = Console.ReadLine();
            Console.Write("Negative points (amount to subtract each time): ");
            int pts = SafeIntInput();
            AddGoal(new NegativeGoal(name, desc, pts));
            Console.WriteLine("Negative goal added! Press Enter.");
            Console.ReadLine();
            return; // Done, return to main menu
        }

        // Prompt if this goal should be a Progress goal
        Console.Write("Is this goal a progress goal (tracks incremental progress to a target)? (y/n): ");
        string isProgress = Console.ReadLine()?.Trim().ToLower();

        if (isProgress == "y" || isProgress == "yes")
        {
            // Gather all required fields for a ProgressGoal and add it
            Console.Write("Goal name: ");
            string name = Console.ReadLine();
            Console.Write("Description: ");
            string desc = Console.ReadLine();
            Console.Write("Points per unit progress: ");
            int pts = SafeIntInput();
            Console.Write("Total required progress amount: ");
            int totalReq = SafeIntInput();
            AddGoal(new ProgressGoal(name, desc, pts, totalReq));
            Console.WriteLine("Progress goal added! Press Enter.");
            Console.ReadLine();
            return; // Done, return to main menu
        }

        // Otherwise, let user choose from standard goal types
        Console.Write("Select goal type (1=Simple, 2=Eternal, 3=Checklist): ");
        string choice = Console.ReadLine()?.Trim();

        // Read common fields for all standard goals
        Console.Write("Goal name: ");
        string nameBase = Console.ReadLine();
        Console.Write("Description: ");
        string descBase = Console.ReadLine();
        Console.Write("Points: ");
        int ptsBase = SafeIntInput();

        // Make the correct goal object based on user's standard selection
        Goal newGoal = null;
        switch (choice)
        {
            case "1": // Simple
                newGoal = new SimpleGoal(nameBase, descBase, ptsBase);
                break;
            case "2": // Eternal
                newGoal = new EternalGoal(nameBase, descBase, ptsBase);
                break;
            case "3": // Checklist
                Console.Write("Target count (times to complete): ");
                int target = SafeIntInput();
                Console.Write("Bonus points on final completion: ");
                int bonus = SafeIntInput();
                newGoal = new ChecklistGoal(nameBase, descBase, ptsBase, target, bonus);
                break;
            default:
                Console.WriteLine("Invalid choice. Press Enter.");
                Console.ReadLine();
                return;
        }

        // Add new goal and confirm to user
        AddGoal(newGoal);
        Console.WriteLine("Goal added! Press Enter.");
        Console.ReadLine();
    }

    // Lets the user select a goal and record an event
    public void RecordGoalInteractively()
    {
        Console.Clear();
        Console.WriteLine("Choose which goal to record an event for:");
        for (int i = 0; i < _goals.Count; i++)
            Console.WriteLine($"{i + 1}. {_goals[i].ShowStatus()}");
        Console.Write("Enter goal number: ");
        int selected = SafeIntInput(1, _goals.Count);
        if (selected < 1 || selected > _goals.Count)
            return;

        Goal chosen = _goals[selected - 1];
        int progress = 1;
        if (chosen is ProgressGoal)
        {
            Console.Write("Enter amount of progress: ");
            progress = SafeIntInput();
        }

        int pointsEarned = chosen.RecordEvent(progress);
        _score += pointsEarned;
        AddExperience(Math.Max(pointsEarned, 0)); // Don't give XP for negative goal

        string verb = pointsEarned < 0 ? "Lost" : "Earned";
        Console.WriteLine($"{verb} {Math.Abs(pointsEarned)} points.");
        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
    }

    // Prompts for a filename and saves progress (score, XP, goals) to disk as plain text
    public void SaveProgress()
    {
        Console.Clear();
        Console.Write("Enter filename to save (default: goals.txt): ");
        string fn = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fn)) fn = "goals.txt";

        try
        {
            using (StreamWriter sw = new StreamWriter(fn))
            {
                // Write meta and then each goal in a serializable line format
                sw.WriteLine(_score);
                sw.WriteLine(_experience);
                sw.WriteLine(_level);
                foreach (Goal g in _goals)
                    sw.WriteLine(SerializeGoal(g));
            }
            Console.WriteLine("Progress saved! Press Enter.");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving: {ex.Message} Press Enter.");
            Console.ReadLine();
        }
    }

    // Prompts for a filename and loads progress back from disk
    public void LoadProgress()
    {
        Console.Clear();
        Console.Write("Enter filename to load (default: goals.txt): ");
        string fn = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(fn)) fn = "goals.txt";
        try
        {
            using (StreamReader sr = new StreamReader(fn))
            {
                // Read meta and reconstruct the goal list from file lines
                _score = int.Parse(sr.ReadLine());
                _experience = int.Parse(sr.ReadLine());
                _level = int.Parse(sr.ReadLine());
                _goals.Clear();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Goal g = DeserializeGoal(line);
                    if (g != null)
                        _goals.Add(g);
                }
            }
            Console.WriteLine("Progress loaded! Press Enter.");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading: {ex.Message} Press Enter.");
            Console.ReadLine();
        }
    }

    // Serializes each goal to a line (type + fields) for file persistency
    private string SerializeGoal(Goal g)
    {
        if (g is SimpleGoal)
            return $"Simple|{g.GetName()}|{g.GetDescription()}|{g.GetPoints()}|{g.GetCompleted()}";
        if (g is EternalGoal)
            return $"Eternal|{g.GetName()}|{g.GetDescription()}|{g.GetPoints()}";
        if (g is ChecklistGoal chg)
            return $"Checklist|{chg.GetName()}|{chg.GetDescription()}|{chg.GetPoints()}|{chg.GetTargetCount()}|{chg.GetCurrentCount()}|{chg.GetBonus()}|{chg.GetCompleted()}";
        if (g is ProgressGoal pg)
            return $"Progress|{pg.GetName()}|{pg.GetDescription()}|{pg.GetPoints()}|{pg.GetTotalRequired()}|{pg.GetCurrentProgress()}|{pg.GetCompleted()}";
        if (g is NegativeGoal)
            return $"Negative|{g.GetName()}|{g.GetDescription()}|{g.GetPoints()}";
        return "";
    }

    // Reconstructs each goal from a line in the saved file
    private Goal DeserializeGoal(string line)
    {
        var parts = line.Split('|');
        if (parts.Length < 4) return null;
        string type = parts[0];
        string name = parts[1];
        string desc = parts[2];
        int pts = int.Parse(parts[3]);
        switch (type)
        {
            case "Simple":
                var sg = new SimpleGoal(name, desc, pts);
                if (parts.Length > 4 && bool.TryParse(parts[4], out bool completedS))
                    sg.SetCompleted(completedS);
                return sg;
            case "Eternal":
                return new EternalGoal(name, desc, pts);
            case "Checklist":
                int target = parts.Length > 4 ? int.Parse(parts[4]) : 1;
                int current = parts.Length > 5 ? int.Parse(parts[5]) : 0;
                int bonus = parts.Length > 6 ? int.Parse(parts[6]) : 0;
                var chg = new ChecklistGoal(name, desc, pts, target, bonus);
                chg.SetCurrentCount(current);
                if (parts.Length > 7 && bool.TryParse(parts[7], out bool completedC))
                    chg.SetCompleted(completedC);
                return chg;
            case "Progress":
                int totalReq = parts.Length > 4 ? int.Parse(parts[4]) : 1;
                int prog = parts.Length > 5 ? int.Parse(parts[5]) : 0;
                var pg = new ProgressGoal(name, desc, pts, totalReq);
                pg.SetCurrentProgress(prog);
                if (parts.Length > 6 && bool.TryParse(parts[6], out bool completedP))
                    pg.SetCompleted(completedP);
                return pg;
            case "Negative":
                return new NegativeGoal(name, desc, pts);
            default:
                return null;
        }
    }

    // Reads and checks for an integer input, optionally enforcing min/max constraints
    private int SafeIntInput(int min = 0, int max = int.MaxValue)
    {
        while (true)
        {
            string inp = Console.ReadLine();
            if (int.TryParse(inp, out int val))
                if (val >= min && val <= max)
                    return val;
            Console.WriteLine($"Invalid input. Enter a whole number between {min} and {max}: ");
        }
    }
}
