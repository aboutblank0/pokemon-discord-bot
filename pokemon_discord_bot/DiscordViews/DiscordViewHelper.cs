using Discord;

namespace pokemon_discord_bot.DiscordViews
{
    public static class DiscordViewHelper
    {
        public static ButtonBuilder CreateViewButton(string customId, string label)
        {
            return new ButtonBuilder()
                .WithCustomId(customId)
                .WithLabel(label)
                .WithStyle(ButtonStyle.Primary);
        }
    }
}
