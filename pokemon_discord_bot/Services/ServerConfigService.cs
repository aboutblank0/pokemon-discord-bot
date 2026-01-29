using Newtonsoft.Json;
using System.Collections.Concurrent;

namespace pokemon_discord_bot.Services
{
    public class ServerConfigService
    {
        private const string FILE_PATH = "server_settings.json";
        private readonly ConcurrentDictionary<ulong, ServerConfigs> _settings = new();
        private readonly ServerConfigs _defaultSettings = new();

        public ServerConfigService()
        {
            LoadSettings();
        }

        public string GetServerPrefix(ulong guildId)
        {
            return GetSettings(guildId).Prefix;
        }

        public void SetServerPrefix(ulong guildId, string prefix)
        {
            var settings = GetSettings(guildId);
            settings.Prefix = prefix;
            _settings[guildId] = settings;
            SaveSettingsAsync();
        }

        public ulong GetPrimaryChannel(ulong guildId)
        {
            return GetSettings(guildId).PrimaryChannel;
        }

        public async Task SetPrimaryChannelAsync(ulong guildId, ulong channelId)
        {
            var settings = GetSettings(guildId);
            settings.PrimaryChannel = channelId;
            _settings[guildId] = settings;
            await SaveSettingsAsync();
        }

        private ServerConfigs GetSettings(ulong guildId)
        {
            return _settings.GetValueOrDefault(guildId, _defaultSettings);
        }

        public void LoadSettings()
        {
            if (File.Exists(FILE_PATH))
            {
                var json = File.ReadAllText(FILE_PATH);
                var data = JsonConvert.DeserializeObject<Dictionary<ulong, ServerConfigs>>(json);

                if (data != null)
                {
                    foreach (var kvp in data)
                    {
                        _settings[kvp.Key] = kvp.Value;
                    }
                }
            }
        }

        public async Task SaveSettingsAsync()
        {
            var data = new Dictionary<ulong, ServerConfigs>(_settings);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            await File.WriteAllTextAsync(FILE_PATH, json);
        }

    }
}
