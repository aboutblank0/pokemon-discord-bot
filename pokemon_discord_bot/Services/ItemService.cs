using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using pokemon_discord_bot.Data;

namespace pokemon_discord_bot.Services
{
    public class ItemService
    {

        public async Task<List<Item>> GetAllPokeballsSortedCache(AppDbContext db, IMemoryCache cache)
        {
            string cacheKey = "AllPokeballs";

            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromDays(7);
                return await db.Items
                    .Where(i => EF.Functions.Like(i.Name, "%Ball%"))
                    .OrderBy(i => i.ItemId)
                    .ToListAsync();
            }) ?? new List<Item>();
        }


    }
}
