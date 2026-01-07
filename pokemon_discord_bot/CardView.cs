using Discord;
using pokemon_discord_bot.Data;

namespace pokemon_discord_bot
{
    public class CardView
    {
        public static MessageComponent CreateDropView(String fileName, string user, EncounterEvent encounter)
        {
            List<ButtonBuilder> buttonList = new List<ButtonBuilder>();

            foreach (Pokemon pokemon in encounter.Pokemons) 
            {
                string label = pokemon.ApiPokemon.Name;
                if (pokemon.IsShiny) label = "\U0001F31F" + pokemon.ApiPokemon.Name + "\U0001F31F";

                buttonList.Add(new ButtonBuilder()
                    .WithCustomId(pokemon.PokemonId.ToString())
                    .WithLabel(label)
                    .WithStyle(ButtonStyle.Primary));
            }

            var builder = new ComponentBuilderV2()
                .WithTextDisplay($"{user} has dropped 3 pokemons!")
                .WithMediaGallery([
                    "attachment://" + fileName
                ])
                .WithActionRow(buttonList)
                .Build();

            return builder;
        }

        public static MessageComponent CreatePokemonView(String filename, Pokemon pokemon)
        {
            string pokemonStats = 
                $"**HP:** {pokemon.PokemonStats.IvHp}\n" +
                $"**ATK:** {pokemon.PokemonStats.IvAtk}\n" +
                $"**DEF:** {pokemon.PokemonStats.IvDef}\n" +
                $"**SPATK:** {pokemon.PokemonStats.IvSpAtk}\n" +
                $"**SPDEF:** {pokemon.PokemonStats.IvSpDef}\n" +
                $"**SPD:** {pokemon.PokemonStats.IvSpeed}\n" +
                $"**SIZE:** {pokemon.PokemonStats.Size}";

            var builder = new ComponentBuilderV2()
                .WithContainer(new ContainerBuilder()
                    .WithTextDisplay($"# {pokemon.FormatPokemonName(pokemon.ApiPokemon.Name)}")
                    .WithAccentColor(Color.DarkBlue)
                    .WithTextDisplay($"{pokemonStats}")
                    .WithMediaGallery([
                    "attachment://" + filename
                    ]))
                .Build();    

            return builder;
        }
    }
}
