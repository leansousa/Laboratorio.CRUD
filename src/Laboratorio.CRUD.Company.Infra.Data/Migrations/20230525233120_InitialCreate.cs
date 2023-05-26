using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Laboratorio.CRUD.Company.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SizeCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "VARCHAR(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Companies_SizeCompany_SizeId",
                        column: x => x.SizeId,
                        principalTable: "SizeCompany",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "SizeCompany",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Pequena" },
                    { 2, "Média" },
                    { 3, "Grande" }
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name", "SizeId" },
                values: new object[,]
                {
                    { 1, "Empresa 01", 1 },
                    { 2, "Empresa 02", 2 },
                    { 3, "Empresa 03", 1 },
                    { 4, "Empresa 04", 1 },
                    { 5, "Empresa 05", 2 },
                    { 6, "Empresa 06", 2 },
                    { 7, "Empresa 07", 1 },
                    { 8, "Empresa 08", 1 },
                    { 9, "Empresa 09", 2 },
                    { 10, "Empresa 10", 1 },
                    { 11, "Empresa 11", 2 },
                    { 12, "Empresa 12", 1 },
                    { 13, "Empresa 13", 2 },
                    { 14, "Empresa 14", 2 },
                    { 15, "Empresa 15", 1 },
                    { 16, "Empresa 16", 1 },
                    { 17, "Empresa 17", 2 },
                    { 18, "Empresa 18", 1 },
                    { 19, "Empresa 19", 1 },
                    { 20, "Empresa 20", 2 },
                    { 21, "Empresa 21", 2 },
                    { 22, "Empresa 22", 2 },
                    { 23, "Empresa 23", 2 },
                    { 24, "Empresa 24", 2 },
                    { 25, "Empresa 25", 2 },
                    { 26, "Empresa 26", 1 },
                    { 27, "Empresa 27", 1 },
                    { 28, "Empresa 28", 2 },
                    { 29, "Empresa 29", 1 },
                    { 30, "Empresa 30", 1 },
                    { 31, "Empresa 31", 2 },
                    { 32, "Empresa 32", 1 },
                    { 33, "Empresa 33", 1 },
                    { 34, "Empresa 34", 2 },
                    { 35, "Empresa 35", 2 },
                    { 36, "Empresa 36", 1 },
                    { 37, "Empresa 37", 1 },
                    { 38, "Empresa 38", 2 },
                    { 39, "Empresa 39", 1 },
                    { 40, "Empresa 40", 2 },
                    { 41, "Empresa 41", 2 },
                    { 42, "Empresa 42", 1 },
                    { 43, "Empresa 43", 2 },
                    { 44, "Empresa 44", 2 },
                    { 45, "Empresa 45", 1 },
                    { 46, "Empresa 46", 1 },
                    { 47, "Empresa 47", 2 },
                    { 48, "Empresa 48", 2 },
                    { 49, "Empresa 49", 1 },
                    { 50, "Empresa 50", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_SizeId",
                table: "Companies",
                column: "SizeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "SizeCompany");
        }
    }
}