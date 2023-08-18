using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_testing2.Migrations
{
    /// <inheritdoc />
    public partial class migracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Villa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenants = table.Column<int>(type: "int", nullable: true),
                    SizeMeters = table.Column<int>(type: "int", nullable: true),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NumeroVilla",
                columns: table => new
                {
                    VillaNro = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Update = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroVilla", x => x.VillaNro);
                    table.ForeignKey(
                        name: "FK_NumeroVilla_Villa_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Villa",
                columns: new[] { "Id", "Creation", "Details", "Fee", "ImageUrl", "Name", "SizeMeters", "Tenants", "Update" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 18, 1, 26, 46, 941, DateTimeKind.Local).AddTicks(1217), "La villa grande", 86m, "", "Villa número 1", 32, 10, new DateTime(2023, 8, 18, 1, 26, 46, 941, DateTimeKind.Local).AddTicks(1229) },
                    { 2, new DateTime(2023, 8, 18, 1, 26, 46, 941, DateTimeKind.Local).AddTicks(1233), "La villa mediana", 50m, "", "Villa número 2", 25, 7, new DateTime(2023, 8, 18, 1, 26, 46, 941, DateTimeKind.Local).AddTicks(1234) },
                    { 3, new DateTime(2023, 8, 18, 1, 26, 46, 941, DateTimeKind.Local).AddTicks(1237), "La villa pequeña", 28m, "", "Villa número 3", 18, 2, new DateTime(2023, 8, 18, 1, 26, 46, 941, DateTimeKind.Local).AddTicks(1237) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroVilla_VillaId",
                table: "NumeroVilla",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroVilla");

            migrationBuilder.DropTable(
                name: "Villa");
        }
    }
}
