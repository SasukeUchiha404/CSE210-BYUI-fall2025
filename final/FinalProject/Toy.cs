using System;

// Toy item that increases happiness.
class Toy : Item
{
    public Toy(string name) : base(name)
    {
    }

    public override void Use(Pet pet)
    {
        if (pet.IsDead())
        {
            Console.WriteLine(pet.GetName() + " is no longer alive.");
            return;
        }

        Console.WriteLine("Using toy '" + GetName() + "' with " + pet.GetName());

        pet.SetHappiness(pet.GetHappiness() + 20);
        Console.WriteLine(pet.GetName() + " looks happier!");
    }
}
