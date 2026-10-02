# Console Dungeon 🗡️

**Console Dungeon** is a text-based, terminal-driven dungeon crawler built from scratch in **C#** using a robust **Clean Architecture** pattern. 

This project was developed to showcase a strong command of **core C# fundamentals**, **Object-Oriented Programming (OOP) principles**, and **modern .NET application architecture** (including built-in Dependency Injection and JSON data parsing).

---

## 🚀 Key C# & Architectural Concepts Demonstrated

Beyond just being a retro text adventure, this codebase serves as a portfolio piece demonstrating professional software engineering practices in .NET:

* **Clean Architecture & Separation of Concerns:** Strictly decoupled into layers (`Domain`, `Application`, `Infrastructure`, and `Presentation`) ensuring business logic is independent of UI, frameworks, and external data sources.
* **Modern Dependency Injection (DI):** Uses `Microsoft.Extensions.Hosting` and `Microsoft.Extensions.DependencyInjection` to completely wire up components via constructor injection.
* **Service Lifetimes (`AddSingleton` vs `AddTransient`):** Implements proper state management by registering core state (like the `Player`) as a `Singleton` while utilizing `Transient` services for transactional gameplay loops.
* **Control Flow & Game Loops:** Utilizes robust `while` loops for continuous dungeon exploration and clean multi-way `switch` statements to manage player choices, safe-zone economies, and branching narrative paths.
* **File I/O & Serialization:** Dynamically reads and parses external configuration files (`enemies.json`, `rooms.json`) using `System.Text.Json` within the Infrastructure layer.
* **Encapsulation & State Mutation:** Employs clean properties and encapsulation to track and modify player metrics such as health points, armor value, weapon damage, inventory potions, and gold.

---

## 📂 Project Structure (Clean Architecture)

```text
ConsoleDungeon/
│
├── Domain/                      # Core entities and enterprise rules
│   ├── Models/                  # Player, Enemy, and RoomData entities
│   └── Interfaces/              # Domain contracts
│
├── Application/                 # Business logic and use cases
│   ├── GameEngine.cs            # Main game loop and state controller
│   ├── Encounters.cs            # Combat mechanics and turn processing
│   └── Services/                # Application-specific logic
│
├── Infrastructure/              # External concerns and data access
│   ├── Data/                    # External JSON configuration files (enemies.json, rooms.json)
│   ├── DungeonLoader.cs         # Room loading and generation implementation
│   └── EnemyLoader.cs           # Enemy JSON parsing stream implementation
│
└── Presentation/                # Entry point and user interface
    └── Program.cs               # DI host configuration and app bootstrapper

```

## 🛠️ Tech Stack & Requirements

* **Language:** C# 10+ / .NET 6.0+ (Compatible with latest .NET SDK)
* **Core Libraries & NuGet Packages:**
  * `Microsoft.Extensions.Hosting` (Handles application hosting infrastructure)
  * `Microsoft.Extensions.DependencyInjection` (Handles the built-in DI container and service mapping)
  * `System.Text.Json` (Handles parsing of external room and enemy configuration streams)

## 🏃‍♂️ How to Run & Build

### 1. Run Locally via CLI
Open your terminal in the root project folder and ensure dependencies are restored:
```bash

dotnet restore

```

Then run the application directly through the CLI:

```bash

dotnet restore

```

Then run the application directly through the CLI:

```bash

dotnet run --project ConsoleDungeon/ConsoleDungeon.csproj

```

### 2. Build a Standalone Executable (`.exe`)
To package your game into a self-contained, single-file executable for distribution and portfolio showcasing, run the following command from your solution root:

```bash

dotnet publish ConsoleDungeon/ConsoleDungeon.csproj -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true -o ./publish

```

This will create a dedicated publish folder right in your root directory containing your standalone ConsoleDungeon.exe.

"Important Deployment Note: Because single-file publishing compiles your C# code into the binary while leaving external data loose, ensure your Data/ folder (containing your rooms.json and enemies.json configuration files) is placed directly alongside the .exe inside the publish folder so the application can load game assets properly at runtime!"

Created as a demonstration of foundational and intermediate C# engineering capabilities.