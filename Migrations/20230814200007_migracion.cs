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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    { 1, new DateTime(2023, 8, 14, 17, 0, 7, 6, DateTimeKind.Local).AddTicks(3522), "", 10m, "", "Villa 1", 0, 0, new DateTime(2023, 8, 14, 17, 0, 7, 6, DateTimeKind.Local).AddTicks(3532) },
                    { 2, new DateTime(2023, 8, 14, 17, 0, 7, 6, DateTimeKind.Local).AddTicks(3535), "", 20m, "", "Villa 2", 0, 0, new DateTime(2023, 8, 14, 17, 0, 7, 6, DateTimeKind.Local).AddTicks(3536) }
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
