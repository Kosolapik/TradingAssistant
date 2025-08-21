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
                name: "AssetTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTypes", x => x.Id);
                },
                comment: "Типы активов")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                },
                comment: "Таблица бирж")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "InstrumentProperties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    data_type = table.Column<string>(type: "ENUM('Decimal','Integer','Boolean','String','DateTime')", nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentProperties", x => x.Id);
                },
                comment: "Свойства инструментов")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "InstrumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentTypes", x => x.Id);
                },
                comment: "Типы инструментов")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "Timeframes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    value = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    unit = table.Column<string>(type: "ENUM('TICK','SECOND','MINUTE','HOUR','DAY','WEEK','MONTH','YEAR')", nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeframes", x => x.Id);
                },
                comment: "Таймфреймы")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AssetTypeId = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AssetTypes_AssetTypeId",
                        column: x => x.AssetTypeId,
                        principalTable: "AssetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Активы")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "PropertyPossibleValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false),
                    value = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numeric_value = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyPossibleValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyPossibleValues_InstrumentProperties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "InstrumentProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Возможные значения свойств")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "TradingInstruments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    code = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BaseAssetId = table.Column<long>(type: "bigint", nullable: false),
                    QuoteAssetId = table.Column<long>(type: "bigint", nullable: false),
                    ExchangeId = table.Column<int>(type: "int", nullable: false),
                    InstrumentTypeId = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingInstruments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradingInstruments_Assets_BaseAssetId",
                        column: x => x.BaseAssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradingInstruments_Assets_QuoteAssetId",
                        column: x => x.QuoteAssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradingInstruments_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TradingInstruments_InstrumentTypes_InstrumentTypeId",
                        column: x => x.InstrumentTypeId,
                        principalTable: "InstrumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Торговые инструменты")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "InstrumentPropertyValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InstrumentId = table.Column<long>(type: "bigint", nullable: false),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false),
                    PossibleValueId = table.Column<long>(type: "bigint", nullable: true),
                    decimal_value = table.Column<decimal>(type: "decimal(18,8)", precision: 18, scale: 8, nullable: true),
                    integer_value = table.Column<int>(type: "int", nullable: true),
                    string_value = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb4_unicode_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    boolean_value = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    datetime_value = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentPropertyValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstrumentPropertyValues_InstrumentProperties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "InstrumentProperties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstrumentPropertyValues_PropertyPossibleValues_PossibleValu~",
                        column: x => x.PossibleValueId,
                        principalTable: "PropertyPossibleValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_InstrumentPropertyValues_TradingInstruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "TradingInstruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Значения свойств инструментов")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.CreateTable(
                name: "OHLCV",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InstrumentId = table.Column<long>(type: "bigint", nullable: false),
                    TimeframeId = table.Column<int>(type: "int", nullable: false),
                    timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    open = table.Column<decimal>(type: "decimal(28,8)", precision: 28, scale: 8, nullable: false),
                    high = table.Column<decimal>(type: "decimal(28,8)", precision: 28, scale: 8, nullable: false),
                    low = table.Column<decimal>(type: "decimal(28,8)", precision: 28, scale: 8, nullable: false),
                    close = table.Column<decimal>(type: "decimal(28,8)", precision: 28, scale: 8, nullable: false),
                    volume = table.Column<decimal>(type: "decimal(36,18)", precision: 36, scale: 18, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OHLCV", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OHLCV_Timeframes_TimeframeId",
                        column: x => x.TimeframeId,
                        principalTable: "Timeframes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OHLCV_TradingInstruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "TradingInstruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "OHLCV данные")
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.InsertData(
                table: "AssetTypes",
                columns: new[] { "Id", "code", "created_at", "description", "updated_at" },
                values: new object[,]
                {
                    { 1, "CRYPTO", new DateTime(2025, 8, 21, 16, 32, 21, 215, DateTimeKind.Utc).AddTicks(5174), "Cryptocurrency", null },
                    { 2, "FIAT", new DateTime(2025, 8, 21, 16, 32, 21, 215, DateTimeKind.Utc).AddTicks(5176), "Fiat currency", null },
                    { 3, "STOCK", new DateTime(2025, 8, 21, 16, 32, 21, 215, DateTimeKind.Utc).AddTicks(5178), "Stock", null }
                });

            migrationBuilder.InsertData(
                table: "InstrumentTypes",
                columns: new[] { "Id", "code", "created_at", "description", "updated_at" },
                values: new object[,]
                {
                    { 1, "SPOT", new DateTime(2025, 8, 21, 16, 32, 21, 215, DateTimeKind.Utc).AddTicks(5474), "Spot trading", null },
                    { 2, "PERPETUAL", new DateTime(2025, 8, 21, 16, 32, 21, 215, DateTimeKind.Utc).AddTicks(5476), "Perpetual futures", null },
                    { 3, "DELIVERY", new DateTime(2025, 8, 21, 16, 32, 21, 215, DateTimeKind.Utc).AddTicks(5478), "Delivery futures", null }
                });

            migrationBuilder.InsertData(
                table: "Timeframes",
                columns: new[] { "Id", "created_at", "unit", "updated_at", "value" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 21, 16, 32, 21, 215, DateTimeKind.Utc).AddTicks(5533), "MINUTE", null, "1" },
                    { 2, new DateTime(2025, 8, 21, 16, 32, 21, 215, DateTimeKind.Utc).AddTicks(5536), "MINUTE", null, "5" },
                    { 3, new DateTime(2025, 8, 21, 16, 32, 21, 215, DateTimeKind.Utc).AddTicks(5538), "MINUTE", null, "15" },
                    { 4, new DateTime(2025, 8, 21, 16, 32, 21, 215, DateTimeKind.Utc).AddTicks(5540), "HOUR", null, "1" },
                    { 5, new DateTime(2025, 8, 21, 16, 32, 21, 215, DateTimeKind.Utc).AddTicks(5542), "HOUR", null, "4" },
                    { 6, new DateTime(2025, 8, 21, 16, 32, 21, 215, DateTimeKind.Utc).AddTicks(5544), "DAY", null, "1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetTypeId",
                table: "Assets",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_Code",
                table: "Assets",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypes_Code",
                table: "AssetTypes",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_Code",
                table: "Exchanges",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentProperties_Code",
                table: "InstrumentProperties",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentPropertyValues_PossibleValueId",
                table: "InstrumentPropertyValues",
                column: "PossibleValueId");

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentPropertyValues_PropertyId",
                table: "InstrumentPropertyValues",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "UX_InstrumentPropertyValues_UniqueComposite",
                table: "InstrumentPropertyValues",
                columns: new[] { "InstrumentId", "PropertyId", "PossibleValueId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentTypes_Code",
                table: "InstrumentTypes",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OHLCV_TimeframeId",
                table: "OHLCV",
                column: "TimeframeId");

            migrationBuilder.CreateIndex(
                name: "UX_OHLCV_UniqueComposite",
                table: "OHLCV",
                columns: new[] { "InstrumentId", "TimeframeId", "timestamp" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_PropertyPossibleValues_UniqueComposite",
                table: "PropertyPossibleValues",
                columns: new[] { "PropertyId", "value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_Timeframes_UniqueComposite",
                table: "Timeframes",
                columns: new[] { "value", "unit" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TradingInstruments_BaseAssetId_QuoteAssetId",
                table: "TradingInstruments",
                columns: new[] { "BaseAssetId", "QuoteAssetId" });

            migrationBuilder.CreateIndex(
                name: "IX_TradingInstruments_BaseAssetId_QuoteAssetId_InstrumentTypeId",
                table: "TradingInstruments",
                columns: new[] { "BaseAssetId", "QuoteAssetId", "InstrumentTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_TradingInstruments_Code",
                table: "TradingInstruments",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "IX_TradingInstruments_ExchangeId_BaseAssetId_QuoteAssetId",
                table: "TradingInstruments",
                columns: new[] { "ExchangeId", "BaseAssetId", "QuoteAssetId" });

            migrationBuilder.CreateIndex(
                name: "IX_TradingInstruments_ExchangeId_InstrumentTypeId",
                table: "TradingInstruments",
                columns: new[] { "ExchangeId", "InstrumentTypeId" });

            migrationBuilder.CreateIndex(
                name: "IX_TradingInstruments_ExchangeId_InstrumentTypeId_QuoteAssetId",
                table: "TradingInstruments",
                columns: new[] { "ExchangeId", "InstrumentTypeId", "QuoteAssetId" });

            migrationBuilder.CreateIndex(
                name: "IX_TradingInstruments_InstrumentTypeId",
                table: "TradingInstruments",
                column: "InstrumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradingInstruments_IsActive_ExchangeId",
                table: "TradingInstruments",
                columns: new[] { "is_active", "ExchangeId" },
                filter: "is_active = 1");

            migrationBuilder.CreateIndex(
                name: "IX_TradingInstruments_QuoteAssetId",
                table: "TradingInstruments",
                column: "QuoteAssetId");

            migrationBuilder.CreateIndex(
                name: "UX_TradingInstruments_UniqueComposite",
                table: "TradingInstruments",
                columns: new[] { "BaseAssetId", "QuoteAssetId", "ExchangeId", "InstrumentTypeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstrumentPropertyValues");

            migrationBuilder.DropTable(
                name: "OHLCV");

            migrationBuilder.DropTable(
                name: "PropertyPossibleValues");

            migrationBuilder.DropTable(
                name: "Timeframes");

            migrationBuilder.DropTable(
                name: "TradingInstruments");

            migrationBuilder.DropTable(
                name: "InstrumentProperties");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DropTable(
                name: "InstrumentTypes");

            migrationBuilder.DropTable(
                name: "AssetTypes");
        }
    }
}
