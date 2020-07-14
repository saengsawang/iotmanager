using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Region",
                table: "Region");

            migrationBuilder.RenameTable(
                name: "Region",
                newName: "IoT_Region");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IoT_Region",
                table: "IoT_Region",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_IoT_Region",
                table: "IoT_Region");

            migrationBuilder.RenameTable(
                name: "IoT_Region",
                newName: "Region");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Region",
                table: "Region",
                column: "Id");
        }
    }
}
