using Discord.Commands;
using Microsoft.EntityFrameworkCore;
using pokemon_discord_bot.Services;
using PokemonBot.Data;
namespace pokemon_discord_bot.Modules
{
    public class DailyRewardModule : ModuleBase<SocketCommandContext>
    {
        private readonly AppDbContext _dbContext; 
        private readonly DailyRewardService _dailyRewardService;

        public DailyRewardModule(AppDbContext dbContext, DailyRewardService dailyRewardService)
        {
            _dbContext = dbContext;
            _dailyRewardService = dailyRewardService;
        }

        [Command("daily")]
        public async Task DailyAsync()
        {
            // Check if the user has already claimed their daily reward
            var canClaim = await _dailyRewardService.CanClaimReward(Context.User.Id, _dbContext);

            if (canClaim)
            {
                try
                {
                    await _dailyRewardService.ClaimDailyReward(Context.User.Id, _dbContext);
                } 
                catch (Exception ex) {

                    await ReplyAsync($"{Context.User.Mention} There was an error claiming your daily reward.");
                }
            }
            else
            {
                await ReplyAsync($"{Context.User.Mention} You have already claimed your daily reward today. Try again tomorrow.");
            }
        }

        //TODO: MOVE TO SEPARATE MODULE
        [Command("inv")]
        public async Task InventoryAsync()
        {
            var userId = Context.User.Id;

            // Fetch the user's items from the database
            var userItems = await _dbContext.PlayerInventory
                .Include(ui => ui.Item)
                .Where(ui => ui.PlayerId == userId)
                .ToListAsync();
        }
    }
}
