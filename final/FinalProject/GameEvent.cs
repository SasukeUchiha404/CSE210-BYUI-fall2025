using System;

// Represents a simple random game event that can affect a pet.
class GameEvent
{
    private string _name;
    private string _description;

    public GameEvent(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public string GetDescription()
    {
        return _description;
    }

    public void SetDescription(string description)
    {
        _description = description;
    }

    // Applies the event effect to the pet, based on event name.
    public void TriggerEvent(Pet pet)
    {
        Console.WriteLine();
        Console.WriteLine("Game Event: " + _name);
        Console.WriteLine(_description);

        if (pet.IsDead())
        {
            Console.WriteLine(pet.GetName() + " is no longer alive and is not affected.");
            return;
        }

        if (_name == "Found Snack")
        {
            ApplyFoundSnack(pet);
        }
        else if (_name == "Surprise Vet Visit")
        {
            ApplySurpriseVetVisit(pet);
        }
        else if (_name == "Thunderstorm")
        {
            ApplyThunderstorm(pet);
        }
        else
        {
            ApplyGenericCheerUp(pet);
        }
    }

    private void ApplyFoundSnack(Pet pet)
    {
        Console.WriteLine(pet.GetName() + " found a tasty snack on the ground.");
        pet.SetHunger(pet.GetHunger() - 10);
    }

    private void ApplySurpriseVetVisit(Pet pet)
    {
        Console.WriteLine(pet.GetName() + " had a surprise visit to the vet.");
        pet.SetHealth(pet.GetHealth() + 15);

        pet.SetHappiness(pet.GetHappiness() - 5);
    }

    private void ApplyThunderstorm(Pet pet)
    {
        Console.WriteLine("A loud thunderstorm scares " + pet.GetName() + ".");
        pet.SetHappiness(pet.GetHappiness() - 10);
    }

    private void ApplyGenericCheerUp(Pet pet)
    {
        Console.WriteLine("A pleasant event brightens " + pet.GetName() + "'s day.");
        pet.SetHappiness(pet.GetHappiness() + 5);
    }
}
