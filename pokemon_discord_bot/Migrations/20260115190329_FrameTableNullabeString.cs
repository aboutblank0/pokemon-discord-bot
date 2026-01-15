using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace pokemon_discord_bot.Migrations
{
    /// <inheritdoc />
    public partial class FrameTableNullabeString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "frames",
                columns: new[] { "frame_id", "cost", "img", "name", "tradeable" },
                values: new object[,]
                {
                    { 1, 0, "assets/frames/default_frame.png", "Default Frame", true },
                    { 2, 0, "assets/frames/pokemon_frame.png", "Pokemon Frame", true }
                });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 1,
                column: "attributes",
                value: new Dictionary<string, object> { ["CatchRateMultiplier"] = 1f });

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
                column: "attributes",
                value: new Dictionary<string, object> { ["CatchRateMultiplier"] = 1f });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 5,
                column: "attributes",
                value: new Dictionary<string, object> { ["GuaranteedCatch"] = true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "frames",
                keyColumn: "frame_id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "frames",
                keyColumn: "frame_id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 1,
                column: "attributes",
                value: new Dictionary<string, object> { ["CatchRateMultiplier"] = 1f });

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
                column: "attributes",
                value: new Dictionary<string, object> { ["CatchRateMultiplier"] = 1f });

            migrationBuilder.UpdateData(
                table: "items",
                keyColumn: "item_id",
                keyValue: 5,
                column: "attributes",
                value: new Dictionary<string, object> { ["GuaranteedCatch"] = true });
        }
    }
}
