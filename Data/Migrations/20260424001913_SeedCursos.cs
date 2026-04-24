using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PortalAcademico.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedCursos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Activo", "Codigo", "Creditos", "CupoMaximo", "HorarioFin", "HorarioInicio", "Nombre" },
                values: new object[,]
                {
                    { 1, true, "CS101", 3, 30, new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), "Programación" },
                    { 2, true, "MAT101", 4, 25, new DateTime(2024, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Matemática" },
                    { 3, true, "FIS101", 3, 20, new DateTime(2024, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), "Física" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
