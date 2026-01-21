using Discord;

namespace pokemon_discord_bot.DiscordViews
{
    public static class DiscordViewHelper
    {
        private static Timer? _inactivityTimer;
        private static readonly TimeSpan _inactivityTimeout = TimeSpan.FromMinutes(10);

        public static void StartInactivityTimer(Action onTimeout)
        {
            _inactivityTimer = new Timer(_ =>
            {
                onTimeout?.Invoke();
                _inactivityTimer?.Dispose();
                _inactivityTimer = null;
            }, null, _inactivityTimeout, Timeout.InfiniteTimeSpan);
        }

        public static void StartInactivityTimer(Func<Task> onTimeoutAsync) // Async for ModifyAsync
        {
            _inactivityTimer = new Timer(async _ =>
            {
                await onTimeoutAsync();
                _inactivityTimer?.Dispose();
                _inactivityTimer = null;
            }, null, _inactivityTimeout, Timeout.InfiniteTimeSpan);
        }

        public static void ResetInactivityTimer()
        {
            _inactivityTimer?.Change(_inactivityTimeout, Timeout.InfiniteTimeSpan);
        }

        // Optional: Cleanup on dispose if needed
        public static void StopInactivityTimer()
        {
            _inactivityTimer?.Dispose();
            _inactivityTimer = null;
        }

        public static readonly Dictionary<string, string> PokeballEmotes = new()
        {
            { "Poke Ball", "<:poke_ball:1463071512096673864>" },
            { "Great Ball", "<:great_ball:1463071536368848958>" },
            { "Ultra Ball", "<:ultra_ball:1463071561920544873>" },
            { "Master Ball", "<:master_ball:1463071578626588839>" },
        };

        public static ButtonBuilder CreateViewButton(string customId, string label, ButtonStyle style = ButtonStyle.Primary, bool isDisabled = false, Emote? emote = null)
        {
            return new ButtonBuilder()
                .WithCustomId(customId)
                .WithLabel(label)
                .WithStyle(style)
                .WithDisabled(isDisabled)
                .WithEmote(emote);
                
        }
    }
}
