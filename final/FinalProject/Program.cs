// ============================================================================
// Virtual Pet Simulator Console Application
// ----------------------------------------------------------------------------
// Description:
//    A menu-driven virtual pet simulator where users adopt and care for pets.
//    - Supports multiple pet types (Dog, Cat, Bird, RobotPet) via inheritance.
//    - Uses polymorphic behaviors such as MakeSound() on the Pet base class.
//    - Allows basic interactions (feed, play, sleep, status) through a console UI.
//    - Pets can own Items (toys, accessories, etc.) and use them.
//    - Includes simple MiniGames and GameEvents that affect pet stats.
//    - Pets can be added at runtime; health can drop to 0 and pets can die.
//    - Pets that die can be revived up to 3 times with Medicine.
// 
// Updated: December 11, 2025
// Standards:
//    - PascalCase for class and method names
//    - _camelCase for private fields
//    - Explicit getter/setter methods (no properties or lambdas)
//    - One class per .cs file with matching filename
//    - Consistent formatting, whitespace, and descriptive comments
// ============================================================================

using System;
using System.Collections.Generic;

class Program
{
    // Counts how many pet actions have happened since the last time tick.
    private static int _actionsSinceLastTick = 0;

    // Entry point for the application.
    static void Main(string[] args)
    {
        // Create a user.
        User user = new User("Player One");

        // Create several different pets using the Pet base type.
        Pet dog = new Dog("Buddy", 2);
        Pet cat = new Cat("Whiskers", 3);
        Pet bird = new Bird("Tweety", 1);
        Pet robot = new RobotPet("Robo", 1);

        // Give each pet some starter items to demonstrate composition.
        Toy ball = new Toy("Rubber Ball");
        Toy mouse = new Toy("Toy Mouse");
        Food seeds = new Food("Bird Seeds");
        Accessory collar = new Accessory("Shiny Collar");
        Accessory leash = new Accessory("Leather Leash");  // NEW
        Toy frisbee = new Toy("Flying Disc");              // NEW

        dog.AddItem(ball);
        dog.AddItem(leash);
        cat.AddItem(mouse);
        bird.AddItem(seeds);
        robot.AddItem(collar);
        dog.AddItem(frisbee);  // Give dog an extra toy

        // Add pets to the user.
        user.AddPet(dog);
        user.AddPet(cat);
        user.AddPet(bird);
        user.AddPet(robot);

        bool running = true;

        while (running)
        {
            Console.Clear();  // Clear before main menu

            Console.WriteLine();
            Console.WriteLine("=== Virtual Pet Simulator ===");
            Console.WriteLine("1. List Pets");
            Console.WriteLine("2. Interact With Pet");
            Console.WriteLine("3. Add New Pet");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();

            if (input == "1")
            {
                Console.Clear();  // Clear before listing pets
                user.ShowPets();
                Console.WriteLine();
                Console.WriteLine("Press Enter to return to the menu...");
                Console.ReadLine();
            }
            else if (input == "2")
            {
                InteractWithPet(user);
            }
            else if (input == "3")
            {
                Console.Clear();  // Clear before add-pet flow
                AddNewPet(user);
                Console.WriteLine();
                Console.WriteLine("Press Enter to return to the menu...");
                Console.ReadLine();
            }
            else if (input == "4")
            {
                running = false;
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }

        Console.WriteLine("Thank you for playing the Virtual Pet Simulator.");
    }

    // Lets the user create and adopt a new pet.
    static void AddNewPet(User user)
    {
        if (user.GetPets().Count >= 8)
        {
            Console.Clear();
            Console.WriteLine("You have reached the maximum of 8 pets!");
            Console.WriteLine("Release a pet first, then try adding again.");
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            return;
        }

        Console.Clear();
        Console.WriteLine("Add a New Pet (" + user.GetPets().Count + "/8)");
        Console.WriteLine("Choose a type:");
        Console.WriteLine("1. Dog");
        Console.WriteLine("2. Cat");
        Console.WriteLine("3. Bird");
        Console.WriteLine("4. RobotPet");
        Console.Write("Type: ");
        string typeChoice = Console.ReadLine();

        Console.Write("Enter a name for your pet: ");
        string name = Console.ReadLine();

        Console.Write("Enter an age (number): ");
        string ageInput = Console.ReadLine();
        int age;
        if (!int.TryParse(ageInput, out age))
        {
            age = 1;
        }

        Pet newPet;

        if (typeChoice == "1")
        {
            newPet = new Dog(name, age);
        }
        else if (typeChoice == "2")
        {
            newPet = new Cat(name, age);
        }
        else if (typeChoice == "3")
        {
            newPet = new Bird(name, age);
        }
        else if (typeChoice == "4")
        {
            newPet = new RobotPet(name, age);
        }
        else
        {
            Console.WriteLine("Invalid type. Pet was not created.");
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            return;
        }

        user.AddPet(newPet);
        Console.WriteLine("Pet " + newPet.GetName() + " has been adopted! (" + user.GetPets().Count + "/8)");
        Console.WriteLine("Press Enter to return to the menu...");
        Console.ReadLine();
    }

    // Lets the user select and remove ("release") a pet from their collection.
    static void ReleasePet(User user)
    {
        Console.Clear();
        Console.WriteLine("Release a Pet (" + user.GetPets().Count + "/8)");

        List<Pet> pets = user.GetPets();

        if (pets.Count == 0)
        {
            Console.WriteLine("You have no pets to release.");
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Select a pet to release:");

        for (int i = 0; i < pets.Count; i++)
        {
            string status = pets[i].IsDead() ? " [DEAD]" : "";
            Console.WriteLine((i + 1) + ". " + pets[i].GetName() + " (" + pets[i].GetType().Name + ")" + status);
        }

        Console.Write("Choice: ");
        string choice = Console.ReadLine();

        int index;
        if (!int.TryParse(choice, out index))
        {
            Console.WriteLine("Invalid number.");
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            return;
        }

        index = index - 1;

        if (index < 0 || index >= pets.Count)
        {
            Console.WriteLine("Invalid pet selection.");
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            return;
        }

        Pet petToRelease = pets[index];
        Console.Clear();
        Console.WriteLine("Releasing " + petToRelease.GetName() + "...");
        Console.WriteLine(petToRelease.GetName() + " has been given a good home.");
        Console.WriteLine("Press Enter to return to the menu...");

        // Remove the pet from the user's collection (last item first to avoid index shifting)
        user.RemovePet(index);
        Console.ReadLine();
    }

    // Displays a submenu to interact with a selected pet.
    static void InteractWithPet(User user)
    {
        Console.Clear();  // Clear before pet selection

        Console.WriteLine("Select a pet by number:");

        List<Pet> pets = user.GetPets();

        if (pets.Count == 0)
        {
            Console.WriteLine("You do not have any pets yet.");
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            return;
        }

        for (int i = 0; i < pets.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + pets[i].GetName() + " (" + pets[i].GetType().Name + ")");
        }

        Console.Write("Choice: ");
        string petChoice = Console.ReadLine();

        int index;
        if (!int.TryParse(petChoice, out index))
        {
            Console.WriteLine("Invalid number.");
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            return;
        }

        index = index - 1;

        if (index < 0 || index >= pets.Count)
        {
            Console.WriteLine("Invalid pet selection.");
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
            return;
        }

        Pet selectedPet = pets[index];

        Console.Clear();  // Clear before pet action menu

        Console.WriteLine("Interacting with " + selectedPet.GetName());
        Console.WriteLine("1. Feed");
        Console.WriteLine("2. Play");
        Console.WriteLine("3. Sleep");
        Console.WriteLine("4. Show Status");
        Console.WriteLine("5. Make Sound");
        Console.WriteLine("6. Show Items");
        Console.WriteLine("7. Use Item");
        Console.WriteLine("8. Play Mini-Game");
        Console.WriteLine("9. Give Medicine");
        Console.WriteLine("10. Give Accessory");  
        Console.WriteLine("11. Give Toy");         
        Console.Write("Choose an action: ");

        string action = Console.ReadLine();
        bool validAction = true;

        if (action == "1")
        {
            selectedPet.Feed();
        }
        else if (action == "2")
        {
            selectedPet.Play();
        }
        else if (action == "3")
        {
            selectedPet.Sleep();
        }
        else if (action == "4")
        {
            selectedPet.ShowStatus();
        }
        else if (action == "5")
        {
            selectedPet.MakeSound();
        }
        else if (action == "6")
        {
            selectedPet.ShowItems();
        }
        else if (action == "7")
        {
            UsePetItem(selectedPet);
        }
        else if (action == "8")
        {
            PlayMiniGameWithPet(selectedPet);
        }
        else if (action == "9")
        {
            GiveMedicineToPet(selectedPet);
        }
        else if (action == "10")
        {
            GiveAccessoryToPet(selectedPet);   // NEW
        }
        else if (action == "11")
        {
            GiveToyToPet(selectedPet);         // NEW
        }
        else
        {
            Console.WriteLine("Invalid action.");
            validAction = false;
        }

        if (validAction)
        {
            Console.WriteLine();
            Console.WriteLine("Press Enter to return to the main menu...");
            Console.ReadLine();

            _actionsSinceLastTick++;
            if (_actionsSinceLastTick >= 5)
            {
                ApplyTimeTick(user);
                _actionsSinceLastTick = 0;
            }
        }
        else
        {
            Console.WriteLine("Press Enter to return...");
            Console.ReadLine();
        }
    }

    // Gives medicine directly to the selected pet (without needing it in the item list).
    static void GiveMedicineToPet(Pet pet)
    {
        Console.Clear();  // Clear before medicine usage screen

        Medicine medicine = new Medicine("Standard Medicine");
        medicine.Use(pet);
    }

    // Prompts user to create and give an accessory to the pet.
    static void GiveAccessoryToPet(Pet pet)
    {
        Console.Clear();
        Console.WriteLine("Give Accessory to " + pet.GetName());
        Console.Write("Enter accessory name: ");
        string name = Console.ReadLine();

        Accessory accessory = new Accessory(name);
        pet.AddItem(accessory);

        Console.WriteLine();
        Console.WriteLine(pet.GetName() + " now has the " + name + " accessory!");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    // Prompts user to create and give a toy to the pet.
    static void GiveToyToPet(Pet pet)
    {
        Console.Clear();
        Console.WriteLine("Give Toy to " + pet.GetName());
        Console.Write("Enter toy name: ");
        string name = Console.ReadLine();

        Toy toy = new Toy(name);
        pet.AddItem(toy);

        Console.WriteLine();
        Console.WriteLine(pet.GetName() + " now has the " + name + " toy!");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }

    // Lets the user choose and use an item that the pet owns.
    static void UsePetItem(Pet pet)
    {
        List<Item> items = pet.GetItems();

        Console.WriteLine();
        Console.WriteLine("Items for " + pet.GetName() + ":");

        if (items.Count == 0)
        {
            Console.WriteLine("This pet does not have any items yet.");
            return;
        }

        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine((i + 1) + ". " + items[i].GetName() + " (" + items[i].GetType().Name + ")");
        }

        Console.Write("Choose an item number to use: ");
        string choice = Console.ReadLine();

        int index;
        if (!int.TryParse(choice, out index))
        {
            Console.WriteLine("Invalid number.");
            return;
        }

        index = index - 1;

        if (index < 0 || index >= items.Count)
        {
            Console.WriteLine("Invalid item selection.");
            return;
        }

        Console.Clear();  // Clear before showing item effect

        Item selectedItem = items[index];
        selectedItem.Use(pet);
    }

    // Lets the user pick a mini-game to play with the pet, then triggers a random event.
    static void PlayMiniGameWithPet(Pet pet)
    {
        if (pet.IsDead())
        {
            Console.WriteLine(pet.GetName() + " is no longer alive and cannot play.");
            return;
        }

        Console.Clear();  // Clear before mini-game selection

        Console.WriteLine("Choose a mini-game for " + pet.GetName() + ":");
        Console.WriteLine("1. Fetch Challenge");
        Console.WriteLine("2. Obstacle Course");
        Console.WriteLine("3. Guess The Number");
        Console.Write("Choice: ");

        string choice = Console.ReadLine();
        MiniGame miniGame;

        if (choice == "1")
        {
            miniGame = new MiniGame("Fetch Challenge");
        }
        else if (choice == "2")
        {
            miniGame = new MiniGame("Obstacle Course");
        }
        else if (choice == "3")
        {
            miniGame = new MiniGame("Guess The Number");
        }
        else
        {
            Console.WriteLine("Invalid mini-game selection.");
            return;
        }

        miniGame.StartGame(pet);

        Console.WriteLine();
        Console.WriteLine("Press Enter to continue to a random event...");
        Console.ReadLine();

        TriggerRandomEvent(pet);
    }

    // Chooses one of several GameEvent types at random and applies it.
    static void TriggerRandomEvent(Pet pet)
    {
        Console.Clear();  // Clear before event screen

        Console.WriteLine("A random event is happening...");

        Random random = new Random();
        int roll = random.Next(0, 3);

        GameEvent gameEvent;

        if (roll == 0)
        {
            gameEvent = new GameEvent(
                "Found Snack",
                pet.GetName() + " discovers a leftover treat near the yard."
            );
        }
        else if (roll == 1)
        {
            gameEvent = new GameEvent(
                "Surprise Vet Visit",
                "Time for a quick checkup at the vet."
            );
        }
        else
        {
            gameEvent = new GameEvent(
                "Thunderstorm",
                "Dark clouds gather and loud thunder echoes outside."
            );
        }

        gameEvent.TriggerEvent(pet);
    }

    // Simulates time passing: all living pets slowly lose health.
    static void ApplyTimeTick(User user)
    {
        Console.WriteLine();
        Console.WriteLine("Time passes... your pets are getting a little tired.");

        List<Pet> pets = user.GetPets();

        for (int i = 0; i < pets.Count; i++)
        {
            Pet pet = pets[i];

            if (!pet.IsDead())
            {
                pet.SetHealth(pet.GetHealth() - 5);
            }
        }
    }
}
