using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IoT.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "IoT_OnlineTimeDaily",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "IoT_OnlineTimeDaily",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "IoT_OnlineTimeDaily",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "IoT_DeviceTag",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "IoT_DeviceTag",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "IoT_DeviceTag",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "IoT_OnlineTimeDaily");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "IoT_OnlineTimeDaily");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "IoT_OnlineTimeDaily");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "IoT_DeviceTag");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "IoT_DeviceTag");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "IoT_DeviceTag");
        }
    }
}
