

using Discord;
using Discord.Commands;
using pokemon_discord_bot.Data;
using pokemon_discord_bot.DiscordViews;
using PokemonBot.Data;

namespace pokemon_discord_bot.Modules
{
    public class PokemonViewModule : ModuleBase<SocketCommandContext>
    {
        private readonly AppDbContext _db;

        public PokemonViewModule(AppDbContext db)
        {
            _db = db;
        }

        [Command("view")]
        public async Task PokemonViewAsync(string pokemonId)
        {
            var user = Context.User;

            Pokemon pokemon = await _db.GetPokemonById(IdHelper.FromBase36(pokemonId));
            var pokemonSize = pokemon.PokemonStats.Size;
            List<string> pokemonSprites = new List<string>() { pokemon.GetFrontSprite() };

            var bytes = await ImageEditor.CombineImagesAsync(pokemonSprites, pokemonSize);
            var fileName = "pokemonview.png";
            var fileAttachment = new FileAttachment(new MemoryStream(bytes), fileName);
            var embed = new PokemonView(fileName, pokemon).GetEmbed();

            await Context.Channel.SendFileAsync(fileAttachment, embed: embed);
        }
    }
}
