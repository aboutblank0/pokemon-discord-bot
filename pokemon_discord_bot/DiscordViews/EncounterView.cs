using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
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

        private string _fileName;

        public EncounterView(EncounterEventHandler encounterEventHandler, EncounterEvent encounter, SocketUser user, string fileName)
        {
            _encounterEventHandler = encounterEventHandler;
            _encounter = encounter;
            _user = user;   
            _fileName = fileName;
        }

        public MessageComponent GetComponent()
        {
            List<ButtonBuilder> buttonList = new List<ButtonBuilder>();

            foreach (Pokemon pokemon in _encounter.Pokemons)
            {
                string label = pokemon.FormattedName;
                if (pokemon.IsShiny) label = "\U0001F31F" + pokemon.FormattedName + "\U0001F31F";

                buttonList.Add(DiscordViewHelper.CreateViewButton("drop-button" + pokemon.PokemonId.ToString(), label));
            }

            return new ComponentBuilderV2()
                .WithTextDisplay($"{_user.Mention} found 3 pokemons!")
                .WithMediaGallery([
                    "attachment://" + _fileName
                ])
                .WithActionRow(buttonList)
                .Build();
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

            //Random random = new Random();
            //var guild = _client.GetGuild(component.GuildId ?? 0);
            //var emoji = guild.Emotes.ElementAt(random.Next(guild.Emotes.Count));

            if (component.User.Id != _user.Id && _encounterEventHandler.CanDifferentUserClaimPokemon(_user.Id) == false)
            {
                await component.RespondAsync("You cannot interact with this button!", ephemeral: true);
                return;
            }

            int pokemonId = int.Parse(component.Data.CustomId.Substring("drop-button".Length));
            Pokemon pokemon = await db.GetPokemonById(pokemonId);

            if (pokemon.CaughtBy != 0) return;

            pokemon.CaughtBy = component.User.Id;
            pokemon.OwnedBy = component.User.Id;
            db.SaveChanges();

            await component.RespondAsync($"{component.User.Mention} caught {pokemon.FormattedName} `{pokemon.IdBase36}` - IV: `{pokemon.PokemonStats.TotalIvPercent}%`");
        }
    }
}
