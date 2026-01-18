using Discord;
using pokemon_discord_bot.Data;

namespace pokemon_discord_bot.DiscordViews
{
    public class PokemonView
    {
        private string _filename;
        private Pokemon _pokemon;

        public PokemonView(string filename, Pokemon pokemon)
        {
            _filename = filename;
            _pokemon = pokemon;
        }

        public Embed GetEmbed()
        {
            string pokemonStats = new AnsiBuilder()
                .WithLine($"{FormatStat(_pokemon.PokemonStats.IvHp)}", TextColor.Green, bold: true).WithText(" HP")
                .WithLine($"{FormatStat(_pokemon.PokemonStats.IvAtk)}", TextColor.Yellow, bold: true).WithText(" ATK")
                .WithLine($"{FormatStat(_pokemon.PokemonStats.IvDef)}", TextColor.Green, bold: true).WithText(" DEF")
                .WithLine($"{FormatStat(_pokemon.PokemonStats.IvSpAtk)}", TextColor.Green, bold: true).WithText(" SPATK")
                .WithLine($"{FormatStat(_pokemon.PokemonStats.IvSpDef)}", TextColor.Green, bold: true).WithText(" SPDEF")
                .WithLine($"{FormatStat(_pokemon.PokemonStats.IvSpeed)}", TextColor.Green, bold: true).WithText(" SPEED")
                .WithBlankSpace()
                .WithLine("SIZE:").WithText($" {_pokemon.PokemonStats.Size}", TextColor.Green, bold: true)
                .Build();

            var builder = new EmbedBuilder()
                .WithColor((uint)new Random().Next(0, 16777216))
                .WithDescription($"### Pokemon stats\n" +
                    $"Owned by <@{_pokemon.OwnedBy}> \n\n" +
                    $"Name: **{_pokemon.FormattedName}** (`{_pokemon.IdBase36}`)\n" +
                    $"Total IV: **{_pokemon.PokemonStats.TotalIvPercent}%**\n" +
                    $"{pokemonStats}")
                .WithImageUrl("attachment://" + _filename)
                .Build();
            return builder;
        }

        public string FormatStat(int stat)
        {
            if (stat < 10)
                return $" {stat}";

            return $"{stat}";
        }
    }
}
