using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MTAdmin.Migrations.Dev.App
{
    public partial class SiteAnalyticsTablesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OfficialName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Alpha2Code = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    Alpha3Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    CountryCode = table.Column<int>(type: "int", nullable: false),
                    TopLevelDomain = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeoIpRanges",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromUp = table.Column<long>(type: "bigint", nullable: false),
                    FromDown = table.Column<long>(type: "bigint", nullable: false),
                    ToUp = table.Column<long>(type: "bigint", nullable: false),
                    ToDown = table.Column<long>(type: "bigint", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeoIpRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeoIpRanges_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SiteRequests",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Identity = table.Column<string>(type: "nvarchar(455)", maxLength: 455, nullable: true),
                    RemoteIpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Method = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    UserAgent = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Referer = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    IsWebSocket = table.Column<bool>(type: "bit", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SiteRequests_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GeoIpRanges_CountryId",
                table: "GeoIpRanges",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_SiteRequests_CountryId",
                table: "SiteRequests",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeoIpRanges");

            migrationBuilder.DropTable(
                name: "SiteRequests");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
