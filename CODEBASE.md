# Console Dungeon 🗡️

**Console Dungeon** is a text-based, terminal-driven dungeon crawler built from scratch in **C#**. 

This project was developed to showcase a strong command of **core C# fundamentals**, **Object-Oriented Programming (OOP) principles**, and **modern .NET application architecture** (including built-in Dependency Injection and JSON data parsing).

---

## 🚀 Key C# & Architectural Concepts Demonstrated

Beyond just being a retro text adventure, this codebase serves as a portfolio piece demonstrating professional software engineering practices in .NET:

* **Modern Dependency Injection (DI):** Uses `Microsoft.Extensions.Hosting` and `Microsoft.Extensions.DependencyInjection` to completely decouple components. Instead of hardcoding object creation with the `new` keyword, services and state are cleanly injected via constructor injection.
* **Service Lifetimes (`AddSingleton` vs `AddTransient`):** Implements proper state management by registering the `Player` model as a `Singleton` (persisting health, coins, and equipment across the entire game session) while utilizing `Transient` services for transactional logic loops.
* **Separation of Concerns & Architecture:** Code is meticulously organized into logical directories (`Models`, `Data`, `Game`, and `Services`) to separate data structures, file I/O operations, and gameplay loops.
* **Control Flow & Game Loops:** Utilizes robust `while` loops for continuous dungeon exploration and clean multi-way `switch` statements to manage player choices, safe-zone economies, and branching narrative paths.
* **File I/O & Serialization:** Dynamically reads and parses external configuration files (`enemies.json`, `rooms.json`) using `System.Text.Json` rather than hardcoding game assets into the source code.
* **Encapsulation & State Mutation:** Employs clean properties and encapsulation to track and modify player metrics such as health points, armor value, weapon damage, inventory potions, and gold.

---

## 📂 Project Structure

```text
ConsoleDungeon/
│
├── Models/                # Plain data structures (Entities)
│   ├── Enemy.cs           # Enemy attributes (Name, Power, Health)
│   ├── Player.cs          # Player state (Name, HP, Coins, Damage, Potions)
│   └── RoomData.cs        # Room structures and metadata
│
├── Data/                  # External configuration files
│   ├── enemies.json       # Enemy database
│   └── rooms.json         # Room templates
│
├── Game/                  # Core game logic, control flow, and UI
│   ├── DungeonLoader.cs   # Handles room loading and generation
│   ├── Encounters.cs      # Combat mechanics and turn processing
│   ├── EnemyLoader.cs     # Parses enemy JSON data streams
│   ├── GameEngine.cs      # Main game loop and room progression controller
│   └── Introduction.cs    # Narrative and startup text blocks
│
└── Program.cs             # The Bootstrapper (Configures the DI host and initializes the app)
```

---

## 🛠️ Tech Stack & Requirements
Language: C# 10+ / .NET 6.0+ (Compatible with latest .NET SDK)
Core Libraries / Packages:
Microsoft.Extensions.Hosting (Handles application hosting and the built-in DI container)
System.Text.Json (Handles parsing of external room and enemy data files)

## 🏃‍♂️ How to Run the Game
Open your terminal in the root project folder (ConsoleDungeon/).
Restore or ensure the required hosting package is installed:

```Bash

dotnet add package Microsoft.Extensions.Hosting
dotnet add package Microsoft.Extensions.DependencyInjection

```

Build and execute the application:

```Bash

dotnet run

```

Created as a demonstration of foundational and intermediate C# engineering capabilities.