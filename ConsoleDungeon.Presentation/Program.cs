using ConsoleDungeon.Application.Game;
using ConsoleDungeon.Application.Interfaces;
using ConsoleDungeon.Domain.Models;
using ConsoleDungeon.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Build the host and configure services container
var host = Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                // Inject services to be available throiugh out the application
                services.AddSingleton<Player>();
                services.AddSingleton<EnemyLoader>();
                services.AddSingleton<IDungeonLoader, DungeonLoader>();
                services.AddTransient<ISaveService, SaveService>();
                services.AddTransient<GameEngine>();
                services.AddTransient<Encounters>();
            })
            .Build();

// Resolve the GameEngine from the container and start the game
var gameEngine = host.Services.GetRequiredService<GameEngine>();
gameEngine.Run();