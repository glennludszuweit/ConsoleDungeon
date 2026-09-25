using ConsoleDungeon.Game;
using ConsoleDungeon.Models;
using ConsoleDungeon.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Build the host and configure services container
var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Inject services to be available throiugh out the application
                services.AddSingleton<Player>();
                services.AddSingleton<EnemyLoader>();
                services.AddSingleton<DungeonLoader>();
                services.AddTransient<GameEngine>();
                services.AddTransient<Encounters>();
                services.AddTransient<SaveService>();
            })
            .Build();

// Resolve the GameEngine from the container and start the game
var gameEngine = host.Services.GetRequiredService<GameEngine>();
gameEngine.Run();