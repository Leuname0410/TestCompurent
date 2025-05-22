using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestCompurent.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlbumsSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumsSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direction = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "MusicRadio")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Client_Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Album_Id = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SongsSet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Album_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongsSet", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AlbumsSet",
                columns: new[] { "Id", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Steal this album", 9.99f },
                    { 2, "Toxixity", 14.99f }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Direction", "Mail", "Name", "Password", "Phone" },
                values: new object[] { "admin01", "Calle Falsa 123", "admin@example.com", "Administrador", "MusicRadioAdmin", "1234567890" });

            migrationBuilder.InsertData(
                table: "PurchaseDetails",
                columns: new[] { "Id", "Album_Id", "Client_Id", "PurchaseDate", "Total" },
                values: new object[,]
                {
                    { 1, 1, "admin01", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 9.99f },
                    { 2, 2, "admin01", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 14.99f }
                });

            migrationBuilder.InsertData(
                table: "SongsSet",
                columns: new[] { "Id", "Album_Id", "Name" },
                values: new object[,]
                {
                    { 1, 2, "Chop Suey" },
                    { 2, 1, "Bounce" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumsSet");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "PurchaseDetails");

            migrationBuilder.DropTable(
                name: "SongsSet");
        }
    }
}
