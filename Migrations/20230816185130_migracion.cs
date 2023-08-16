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

            migrationBuilder.InsertData(
                table: "Villa",
                columns: new[] { "Id", "Creation", "Details", "Fee", "ImageUrl", "Name", "SizeMeters", "Tenants", "Update" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 8, 16, 15, 51, 29, 909, DateTimeKind.Local).AddTicks(7734), "La villa grande", 86m, "", "Villa número 1", 32, 10, new DateTime(2023, 8, 16, 15, 51, 29, 909, DateTimeKind.Local).AddTicks(7745) },
                    { 2, new DateTime(2023, 8, 16, 15, 51, 29, 909, DateTimeKind.Local).AddTicks(7747), "La villa mediana", 50m, "", "Villa número 2", 25, 7, new DateTime(2023, 8, 16, 15, 51, 29, 909, DateTimeKind.Local).AddTicks(7748) },
                    { 3, new DateTime(2023, 8, 16, 15, 51, 29, 909, DateTimeKind.Local).AddTicks(7750), "La villa pequeña", 28m, "", "Villa número 3", 18, 2, new DateTime(2023, 8, 16, 15, 51, 29, 909, DateTimeKind.Local).AddTicks(7750) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Villa");
        }
    }
}
