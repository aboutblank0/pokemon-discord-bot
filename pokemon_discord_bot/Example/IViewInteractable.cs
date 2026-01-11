using Discord.WebSocket;

namespace pokemon_discord_bot.Example
{
    public interface IViewInteractable
    {
        public Task HandleInteraction(SocketMessageComponent component, IServiceProvider serviceProvider);
    }
}
