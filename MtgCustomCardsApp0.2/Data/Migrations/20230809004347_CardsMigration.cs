using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MtgCustomCardsApp0._2.Data.Migrations
{
    /// <inheritdoc />
    public partial class CardsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardData",
                columns: table => new
                {
                    CardId = table.Column<uint>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<uint>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CardImg = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    SubType = table.Column<string>(type: "TEXT", nullable: false),
                    CardText = table.Column<string>(type: "TEXT", nullable: false),
                    CardFlavorText = table.Column<string>(type: "TEXT", nullable: false),
                    Illustrator = table.Column<string>(type: "TEXT", nullable: false),
                    Rarity = table.Column<char>(type: "TEXT", nullable: false),
                    Power = table.Column<string>(type: "TEXT", nullable: false),
                    Toughness = table.Column<string>(type: "TEXT", nullable: false),
                    IsLegendary = table.Column<bool>(type: "INTEGER", nullable: false),
                    CardManaCostString = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "ManaCost",
                columns: table => new
                {
                    CardId = table.Column<uint>(type: "INTEGER", nullable: false),
                    Colorless = table.Column<uint>(type: "INTEGER", nullable: false),
                    White = table.Column<uint>(type: "INTEGER", nullable: false),
                    Blue = table.Column<uint>(type: "INTEGER", nullable: false),
                    Black = table.Column<uint>(type: "INTEGER", nullable: false),
                    Red = table.Column<uint>(type: "INTEGER", nullable: false),
                    Green = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManaCost", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_ManaCost_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "CardData",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ManaCost");

            migrationBuilder.DropTable(
                name: "CardData");
        }
    }
}