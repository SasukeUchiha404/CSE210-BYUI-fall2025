using System;
using System.Collections.Generic;

// Represents the player who owns and manages pets.
class User
{
    private string _name;
    private List<Pet> _pets;

    public User(string name)
    {
        _name = name;
        _pets = new List<Pet>();
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    // Adds a pet to the user's collection.
    public void AddPet(Pet pet)
    {
        _pets.Add(pet);
    }

    // Returns the list of pets for display and interaction.
    public List<Pet> GetPets()
    {
        return _pets;
    }

    // Displays all pets owned by the user.
    public void ShowPets()
    {
        Console.WriteLine();
        Console.WriteLine("Pets owned by " + _name + ":");

        if (_pets.Count == 0)
        {
            Console.WriteLine("No pets yet.");
            return;
        }

        for (int i = 0; i < _pets.Count; i++)
        {
            Pet pet = _pets[i];
            Console.WriteLine((i + 1) + ". " + pet.GetName() + " (" + pet.GetType().Name + ")");
        }
    }
}
