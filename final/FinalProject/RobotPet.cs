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

    public void Recharge()
    {
        Console.WriteLine(GetName() + " is recharging its battery.");
        int newHealth = GetHealth() + 20;

        if (newHealth > 100)
        {
            newHealth = 100;
        }

        SetHealth(newHealth);
    }

    public override void Sleep()
    {
        Console.WriteLine(GetName() + " powers down instead of sleeping.");
        Recharge();
    }
}
