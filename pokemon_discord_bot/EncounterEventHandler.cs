using Microsoft.Extensions.DependencyInjection;
using pokemon_discord_bot.Data;
using PokemonBot.Data;

namespace pokemon_discord_bot
{
    public class EncounterEventHandler
    {
        private const uint DROP_COOLDOWN_SECONDS = 5;
        private const uint CLAIM_COOLDOWN_SECONDS = 5;

        private readonly IServiceScopeFactory _scopeFactory;

        private Dictionary<ulong, DateTimeOffset> _lastTriggerTime;
        private Dictionary<ulong, DateTimeOffset> _lastClaimTime;

        public EncounterEventHandler(IServiceScopeFactory scopeFactory) {
            _scopeFactory = scopeFactory;

            _lastTriggerTime = new Dictionary<ulong, DateTimeOffset>();
            _lastClaimTime = new Dictionary<ulong, DateTimeOffset>();
        }

        public async Task<EncounterEventWithPokemon> CreateRandomEncounterEvent(int pokemonAmount, ulong userId)
        {
            if (!CanUserTriggerEncounter(userId)) 
                throw new Exception("Tried to create random encounter event when user is on cooldown. Should always call CanUserTriggerEncounter first");

            EncounterEvent encounterEvent = new EncounterEvent();
            encounterEvent.Biome = BiomeType.FOREST;
            encounterEvent.TriggeredBy = (long) userId;

            //Below line needs an EncounterEvent in the DB present
            using var scope = _scopeFactory.CreateScope();
            AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            List<Pokemon> pokemons = await CreateRandomPokemons(pokemonAmount, encounterEvent, db);
            await db.SaveChangesAsync();

            _lastTriggerTime[userId] = DateTimeOffset.UtcNow;

            EncounterEventWithPokemon encounterEventWithPokemon = new EncounterEventWithPokemon();
            encounterEventWithPokemon.EncounterEvent = encounterEvent;
            encounterEventWithPokemon.Pokemons = pokemons;
            return encounterEventWithPokemon;
        }

        private async Task<List<Pokemon>> CreateRandomPokemons(int pokemonAmount, EncounterEvent encounterEvent, AppDbContext db)
        {
            ApiPokemon[] randomPokemons = ApiPokemonData.Instance.GetRandomPokemon(3);
            List<Pokemon> pokemons = new List<Pokemon>();

            foreach (ApiPokemon apiPokemon in randomPokemons)
            {
                Pokemon pokemon = new Pokemon();
                pokemon.ApiPokemonId = (int) apiPokemon.Id;
                pokemon.EncounterEvent = encounterEvent;
                pokemon.IsShiny = false;             //TODO: SHOULD BE RANDOMLY SHINY/NOT SHINY DEPENDING ON SOME CHANCE
                pokemon.Gender = PokemonGender.MALE; //TODO: SHOULD BE RANDOMLY MALE/FEMALE/GENDERLESS (depending on whicih pokemon)

                Random random = new Random();
                pokemon.PokemonStats = new PokemonStats()
                {
                    IvAtk = (short)(random.NextInt64(0, 31) + 1),
                    IvDef = (short)(random.NextInt64(0, 31) + 1),
                    IvHp = (short)(random.NextInt64(0, 31) + 1),
                    IvSpAtk = (short)(random.NextInt64(0, 31) + 1),
                    IvSpDef = (short)(random.NextInt64(0, 31) + 1),
                    IvSpeed = (short)(random.NextInt64(0, 31) + 1),
                    Size = 1.0f //TODO: SHOULD BE RANDOMLY GENERATED
                };

                pokemons.Add(pokemon);
            }

            await db.Pokemon.AddRangeAsync(pokemons);
            return pokemons;
        }

        public bool CanUserTriggerEncounter(ulong userId)
        {
            if (!_lastTriggerTime.ContainsKey(userId)) return true;

            //Check if user is on cooldown
            DateTimeOffset lastTrigger = _lastTriggerTime[userId];
            var elapsed = DateTimeOffset.UtcNow - lastTrigger;
            return elapsed.TotalSeconds > DROP_COOLDOWN_SECONDS;
        }
    }

    public class EncounterEventWithPokemon
    {
        public EncounterEvent EncounterEvent { get; set; }
        public List<Pokemon> Pokemons { get; set; }
    }
}
