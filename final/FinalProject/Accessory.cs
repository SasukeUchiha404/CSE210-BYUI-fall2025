using System;

// Accessory item that customizes a pet (cosmetic effect).
class Accessory : Item
{
    public Accessory(string name) : base(name)
    {
    }

    public override void Use(Pet pet)
    {
        Console.WriteLine("Using accessory '" + GetName() + "' on " + pet.GetName());
        Console.WriteLine(pet.GetName() + " looks stylish!");
    }
}
