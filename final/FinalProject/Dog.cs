using System;

// Represents a dog pet with dog-specific behavior.
class Dog : Pet
{
    public Dog(string name, int age) : base(name, age)
    {
    }

    // Polymorphic sound implementation for dogs.
    public override void MakeSound()
    {
        Console.WriteLine(GetName() + " says: Woof!");
    }

    public override void Play()
    {
        Console.WriteLine(GetName() + " is fetching a ball.");
        base.Play();
    }
}
