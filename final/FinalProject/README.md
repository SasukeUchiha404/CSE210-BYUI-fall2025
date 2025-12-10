Virtual Pet Simulator
Overview
A complete console-based virtual pet simulator built in C# demonstrating OOP principles including inheritance, polymorphism, encapsulation, and composition. Pets have health, hunger, happiness stats and can die if neglected.

Features
4 Pet Types: Dog, Cat, Bird, RobotPet (polymorphic MakeSound(), custom behaviors)

Pet Stats: Health (decays over time), Hunger, Happiness, Items, Revive Count

Pet Limits: Maximum 8 pets, "Release Pet" option to remove

3 Mini-Games: Fetch Challenge, Obstacle Course, Guess The Number (some risk health loss)

3 Random Events: Found Snack, Surprise Vet Visit, Thunderstorm (trigger after games)

Item System: Food, Toys, Medicine (3-revive limit, no back-to-back), Accessories

Death & Revival: Pets die at 0 health, medicine revives (max 3x per pet)

Clean UI: Console clears between menus/activities for professional look

How to Run
Requirements: Visual Studio 2022 (Community free) or VS Code with C# extension

Clone/Download: Get all .cs files in one folder

Build & Run:

text
# In Visual Studio: File > Open > Folder > Build > Run (F5)
# Or terminal: dotnet new console -n VirtualPet && cd VirtualPet && dotnet run
Each class must be in its own .cs file (Program.cs, Pet.cs, Dog.cs, etc.)

Controls & Gameplay
text
Main Menu:
1. List Pets (shows X/8 count, dead status)
2. Interact With Pet (full actions menu)
3. Add New Pet (name pet, choose type)
4. Release Pet (remove from collection)
5. Exit

Pet Actions:
1-3. Feed/Play/Sleep (basic care)
4. Show Status (all stats + revive count)
5. Make Sound (polymorphic!)
6-7. Show/Use Items
8. Play Mini-Game (triggers random event after)
9. Give Medicine (revive or heal)
10. Give Accessory (cosmetic)
11. Give Toy (custom name)
Key Mechanics
Health Decay: Every 5 actions, all pets lose 5 health

Death: Health ≤ 0 = dead (can't interact, shows [DEAD])

Medicine Rules: Max 3 revives/pet, can't use twice in a row

Mini-Games: Some fail → health loss, success → stat gains

Time Passes: Action counter triggers periodic health drain

OOP Design
text
Pet (abstract base)
├── Dog     ├── Cat     ├── Bird     └── RobotPet
Item (abstract base)                    ↑
├── Food    ├── Toy     ├── Medicine   └── Accessory
MiniGame & GameEvent (utility classes)
User (owns collection of Pet)
Program (menu navigation)
Sample Output
text
=== Virtual Pet Simulator ===
Current pets: 4/8
1. List Pets           2. Interact With Pet
3. Add New Pet         4. Release Pet
5. Exit
Troubleshooting
"Class not found": Ensure each class is in its own .cs file with matching name

Build errors: Delete bin/obj folders, rebuild

No pets?: Use "Add New Pet" → all start at 50/100 stats

Pets dying fast?: Health decays every 5 actions, use Sleep/Medicine

Academic Standards Met
PascalCase classes/methods, camelCase fields

Explicit getters/setters (no properties/lambdas)

One class per .cs file

Proper formatting/whitespace

Full encapsulation with private fields

Built December 3, 2025 - Current version December 10th, 2025