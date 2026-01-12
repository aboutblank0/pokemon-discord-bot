using Discord;
using Discord.Commands;
using pokemon_discord_bot.Data;
using pokemon_discord_bot.DiscordViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pokemon_discord_bot.Modules
{
    public class CommandsModule : ModuleBase<SocketCommandContext>
    {

        [Command("commands")]
        [Alias("help")]

        public async Task CommandsViewAsync()
        {
            var component = new CommandsView().GetComponent();

            await Context.Channel.SendMessageAsync(null, components: component);
        }
    }
}
