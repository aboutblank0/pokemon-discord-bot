using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using PokemonBot.Data;

namespace pokemon_discord_bot
{
    public class Program
    {
        private static DiscordSocketClient _client = null!;
        private static AppDbContext _db = null!;

        public static async Task Main()
        {
            ApiPokemonData.Init();

            // Apply migrations automatically 
            _db = new AppDbContext();
            await _db.Database.MigrateAsync();

            var config = new DiscordSocketConfig
            {
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            };

            _client = new DiscordSocketClient(config);
            _client.Log += Log;
            _client.Ready += OnReady;

            string? token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            CommandService services = new CommandService();
            CommandHandler commands = new CommandHandler(_client, services);
            await commands.InstallCommandsAsync();

            await Task.Delay(-1);
        }

        private static Task OnReady()
        {
            return Task.CompletedTask;
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
