using System;

// Represents a bird pet with bird-specific behavior.
class Bird : Pet
{
    public Bird(string name, int age) : base(name, age)
    {
    }

    public override void MakeSound()
    {
        Console.WriteLine(GetName() + " chirps a cheerful song.");
    }

    public override void Play()
    {
        Console.WriteLine(GetName() + " is flying around happily.");
        base.Play();
    }
}
