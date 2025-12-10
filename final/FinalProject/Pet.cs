using System;
using System.Collections.Generic;

// Abstract base class for all pets.
abstract class Pet
{
    private string _name;
    private int _age;
    private int _hunger;
    private int _happiness;
    private int _health;
    private bool _isDead;

    // Track how many times this pet has been revived.
    private int _reviveCount;

    // Track whether the last action on this pet was medicine.
    private bool _lastActionWasMedicine;

    // Each pet has a list of items (accessories, toys, etc.).
    private List<Item> _items;

    public Pet(string name, int age)
    {
        _name = name;
        _age = age;
        _hunger = 50;
        _happiness = 50;
        _health = 50;
        _items = new List<Item>();
        _isDead = false;
        _reviveCount = 0;
        _lastActionWasMedicine = false;
    }

    public string GetName()
    {
        return _name;
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public int GetAge()
    {
        return _age;
    }

    public void SetAge(int age)
    {
        _age = age;
    }

    public int GetHunger()
    {
        return _hunger;
    }

    public void SetHunger(int value)
    {
        _hunger = value;

        if (_hunger < 0)
        {
            _hunger = 0;
        }

        if (_hunger > 100)
        {
            _hunger = 100;
        }
    }

    public int GetHappiness()
    {
        return _happiness;
    }

    public void SetHappiness(int value)
    {
        _happiness = value;

        if (_happiness < 0)
        {
            _happiness = 0;
        }

        if (_happiness > 100)
        {
            _happiness = 100;
        }
    }

    public int GetHealth()
    {
        return _health;
    }

    public void SetHealth(int value)
    {
        _health = value;

        if (_health <= 0)
        {
            _health = 0;
            MarkDead();
        }
        else
        {
            if (_health > 100)
            {
                _health = 100;
            }

            // Any positive health means the pet is alive again.
            if (_isDead)
            {
                _isDead = false;
                Console.WriteLine(_name + " has been revived!");
            }
        }
    }

    public bool IsDead()
    {
        return _isDead;
    }

    private void MarkDead()
    {
        if (!_isDead)
        {
            _isDead = true;
            Console.WriteLine(_name + " has died...");
        }
    }

    public int GetReviveCount()
    {
        return _reviveCount;
    }

    public void IncrementReviveCount()
    {
        _reviveCount = _reviveCount + 1;
    }

    public bool GetLastActionWasMedicine()
    {
        return _lastActionWasMedicine;
    }

    public void SetLastActionWasMedicine(bool value)
    {
        _lastActionWasMedicine = value;
    }

    // Adds an item (e.g., accessory or toy) to this pet.
    public void AddItem(Item item)
    {
        _items.Add(item);
        Console.WriteLine(GetName() + " received item: " + item.GetName());
    }

    // Returns the current list of items.
    public List<Item> GetItems()
    {
        return _items;
    }

    // Displays a simple list of items this pet owns.
    public void ShowItems()
    {
        Console.WriteLine();
        Console.WriteLine("Items for " + _name + ":");

        if (_items.Count == 0)
        {
            Console.WriteLine("No items.");
            return;
        }

        for (int i = 0; i < _items.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + _items[i].GetName() + " (" + _items[i].GetType().Name + ")");
        }
    }

    // Polymorphic sound method: each derived pet overrides this.
    public abstract void MakeSound();

    // Feeds the pet and reduces hunger.
    public virtual void Feed()
    {
        if (_isDead)
        {
            Console.WriteLine(_name + " is no longer alive.");
            return;
        }

        // Any non-medicine action clears the medicine flag.
        _lastActionWasMedicine = false;

        Console.WriteLine(_name + " is eating.");
        SetHunger(_hunger - 10);
    }

    // Plays with the pet and increases happiness.
    public virtual void Play()
    {
        if (_isDead)
        {
            Console.WriteLine(_name + " is no longer alive.");
            return;
        }

        _lastActionWasMedicine = false;

        Console.WriteLine(_name + " is playing.");
        SetHappiness(_happiness + 10);
    }

    // Lets the pet sleep and restores health.
    public virtual void Sleep()
    {
        if (_isDead)
        {
            Console.WriteLine(_name + " is no longer alive.");
            return;
        }

        _lastActionWasMedicine = false;

        Console.WriteLine(_name + " is sleeping.");
        SetHealth(_health + 10);
    }

    // Displays the pet's current status, including item count.
    public virtual void ShowStatus()
    {
        Console.WriteLine();
        Console.WriteLine("Status of " + _name + ":");
        Console.WriteLine("Age: " + _age);
        Console.WriteLine("Hunger: " + _hunger);
        Console.WriteLine("Happiness: " + _happiness);
        Console.WriteLine("Health: " + _health);
        Console.WriteLine("Number of Items: " + _items.Count);
        Console.WriteLine("Revives Used: " + _reviveCount + "/3");
        Console.WriteLine("Is Dead: " + (_isDead ? "Yes" : "No"));
    }
}
