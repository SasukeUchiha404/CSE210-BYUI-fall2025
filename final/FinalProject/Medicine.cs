using System;

// Medicine item that heals the pet.
class Medicine : Item
{
    public Medicine(string name) : base(name)
    {
    }

    public override void Use(Pet pet)
    {
        Console.WriteLine("Using medicine '" + GetName() + "' on " + pet.GetName());

        int newHealth = pet.GetHealth() + 20;

        if (newHealth > 100)
        {
            newHealth = 100;
        }

        pet.SetHealth(newHealth);
        Console.WriteLine(pet.GetName() + " looks healthier!");
    }
}
