using Discord;
using pokemon_discord_bot.Data;

namespace pokemon_discord_bot.DiscordViews
{
    public class PokemonView
    {
        public static Embed CreatePokemonView(String filename, Pokemon pokemon)
        {
            string pokemonStats = new AnsiBuilder()
                .WithLine($"{pokemon.PokemonStats.IvHp}", TextColor.Green).WithText(" HP")
                .WithLine($"{pokemon.PokemonStats.IvAtk}", TextColor.Yellow).WithText(" ATK", TextColor.Yellow, bold: true)
                .WithLine($"{pokemon.PokemonStats.IvDef}", TextColor.Green).WithText(" DEF")
                .WithLine($"{pokemon.PokemonStats.IvSpAtk}", TextColor.Green).WithText(" SPATK")
                .WithLine($"{pokemon.PokemonStats.IvSpDef}", TextColor.Green).WithText(" SPDEF")
                .WithLine($"{pokemon.PokemonStats.IvSpeed}", TextColor.Green).WithText(" SPEED")
                .WithBlankSpace()
                .WithLine("SIZE:").WithText($" {pokemon.PokemonStats.Size}", TextColor.Green, bold: true)
                .Build();

            var builder = new EmbedBuilder()
                .WithColor((uint)new Random().Next(0, 16777216))
                .WithThumbnailUrl("attachment://" + filename)
                .WithDescription($"### Pokemon stats\n" +
                    $"Name: **{pokemon.FormattedName}** (`{pokemon.IdBase36}`)\n" +
                    $"Total IV: **{pokemon.PokemonStats.TotalIvPercent}%**\n" +
                    $"{pokemonStats}")
                .Build();

            return builder;
        }
    }
}
