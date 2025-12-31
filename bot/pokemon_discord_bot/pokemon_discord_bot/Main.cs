using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pokemon_discord_bot
{
    public class Program
    {

        

        public static async Task Main()
        {
            DiscordSocketClient client = new DiscordSocketClient();
            client.Log += Log;

            String token = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await Task.Delay(-1);
        }


        private static Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

    }

}
