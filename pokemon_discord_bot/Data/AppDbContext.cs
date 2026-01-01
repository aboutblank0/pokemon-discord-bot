using Microsoft.EntityFrameworkCore;

namespace PokemonBot.Data;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        String? connectionUrl = Environment.GetEnvironmentVariable("POKEMON_DISCORD_BOT_DB_URL");
        options.UseNpgsql( connectionUrl );
    }
}
