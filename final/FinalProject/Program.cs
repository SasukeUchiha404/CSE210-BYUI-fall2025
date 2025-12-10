// ============================================================================
// Virtual Pet Simulator Console Application
// ----------------------------------------------------------------------------
// Description:
//    A menu-driven virtual pet simulator where users adopt and care for pets.
//    - Supports multiple pet types (Dog, Cat, Bird, RobotPet) via inheritance.
//    - Uses polymorphic behaviors such as MakeSound() on the Pet base class.
//    - Allows basic interactions (feed, play, sleep, status) through a console UI.
//    - Pets can own Items (toys, accessories, etc.) and show them to the user.
// 
// Updated: December 10, 2025
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

        dog.AddItem(ball);
        cat.AddItem(mouse);
        bird.AddItem(seeds);
        robot.AddItem(collar);

        // Add pets to the user.
        user.AddPet(dog);
        user.AddPet(cat);
        user.AddPet(bird);
        user.AddPet(robot);

        bool running = true;

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== Virtual Pet Simulator ===");
            Console.WriteLine("1. List Pets");
            Console.WriteLine("2. Interact With Pet");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();

            if (input == "1")
            {
                user.ShowPets();
            }
            else if (input == "2")
            {
                InteractWithPet(user);
            }
            else if (input == "3")
            {
                running = false;
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }

        Console.WriteLine("Thank you for playing the Virtual Pet Simulator.");
    }

    // Displays a submenu to interact with a selected pet.
    static void InteractWithPet(User user)
    {
        Console.WriteLine();
        Console.WriteLine("Select a pet by number:");

        List<Pet> pets = user.GetPets();

        if (pets.Count == 0)
        {
            Console.WriteLine("You do not have any pets yet.");
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
            return;
        }

        index = index - 1;

        if (index < 0 || index >= pets.Count)
        {
            Console.WriteLine("Invalid pet selection.");
            return;
        }

        Pet selectedPet = pets[index];

        Console.WriteLine();
        Console.WriteLine("Interacting with " + selectedPet.GetName());
        Console.WriteLine("1. Feed");
        Console.WriteLine("2. Play");
        Console.WriteLine("3. Sleep");
        Console.WriteLine("4. Show Status");
        Console.WriteLine("5. Make Sound (Polymorphism)");
        Console.WriteLine("6. Show Items");
        Console.WriteLine("7. Use Item");
        Console.Write("Choose an action: ");

        string action = Console.ReadLine();

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
            // Polymorphic call: concrete MakeSound() depends on the pet type.
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
        else
        {
            Console.WriteLine("Invalid action.");
        }
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

        Item selectedItem = items[index];

        // Use is also polymorphic on the Item hierarchy.
        selectedItem.Use(pet);
    }
}
