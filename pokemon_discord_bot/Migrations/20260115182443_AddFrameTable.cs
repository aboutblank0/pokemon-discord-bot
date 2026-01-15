using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace pokemon_discord_bot.Migrations
{
    /// <inheritdoc />
    public partial class AddFrameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "frame",
                table: "pokemons");

            migrationBuilder.AddColumn<int>(
                name: "frame_id",
                table: "pokemons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "frame_id",
                table: "player_inventory",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "frames",
                columns: table => new
                {
                    frame_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    cost = table.Column<int>(type: "integer", nullable: false),
                    tradeable = table.Column<bool>(type: "boolean", nullable: false),
                    img = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_frames", x => x.frame_id);
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

            migrationBuilder.CreateIndex(
                name: "IX_pokemons_frame_id",
                table: "pokemons",
                column: "frame_id");

            migrationBuilder.CreateIndex(
                name: "IX_player_inventory_frame_id",
                table: "player_inventory",
                column: "frame_id");

            migrationBuilder.AddForeignKey(
                name: "FK_player_inventory_frames_frame_id",
                table: "player_inventory",
                column: "frame_id",
                principalTable: "frames",
                principalColumn: "frame_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pokemons_frames_frame_id",
                table: "pokemons",
                column: "frame_id",
                principalTable: "frames",
                principalColumn: "frame_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_player_inventory_frames_frame_id",
                table: "player_inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_pokemons_frames_frame_id",
                table: "pokemons");

            migrationBuilder.DropTable(
                name: "frames");

            migrationBuilder.DropIndex(
                name: "IX_pokemons_frame_id",
                table: "pokemons");

            migrationBuilder.DropIndex(
                name: "IX_player_inventory_frame_id",
                table: "player_inventory");

            migrationBuilder.DropColumn(
                name: "frame_id",
                table: "pokemons");

            migrationBuilder.DropColumn(
                name: "frame_id",
                table: "player_inventory");

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
    }
}
