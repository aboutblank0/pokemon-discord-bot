using Discord.Commands;
using pokemon_discord_bot.Services;

namespace pokemon_discord_bot.Modules
{
    public class SetupModule : ModuleBase<SocketCommandContext>
    {
        private readonly ServerConfigService _serverSettings;

        public SetupModule(ServerConfigService serverSettings)
        {
            _serverSettings = serverSettings;
        }

        [Command("prefix")]
        public async Task SetPrefixAsync(string customPrefix = "p!")
        {
            _serverSettings.SetServerPrefix(Context.Guild.Id, customPrefix);

            await Context.Channel.SendMessageAsync($"Prefix changed to `{customPrefix}`");
        }


        [Command("set")]
        public async Task SetPrimaryChannelAsync()
        {
            await _serverSettings.SetPrimaryChannelAsync(Context.Guild.Id, Context.Channel.Id);

            await Context.Channel.SendMessageAsync($"Pokemon encounters set to <#{Context.Channel.Id}>");
        }
    }
}
