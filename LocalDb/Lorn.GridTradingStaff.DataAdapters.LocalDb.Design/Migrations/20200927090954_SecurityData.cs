using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lorn.GridTradingStaff.DataAdapters.LocalDb.Migrations
{
    public partial class SecurityData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChinaStocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    UpdateTime = table.Column<DateTimeOffset>(nullable: false),
                    Code = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TradingMethod = table.Column<int>(nullable: false),
                    SecurityType = table.Column<int>(nullable: false),
                    Listdate = table.Column<DateTime>(nullable: false),
                    ListStatus = table.Column<int>(nullable: false),
                    Exchange = table.Column<int>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    Industry = table.Column<string>(nullable: true),
                    Market = table.Column<int>(nullable: false),
                    Is_hs = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChinaStocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SecurityProviderInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    UpdateTime = table.Column<DateTimeOffset>(nullable: false),
                    Provider = table.Column<int>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: true),
                    SecurityId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityProviderInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradeCalendars",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    UpdateTime = table.Column<DateTimeOffset>(nullable: false),
                    Exchange = table.Column<int>(nullable: false),
                    CalendarDate = table.Column<DateTime>(nullable: false),
                    IsOpen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeCalendars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChinaStocks");

            migrationBuilder.DropTable(
                name: "SecurityProviderInfos");

            migrationBuilder.DropTable(
                name: "TradeCalendars");
        }
    }
}
