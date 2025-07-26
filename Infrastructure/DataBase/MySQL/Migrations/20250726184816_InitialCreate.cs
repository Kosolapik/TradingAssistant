using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TradingAssistant.Infrastructure.DataBase.MySQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "MarketTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "Timeframes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeframes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "Symbols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BaseAsset = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    QuoteAsset = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ExchangeId = table.Column<int>(type: "int", nullable: false),
                    MarketTypeId = table.Column<int>(type: "int", nullable: false),
                    MinTradeQuantity = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: false),
                    MinNotionalValue = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: false),
                    MaxTradeQuantity = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: true),
                    QuantityStep = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: false),
                    PriceStep = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: false),
                    QuantityDecimals = table.Column<int>(type: "int", nullable: true),
                    PriceDecimals = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ContractSize = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: true),
                    DeliveryTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    MaxShortLeverage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    MaxLongLeverage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Symbols", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Symbols_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Symbols_MarketTypes_MarketTypeId",
                        column: x => x.MarketTypeId,
                        principalTable: "MarketTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "OhlcvData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SymbolId = table.Column<int>(type: "int", nullable: false),
                    TimeframeId = table.Column<int>(type: "int", nullable: false),
                    Open = table.Column<double>(type: "double", precision: 18, scale: 8, nullable: false),
                    High = table.Column<double>(type: "double", precision: 18, scale: 8, nullable: false),
                    Low = table.Column<double>(type: "double", precision: 18, scale: 8, nullable: false),
                    Close = table.Column<double>(type: "double", precision: 18, scale: 8, nullable: false),
                    Volume = table.Column<double>(type: "double", precision: 18, scale: 8, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OhlcvData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OhlcvData_Symbols_SymbolId",
                        column: x => x.SymbolId,
                        principalTable: "Symbols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OhlcvData_Timeframes_TimeframeId",
                        column: x => x.TimeframeId,
                        principalTable: "Timeframes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.InsertData(
                table: "MarketTypes",
                columns: new[] { "Id", "Description", "Type" },
                values: new object[,]
                {
                    { 1, "Spot trading", "Spot" },
                    { 2, "Linear perpetual contracts", "PerpetualLinear" },
                    { 3, "Linear delivery contracts", "DeliveryLinear" },
                    { 4, "Inverse perpetual contracts", "PerpetualInverse" },
                    { 5, "Inverse delivery contracts", "DeliveryInverse" }
                });

            migrationBuilder.InsertData(
                table: "Timeframes",
                columns: new[] { "Id", "Unit", "Value" },
                values: new object[,]
                {
                    { 1, "minute", 1 },
                    { 2, "minute", 5 },
                    { 3, "minute", 15 },
                    { 4, "hour", 1 },
                    { 5, "hour", 4 },
                    { 6, "day", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_Name",
                table: "Exchanges",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MarketTypes_Type",
                table: "MarketTypes",
                column: "Type",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OhlcvData_SymbolId_TimeframeId_Timestamp",
                table: "OhlcvData",
                columns: new[] { "SymbolId", "TimeframeId", "Timestamp" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OhlcvData_TimeframeId",
                table: "OhlcvData",
                column: "TimeframeId");

            migrationBuilder.CreateIndex(
                name: "IX_Symbols_ExchangeId",
                table: "Symbols",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Symbols_MarketTypeId",
                table: "Symbols",
                column: "MarketTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Symbols_Name_ExchangeId",
                table: "Symbols",
                columns: new[] { "Name", "ExchangeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Timeframes_Value_Unit",
                table: "Timeframes",
                columns: new[] { "Value", "Unit" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OhlcvData");

            migrationBuilder.DropTable(
                name: "Symbols");

            migrationBuilder.DropTable(
                name: "Timeframes");

            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DropTable(
                name: "MarketTypes");
        }
    }
}
