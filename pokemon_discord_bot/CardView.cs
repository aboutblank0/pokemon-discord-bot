using Discord;
using Discord.WebSocket;
using System.Text;

namespace pokemon_discord_bot
{
    public class CardView
    {
        public static MessageComponent CreateDropView(String fileName)
        {
            var builder = new ComponentBuilderV2()
                .WithTextDisplay("Testing")
                .WithMediaGallery([
                    "attachment://" + fileName
                ])
                .WithActionRow([
                    new ButtonBuilder()
                    .WithCustomId("1")
                    .WithLabel("1")
                    .WithStyle(ButtonStyle.Primary),

                    new ButtonBuilder()
                    .WithCustomId("2")
                    .WithLabel("2")
                    .WithStyle(ButtonStyle.Primary),

                    new ButtonBuilder()
                    .WithCustomId("3")
                    .WithLabel("3")
                    .WithStyle(ButtonStyle.Primary)
                ])
                .Build();


            return builder;
        }
    }
}
