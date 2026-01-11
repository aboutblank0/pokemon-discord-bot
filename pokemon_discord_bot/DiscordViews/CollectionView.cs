using Discord;
using Discord.WebSocket;
using pokemon_discord_bot.Data;
using System.Text;

namespace pokemon_discord_bot.DiscordViews
{
    public class CollectionView
    {
        private const int POKEMONS_PER_COLLECTION_PAGE = 10;

        public static (Embed, MessageComponent) CreateCollectionView(List<Pokemon> pokemonList, SocketUser user, int pageIndex)
        {
            StringBuilder list = new StringBuilder();

            var range = new Range(pageIndex * POKEMONS_PER_COLLECTION_PAGE, (pageIndex + 1) * POKEMONS_PER_COLLECTION_PAGE);
            foreach (Pokemon pokemon in pokemonList.Take(range))
            {
                string name = pokemon.FormattedName;
                if (pokemon.IsShiny) name = pokemon.FormattedName + " ✨";
                list.AppendLine($"**{name}** - `{pokemon.IdBase36}` - IV: `{pokemon.PokemonStats.TotalIvPercent}%`");
            }

            List<ButtonBuilder> buttonList = new List<ButtonBuilder>();
            buttonList.Add(DiscordViewHelper.CreateViewButton($"pagination-button-first-page-{user.Id}", "\U000021A9"));
            buttonList.Add(DiscordViewHelper.CreateViewButton($"pagination-button-previous-page-{user.Id}", "\U00002190"));
            buttonList.Add(DiscordViewHelper.CreateViewButton($"pagination-button-next-page-{user.Id}", "\U00002192"));
            buttonList.Add(DiscordViewHelper.CreateViewButton($"pagination-button-last-page-{user.Id}", "\U000021AA"));

            var embed = new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithDescription($"### {user.Mention}'s collection\n\n\n" + list.ToString())
                .Build();

            var component = new ComponentBuilderV2()
                .WithActionRow(buttonList)
                .Build();

            return (embed, component);
        }
    }
}
