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
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenants = table.Column<int>(type: "int", nullable: true),
                    SizeMeters = table.Column<int>(type: "int", nullable: true),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
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
                    { 1L, new DateTime(2023, 8, 13, 19, 20, 47, 164, DateTimeKind.Local).AddTicks(4453), "La Villa Real 1 es grande y linda", 200m, "", "Villa Real 1", 50, 5, new DateTime(2023, 8, 13, 19, 20, 47, 164, DateTimeKind.Local).AddTicks(4466) },
                    { 2L, new DateTime(2023, 8, 13, 19, 20, 47, 164, DateTimeKind.Local).AddTicks(4469), "La Villa Real 2 es chica", 100m, "", "Villa Real 2", 23, 2, new DateTime(2023, 8, 13, 19, 20, 47, 164, DateTimeKind.Local).AddTicks(4470) }
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
