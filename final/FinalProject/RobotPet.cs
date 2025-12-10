using System;

// Represents a robotic pet with special actions.
class RobotPet : Pet
{
    public RobotPet(string name, int age) : base(name, age)
    {
    }

    public override void MakeSound()
    {
        Console.WriteLine(GetName() + " beeps electronically.");
    }

    // Robot-specific behavior for restoring health.
    public void Recharge()
    {
        if (IsDead())
        {
            Console.WriteLine(GetName() + " is no longer functioning.");
            return;
        }

        Console.WriteLine(GetName() + " is recharging its battery.");
        SetHealth(GetHealth() + 20);
    }

    public override void Sleep()
    {
        if (IsDead())
        {
            Console.WriteLine(GetName() + " is no longer functioning.");
            return;
        }

        Console.WriteLine(GetName() + " powers down instead of sleeping.");
        Recharge();
    }
}
