using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace pokemon_discord_bot.Migrations
{
    /// <inheritdoc />
    public partial class AddPokeballsDailyReward : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "daily_reward_items",
                keyColumn: "id",
                keyValue: 1,
                column: "quantity",
                value: 10);

            migrationBuilder.InsertData(
                table: "daily_reward_items",
                columns: new[] { "id", "daily_reward_id", "item_id", "quantity" },
                values: new object[,]
                {
                    { 2, 1, 2, 10 },
                    { 3, 1, 3, 10 },
                    { 4, 1, 4, 10 }
                });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 1,
                columns: new[] { "attributes", "name" },
                values: new object[] { new Dictionary<string, object> { ["CatchRateMultiplier"] = 1f }, "Poke Ball" });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 2,
                column: "attributes",
                value: new Dictionary<string, object> { ["CatchRateMultiplier"] = 1.2f });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 3,
                column: "attributes",
                value: new Dictionary<string, object> { ["CatchRateMultiplier"] = 1.5f });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 4,
                columns: new[] { "attributes", "name" },
                values: new object[] { new Dictionary<string, object> { ["GuaranteedCatch"] = true }, "Master Ball" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "daily_reward_items",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "daily_reward_items",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "daily_reward_items",
                keyColumn: "id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "daily_reward_items",
                keyColumn: "id",
                keyValue: 1,
                column: "quantity",
                value: 5);

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 1,
                columns: new[] { "attributes", "name" },
                values: new object[] { new Dictionary<string, object> { ["CatchRateMultiplier"] = 1f }, "Pokeball" });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 2,
                column: "attributes",
                value: new Dictionary<string, object> { ["CatchRateMultiplier"] = 1.2f });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 3,
                column: "attributes",
                value: new Dictionary<string, object> { ["CatchRateMultiplier"] = 1.5f });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 4,
                columns: new[] { "attributes", "name" },
                values: new object[] { new Dictionary<string, object> { ["CatchRateMultiplier"] = 1f }, "Love Ball" });

            migrationBuilder.InsertData(
                table: "items",
                columns: new[] { "item_id", "attributes", "drop_chance", "name", "tradeable" },
                values: new object[] { 5, new Dictionary<string, object> { ["GuaranteedCatch"] = true }, 0.1f, "Master Ball", true });
        }
    }
}
