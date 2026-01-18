using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using pokemon_discord_bot.Data;
using pokemon_discord_bot.Example;
using pokemon_discord_bot.Services;
using PokemonBot.Data;

namespace pokemon_discord_bot.DiscordViews
{
    public class EncounterView : IViewInteractable
    {
        private readonly EncounterEventHandler _encounterEventHandler;
        private readonly EncounterEvent _encounter;
        private readonly SocketUser _user;
        private readonly PokemonHandler _pokemonHandler;

        private string _fileName;

        public EncounterView(EncounterEventHandler encounterEventHandler, EncounterEvent encounter, SocketUser user, PokemonHandler pokemonHandler, string fileName)
        {
            _encounterEventHandler = encounterEventHandler;
            _encounter = encounter;
            _user = user;
            _pokemonHandler = pokemonHandler;
            _fileName = fileName;
        }

        public MessageComponent GetComponent()
        {
            bool isLegendary = false;
            bool isMythical = false;

            List<ButtonBuilder> buttonList = new List<ButtonBuilder>();

            foreach (Pokemon pokemon in _encounter.Pokemons)
            {
                if (pokemon.ApiPokemon.Weight == 10) isLegendary = true;
                if (pokemon.ApiPokemon.Weight == 5) isMythical = true;

                string label = pokemon.FormattedName;
                if (pokemon.IsShiny) label = "\U0001F31F" + pokemon.FormattedName + "\U0001F31F";

                buttonList.Add(DiscordViewHelper.CreateViewButton("drop-button" + IdHelper.ToBase36(pokemon.PokemonId), label));
            }

            return new ComponentBuilderV2()
                .WithTextDisplay($"{_user.Mention} found 3 pokemons!\n" +
                    $"{HasFoundSpecialPokemon(isLegendary, isMythical)}")
                .WithMediaGallery([
                    "attachment://" + _fileName
                ])
                .WithActionRow(buttonList)
                .Build();
        }

        private string HasFoundSpecialPokemon(bool isLegendary, bool isMythical)
        {
            if (isLegendary && isMythical) return "A **Mythical** and a **Legendary** Pokemon have appeared! \U0001F52E";
            if (isLegendary) return "A **Legendary** Pokemon has appeared! \U0001F52E";
            if (isMythical) return "A **Mythical** Pokemon has appeared! \U0001F52E";
            
            return "";
        }

        public MessageComponent GetExpiredContent()
        {
            return new ComponentBuilderV2()
                .WithTextDisplay("*All remaining pokemons have vanished.*")
                .WithMediaGallery([
                    "attachment://" + _fileName
                ])
                .Build();
        }

        public async Task HandleInteraction(SocketMessageComponent component, IServiceProvider serviceProvider)
        {
            var db = serviceProvider.GetRequiredService<AppDbContext>();

            if (component.User.Id != _user.Id && _encounterEventHandler.CanDifferentUserClaimPokemon(_user.Id) == false)
            {
                await component.RespondAsync("You cannot interact with this button!", ephemeral: true);
                return;
            }

            string pokemonId = component.Data.CustomId.Substring("drop-button".Length);
            Pokemon pokemon = await _pokemonHandler.GetPokemonAsync(component.User.Id, pokemonId, db);

            if (pokemon.CaughtBy != 0) return;

            //Cache last pokemon caught by user who interacted with this component
            _pokemonHandler.SetLastPokemonOwned(component.User.Id, pokemon);

            pokemon.CaughtBy = component.User.Id;
            pokemon.OwnedBy = component.User.Id;
            db.SaveChanges();

            await component.RespondAsync($"{component.User.Mention} caught {pokemon.FormattedName} `{pokemon.IdBase36}` - IV: `{pokemon.PokemonStats.TotalIvPercent}%`");
        }
    }
}
