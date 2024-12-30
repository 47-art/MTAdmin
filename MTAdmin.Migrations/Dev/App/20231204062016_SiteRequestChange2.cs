using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTAdmin.Migrations.Dev.App
{
    public partial class SiteRequestChange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "SiteRequests",
                newName: "StartTimestamp");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTimestamp",
                table: "SiteRequests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTimestamp",
                table: "SiteRequests");

            migrationBuilder.RenameColumn(
                name: "StartTimestamp",
                table: "SiteRequests",
                newName: "Timestamp");
        }
    }
}
