using System;

// Medicine item that heals the pet and can revive it.
class Medicine : Item
{
    public Medicine(string name) : base(name)
    {
    }

    public override void Use(Pet pet)
    {
        Console.WriteLine("Attempting to use medicine '" + GetName() + "' on " + pet.GetName());

        // Prevent using medicine twice in a row on the same pet.
        if (pet.GetLastActionWasMedicine())
        {
            Console.WriteLine("You cannot use medicine on " + pet.GetName() + " twice in a row.");
            return;
        }

        // If the pet is dead, try to revive (max 3 times).
        if (pet.IsDead())
        {
            if (pet.GetReviveCount() >= 3)
            {
                Console.WriteLine(pet.GetName() + " cannot be revived anymore (maximum revives reached).");
                return;
            }

            Console.WriteLine(pet.GetName() + " is being revived with medicine!");
            pet.SetHealth(20);           // SetHealth will also clear the dead flag.
            pet.IncrementReviveCount();  // Track revive use.
        }
        else
        {
            // If the pet is alive, just heal 20 health.
            pet.SetHealth(pet.GetHealth() + 20);
            Console.WriteLine(pet.GetName() + " looks healthier!");
        }

        // Mark that the last action on this pet was medicine.
        pet.SetLastActionWasMedicine(true);
    }
}
