using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestTaks.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AggreggationId = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTimeOffset>(nullable: false),
                    Data = table.Column<string>(nullable: true),
                    User = table.Column<string>(nullable: true),
                    EventClass = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Eventos_AggreggationId",
                table: "Eventos",
                column: "AggreggationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Eventos");
        }
    }
}
