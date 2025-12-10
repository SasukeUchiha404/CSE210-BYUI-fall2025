using System;

// Represents a simple mini-game played with a pet.
class MiniGame
{
    private string _name;
    private string _lastResult;

    public MiniGame(string name)
    {
        _name = name;
        _lastResult = "";
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public string GetLastResult()
    {
        return _lastResult;
    }

    public void SetLastResult(string result)
    {
        _lastResult = result;
    }

    // Starts a mini-game by name and updates the pet's stats.
    public void StartGame(Pet pet)
    {
        if (pet.IsDead())
        {
            Console.WriteLine(pet.GetName() + " is no longer alive and cannot play.");
            return;
        }

        Console.Clear();  // Clear before mini-game screen

        Console.WriteLine("Starting mini-game '" + _name + "' with " + pet.GetName() + "...");
        Console.WriteLine();

        if (_name == "Fetch Challenge")
        {
            PlayFetchChallenge(pet);
        }
        else if (_name == "Obstacle Course")
        {
            PlayObstacleCourse(pet);
        }
        else if (_name == "Guess The Number")
        {
            PlayGuessTheNumber(pet);
        }
        else
        {
            PlaySimpleGame(pet);
        }

        Console.WriteLine();
        Console.WriteLine(_lastResult);
    }

    private void PlayFetchChallenge(Pet pet)
    {
        Console.WriteLine(pet.GetName() + " is chasing a ball...");
        Random random = new Random();
        int roll = random.Next(0, 2);

        if (roll == 0)
        {
            _lastResult = pet.GetName() + " ran fast but missed the catch.";
            pet.SetHappiness(pet.GetHappiness() + 5);
        }
        else
        {
            _lastResult = pet.GetName() + " caught the ball perfectly!";
            pet.SetHappiness(pet.GetHappiness() + 10);
        }
    }

    private void PlayObstacleCourse(Pet pet)
    {
        Console.WriteLine(pet.GetName() + " is running an obstacle course...");
        Random random = new Random();
        int roll = random.Next(0, 3);

        if (roll == 0)
        {
            _lastResult = pet.GetName() + " tripped over one of the obstacles and got a bit hurt.";
            pet.SetHealth(pet.GetHealth() - 10);   // lose health on failure
        }
        else
        {
            _lastResult = pet.GetName() + " finished the obstacle course!";
            pet.SetHealth(pet.GetHealth() + 10);   // gain health on success
        }
    }

    private void PlayGuessTheNumber(Pet pet)
    {
        Console.WriteLine("Guess a number between 1 and 3 to cheer up " + pet.GetName() + ": ");
        Console.Write("Your guess: ");
        string input = Console.ReadLine();

        int guess;
        Random random = new Random();
        int secret = random.Next(1, 4);

        if (int.TryParse(input, out guess) && guess == secret)
        {
            _lastResult = "Correct! " + pet.GetName() + " is impressed.";
            pet.SetHappiness(pet.GetHappiness() + 15);
        }
        else
        {
            _lastResult = "Not quite. The correct number was " + secret + ". " +
                          pet.GetName() + " got a little stressed.";
            pet.SetHappiness(pet.GetHappiness() + 5);
            pet.SetHealth(pet.GetHealth() - 5);    // small health loss
        }
    }

    private void PlaySimpleGame(Pet pet)
    {
        Console.WriteLine(pet.GetName() + " is playing a simple game.");
        pet.SetHappiness(pet.GetHappiness() + 5);
        _lastResult = pet.GetName() + " seems a little happier after playing.";
    }
}
