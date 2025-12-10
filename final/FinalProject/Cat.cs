using System;

// Represents a cat pet with cat-specific behavior.
class Cat : Pet
{
    public Cat(string name, int age) : base(name, age)
    {
    }

    public override void MakeSound()
    {
        Console.WriteLine(GetName() + " says: Meow!");
    }

    public override void Play()
    {
        if (IsDead())
        {
            Console.WriteLine(GetName() + " is no longer alive.");
            return;
        }

        Console.WriteLine(GetName() + " is chasing a laser dot.");
        base.Play();
    }
}
