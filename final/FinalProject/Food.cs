using System;

// Food item that reduces hunger.
class Food : Item
{
    public Food(string name) : base(name)
    {
    }

    public override void Use(Pet pet)
    {
        if (pet.IsDead())
        {
            Console.WriteLine(pet.GetName() + " is no longer alive.");
            return;
        }

        Console.WriteLine("Using food '" + GetName() + "' on " + pet.GetName());

        pet.SetHunger(pet.GetHunger() - 20);
        Console.WriteLine(pet.GetName() + " looks less hungry now.");
    }
}
