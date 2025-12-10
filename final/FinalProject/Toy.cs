using System;

// Toy item that increases happiness.
class Toy : Item
{
    public Toy(string name) : base(name)
    {
    }

    public override void Use(Pet pet)
    {
        Console.WriteLine("Using toy '" + GetName() + "' with " + pet.GetName());

        int newHappiness = pet.GetHappiness() + 20;

        if (newHappiness > 100)
        {
            newHappiness = 100;
        }

        pet.SetHappiness(newHappiness);
        Console.WriteLine(pet.GetName() + " looks happier!");
    }
}
