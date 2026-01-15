using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace pokemon_discord_bot.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnOwnedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "owned_at",
                table: "pokemons",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

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
                name: "owned_at",
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
