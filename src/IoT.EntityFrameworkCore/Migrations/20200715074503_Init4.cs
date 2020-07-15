using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddPrimaryKey(
                name: "PK_IoT_Region",
                table: "IoT_Region",
                column: "Id");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
