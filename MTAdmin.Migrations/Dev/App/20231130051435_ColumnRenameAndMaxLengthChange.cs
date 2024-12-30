using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTAdmin.Migrations.Dev.App
{
    public partial class ColumnRenameAndMaxLengthChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MetaTitle",
                table: "Templates",
                newName: "MetaDesc");

            migrationBuilder.RenameColumn(
                name: "MetaTitle",
                table: "Categories",
                newName: "MetaDesc");

            migrationBuilder.AlterColumn<string>(
                name: "IPAddress",
                table: "UserAudits",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "Contacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IPAddress",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "MetaDesc",
                table: "Templates",
                newName: "MetaTitle");

            migrationBuilder.RenameColumn(
                name: "MetaDesc",
                table: "Categories",
                newName: "MetaTitle");

            migrationBuilder.AlterColumn<string>(
                name: "IPAddress",
                table: "UserAudits",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
