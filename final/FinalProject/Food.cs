using System;

// Food item that reduces hunger.
class Food : Item
{
    public Food(string name) : base(name)
    {
    }

    public override void Use(Pet pet)
    {
        Console.WriteLine("Using food '" + GetName() + "' on " + pet.GetName());

        int newHunger = pet.GetHunger() - 20;

        if (newHunger < 0)
        {
            newHunger = 0;
        }

        pet.SetHunger(newHunger);
        Console.WriteLine(pet.GetName() + " looks less hungry now.");
    }
}
