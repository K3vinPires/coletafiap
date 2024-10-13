using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ColetaApi.Migrations
{
    /// <inheritdoc />
    public partial class _01Coleta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COLETA",
                columns: table => new
                {
                    Pk_id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Ds_coleta = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Ds_tipocoleta = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Dt_coleta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COLETA", x => x.Pk_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_COLETA_Ds_coleta",
                table: "COLETA",
                column: "Ds_coleta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COLETA");
        }
    }
}
