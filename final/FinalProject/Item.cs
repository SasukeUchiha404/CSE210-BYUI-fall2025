using System;

// Abstract base class for all items that can be used on pets.
abstract class Item
{
    private string _name;

    public Item(string name)
    {
        _name = name;
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    // Uses this item on a pet.
    public abstract void Use(Pet pet);
}
