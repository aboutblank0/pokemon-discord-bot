using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PokemonBot.Data;

namespace pokemon_discord_bot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var services = new ServiceCollection();

            // Discord services
            services.AddSingleton(new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            });
            services.AddSingleton<DiscordSocketClient>();
            services.AddSingleton<CommandService>();
            services.AddSingleton<CommandHandler>();
            services.AddSingleton<EncounterEventHandler>();

            services.AddScoped<AppDbContext>();

            // Add any other services here (e.g., custom services for your bot logic)
            var provider = services.BuildServiceProvider(validateScopes: true);

            var bot = new DiscordBotService(provider);
            await bot.StartAsync();

            // Keep the bot running
            await Task.Delay(-1);
        }
    }
}

