using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MerkezBankasıDövizVerileri.Data.Migrations
{
    public partial class Dvz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AUD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EUR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GBP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entities");
        }
    }
}
