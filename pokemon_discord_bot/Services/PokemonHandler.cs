using pokemon_discord_bot.Data;
using PokemonBot.Data;

namespace pokemon_discord_bot.Services
{
    public class PokemonHandler
    {
        private Dictionary<ulong, Pokemon> _lastPokemonOwned;

        public PokemonHandler() 
        {
            _lastPokemonOwned = new Dictionary<ulong, Pokemon>();
        }

        public async Task<Pokemon> GetPokemonAsync(ulong userId, string? pokemonId, AppDbContext db)
        {
            if (pokemonId != null)
                return await db.GetPokemonById(IdHelper.FromBase36(pokemonId));

            if (!_lastPokemonOwned.ContainsKey(userId)) 
                throw new Exception("Bot has restarted, no Pokemon cached");

            return _lastPokemonOwned[userId];
        }

        public void SetLastPokemonOwned(ulong userId, Pokemon pokemon)
        {
            _lastPokemonOwned[userId] = pokemon;
        }
    }
}
