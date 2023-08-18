using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_testing2.Migrations
{
    /// <inheritdoc />
    public partial class numerovilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Creation", "Update" },
                values: new object[] { new DateTime(2023, 8, 17, 19, 48, 56, 742, DateTimeKind.Local).AddTicks(6720), new DateTime(2023, 8, 17, 19, 48, 56, 742, DateTimeKind.Local).AddTicks(6736) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Creation", "Update" },
                values: new object[] { new DateTime(2023, 8, 17, 19, 48, 56, 742, DateTimeKind.Local).AddTicks(6740), new DateTime(2023, 8, 17, 19, 48, 56, 742, DateTimeKind.Local).AddTicks(6741) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Creation", "Update" },
                values: new object[] { new DateTime(2023, 8, 17, 19, 48, 56, 742, DateTimeKind.Local).AddTicks(6743), new DateTime(2023, 8, 17, 19, 48, 56, 742, DateTimeKind.Local).AddTicks(6744) });

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

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Creation", "Update" },
                values: new object[] { new DateTime(2023, 8, 16, 15, 51, 29, 909, DateTimeKind.Local).AddTicks(7734), new DateTime(2023, 8, 16, 15, 51, 29, 909, DateTimeKind.Local).AddTicks(7745) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Creation", "Update" },
                values: new object[] { new DateTime(2023, 8, 16, 15, 51, 29, 909, DateTimeKind.Local).AddTicks(7747), new DateTime(2023, 8, 16, 15, 51, 29, 909, DateTimeKind.Local).AddTicks(7748) });

            migrationBuilder.UpdateData(
                table: "Villa",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Creation", "Update" },
                values: new object[] { new DateTime(2023, 8, 16, 15, 51, 29, 909, DateTimeKind.Local).AddTicks(7750), new DateTime(2023, 8, 16, 15, 51, 29, 909, DateTimeKind.Local).AddTicks(7750) });
        }
    }
}
