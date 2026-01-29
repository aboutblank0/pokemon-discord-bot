using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using pokemon_discord_bot.Data;
using pokemon_discord_bot.Handlers;

namespace pokemon_discord_bot.Services
{
    public class DiscordBotService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly CommandHandler _commandHandler;
        private readonly ServerConfigService _serverSettings;

        private readonly string? _token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");

        public DiscordBotService(IServiceProvider provider)
        {
            _provider = provider;
            _client = provider.GetRequiredService<DiscordSocketClient>();
            _commands = provider.GetRequiredService<CommandService>();
            _commandHandler = provider.GetRequiredService<CommandHandler>();
            _serverSettings = provider.GetRequiredService<ServerConfigService>();
        }

        public async Task StartAsync()
        {
            if (string.IsNullOrEmpty(_token))
                throw new InvalidOperationException("DISCORD_BOT_TOKEN environment variable is missing.");

            // Apply migrations on startup (using a temporary scope)
            using (var scope = _provider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await db.Database.MigrateAsync();
            }

            // Logging
            _client.Log += Log;
            _client.Ready += OnReady;

            _client.JoinedGuild += OnGuildJoinedAsync;

            // Install commands and hook message handler
            await _commandHandler.InstallCommandsAsync();

            // Login and start
            await _client.LoginAsync(TokenType.Bot, _token);
            await _client.StartAsync();
        }

        private static Task OnReady()
        {
            Console.WriteLine("Bot is ready!");
            return Task.CompletedTask;
        }

        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task OnGuildJoinedAsync(SocketGuild guild)
        {
            ulong defaultChannelId = guild.SystemChannel != null ? guild.SystemChannel.Id : guild.TextChannels.FirstOrDefault(c => c is SocketTextChannel && c.GetType() == typeof(SocketTextChannel))?.Id ?? 0;

            await _serverSettings.SetPrimaryChannelAsync(guild.Id, defaultChannelId);

            var channel = guild.GetTextChannel(defaultChannelId);

            if (channel != null)
                await channel.SendMessageAsync("Beep Boop, this channel is set for the Pokemon encounters. Type p!set on another channel to change it");
            else
                Console.WriteLine($"Joined guild {guild.Name} but no text channels found.");
        }
    }
}
