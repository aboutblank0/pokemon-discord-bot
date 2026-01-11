using Discord;
using Discord.WebSocket;
using pokemon_discord_bot.Data;

namespace pokemon_discord_bot.DiscordViews
{
    public class EncounterView
    {
        public static MessageComponent CreateEncounterView(String fileName, SocketUser user, EncounterEvent encounter)
        {
            List<ButtonBuilder> buttonList = new List<ButtonBuilder>();

            foreach (Pokemon pokemon in encounter.Pokemons)
            {
                string label = pokemon.FormattedName;
                if (pokemon.IsShiny) label = "\U0001F31F" + pokemon.FormattedName + "\U0001F31F";

                buttonList.Add(DiscordViewHelper.CreateViewButton("drop-button" + pokemon.PokemonId.ToString(), label));
            }

            var builder = new ComponentBuilderV2()
                .WithTextDisplay($"{user.Mention} found 3 pokemons!")
                .WithMediaGallery([
                    "attachment://" + fileName
                ])
                .WithActionRow(buttonList)
                .Build();

            return builder;
        }
    }
}
