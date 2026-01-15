using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pokemon_discord_bot.Migrations
{
    /// <inheritdoc />
    public partial class PokemonAddFrameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "frame",
                table: "pokemons",
                type: "text",
                nullable: false,
                defaultValue: "");

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
            migrationBuilder.DropColumn(
                name: "frame",
                table: "pokemons");

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
