using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTypes", x => x.Id);
                },
                comment: "Типы активов");

            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                },
                comment: "Таблица бирж");

            migrationBuilder.CreateTable(
                name: "instrument_properties",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    data_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instrument_properties", x => x.Id);
                },
                comment: "Свойства инструментов");

            migrationBuilder.CreateTable(
                name: "InstrumentTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstrumentTypes", x => x.Id);
                },
                comment: "Типы инструментов");

            migrationBuilder.CreateTable(
                name: "timeframes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    value = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    unit = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timeframes", x => x.Id);
                },
                comment: "Таймфреймы");

            migrationBuilder.CreateTable(
                name: "assets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    AssetTypeId = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_assets_AssetTypes_AssetTypeId",
                        column: x => x.AssetTypeId,
                        principalTable: "AssetTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Активы");

            migrationBuilder.CreateTable(
                name: "PropertyPossibleValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false),
                    value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    numeric_value = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP(6)"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyPossibleValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyPossibleValues_instrument_properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "instrument_properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Возможные значения свойств");

            migrationBuilder.CreateTable(
                name: "trading_instruments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    code = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    BaseAssetId = table.Column<long>(type: "bigint", nullable: false),
                    QuoteAssetId = table.Column<long>(type: "bigint", nullable: false),
                    ExchangeId = table.Column<int>(type: "integer", nullable: false),
                    InstrumentTypeId = table.Column<int>(type: "integer", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trading_instruments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trading_instruments_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_trading_instruments_InstrumentTypes_InstrumentTypeId",
                        column: x => x.InstrumentTypeId,
                        principalTable: "InstrumentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_trading_instruments_assets_BaseAssetId",
                        column: x => x.BaseAssetId,
                        principalTable: "assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_trading_instruments_assets_QuoteAssetId",
                        column: x => x.QuoteAssetId,
                        principalTable: "assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Торговые инструменты");

            migrationBuilder.CreateTable(
                name: "instrument_property_values",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    InstrumentId = table.Column<long>(type: "bigint", nullable: false),
                    PropertyId = table.Column<long>(type: "bigint", nullable: false),
                    PossibleValueId = table.Column<long>(type: "bigint", nullable: true),
                    decimal_value = table.Column<decimal>(type: "numeric(18,8)", precision: 18, scale: 8, nullable: true),
                    integer_value = table.Column<int>(type: "integer", nullable: true),
                    string_value = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    boolean_value = table.Column<bool>(type: "boolean", nullable: true),
                    datetime_value = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instrument_property_values", x => x.Id);
                    table.ForeignKey(
                        name: "FK_instrument_property_values_PropertyPossibleValues_PossibleV~",
                        column: x => x.PossibleValueId,
                        principalTable: "PropertyPossibleValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_instrument_property_values_instrument_properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "instrument_properties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_instrument_property_values_trading_instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "trading_instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Значения свойств инструментов");

            migrationBuilder.CreateTable(
                name: "ohlcv_data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    InstrumentId = table.Column<long>(type: "bigint", nullable: false),
                    TimeframeId = table.Column<int>(type: "integer", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    open = table.Column<decimal>(type: "numeric(28,8)", precision: 28, scale: 8, nullable: false),
                    high = table.Column<decimal>(type: "numeric(28,8)", precision: 28, scale: 8, nullable: false),
                    low = table.Column<decimal>(type: "numeric(28,8)", precision: 28, scale: 8, nullable: false),
                    close = table.Column<decimal>(type: "numeric(28,8)", precision: 28, scale: 8, nullable: false),
                    volume = table.Column<decimal>(type: "numeric(36,18)", precision: 36, scale: 18, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ohlcv_data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ohlcv_data_timeframes_TimeframeId",
                        column: x => x.TimeframeId,
                        principalTable: "timeframes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ohlcv_data_trading_instruments_InstrumentId",
                        column: x => x.InstrumentId,
                        principalTable: "trading_instruments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "OHLCV данные");

            migrationBuilder.InsertData(
                table: "AssetTypes",
                columns: new[] { "Id", "code", "created_at", "description", "updated_at" },
                values: new object[,]
                {
                    { 1, "CRYPTO", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Cryptocurrency", null },
                    { 2, "FIAT", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Fiat currency", null },
                    { 3, "STOCK", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Stock", null }
                });

            migrationBuilder.InsertData(
                table: "InstrumentTypes",
                columns: new[] { "Id", "code", "created_at", "description", "updated_at" },
                values: new object[,]
                {
                    { 1, "SPOT", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Spot trading", null },
                    { 2, "PERPETUAL", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Perpetual futures", null },
                    { 3, "DELIVERY", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Delivery futures", null }
                });

            migrationBuilder.InsertData(
                table: "timeframes",
                columns: new[] { "Id", "created_at", "unit", "updated_at", "value" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "MINUTE", null, "1" },
                    { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "MINUTE", null, "5" },
                    { 3, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "MINUTE", null, "15" },
                    { 4, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "HOUR", null, "1" },
                    { 5, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "HOUR", null, "4" },
                    { 6, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DAY", null, "1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_assets_AssetTypeId",
                table: "assets",
                column: "AssetTypeId");

            migrationBuilder.CreateIndex(
                name: "ix_assets_code",
                table: "assets",
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
                name: "ix_instrument_properties_code",
                table: "instrument_properties",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_instrument_property_values_PossibleValueId",
                table: "instrument_property_values",
                column: "PossibleValueId");

            migrationBuilder.CreateIndex(
                name: "IX_instrument_property_values_PropertyId",
                table: "instrument_property_values",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "ux_instrument_property_values_unique",
                table: "instrument_property_values",
                columns: new[] { "InstrumentId", "PropertyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstrumentTypes_Code",
                table: "InstrumentTypes",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ohlcv_data_TimeframeId",
                table: "ohlcv_data",
                column: "TimeframeId");

            migrationBuilder.CreateIndex(
                name: "ix_ohlcv_data_timestamp",
                table: "ohlcv_data",
                column: "timestamp");

            migrationBuilder.CreateIndex(
                name: "ux_ohlcv_data_unique",
                table: "ohlcv_data",
                columns: new[] { "InstrumentId", "TimeframeId", "timestamp" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UX_PropertyPossibleValues_UniqueComposite",
                table: "PropertyPossibleValues",
                columns: new[] { "PropertyId", "value" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ux_timeframes_unique",
                table: "timeframes",
                columns: new[] { "value", "unit" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trading_instruments_active_exchange",
                table: "trading_instruments",
                columns: new[] { "is_active", "ExchangeId" });

            migrationBuilder.CreateIndex(
                name: "ix_trading_instruments_base_quote",
                table: "trading_instruments",
                columns: new[] { "BaseAssetId", "QuoteAssetId" });

            migrationBuilder.CreateIndex(
                name: "ix_trading_instruments_base_quote_instrument",
                table: "trading_instruments",
                columns: new[] { "BaseAssetId", "QuoteAssetId", "InstrumentTypeId" });

            migrationBuilder.CreateIndex(
                name: "ix_trading_instruments_code",
                table: "trading_instruments",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_trading_instruments_exchange_base_quote",
                table: "trading_instruments",
                columns: new[] { "ExchangeId", "BaseAssetId", "QuoteAssetId" });

            migrationBuilder.CreateIndex(
                name: "ix_trading_instruments_exchange_instrument",
                table: "trading_instruments",
                columns: new[] { "ExchangeId", "InstrumentTypeId" });

            migrationBuilder.CreateIndex(
                name: "ix_trading_instruments_exchange_instrument_quote",
                table: "trading_instruments",
                columns: new[] { "ExchangeId", "InstrumentTypeId", "QuoteAssetId" });

            migrationBuilder.CreateIndex(
                name: "IX_trading_instruments_InstrumentTypeId",
                table: "trading_instruments",
                column: "InstrumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_trading_instruments_QuoteAssetId",
                table: "trading_instruments",
                column: "QuoteAssetId");

            migrationBuilder.CreateIndex(
                name: "ux_trading_instruments_unique",
                table: "trading_instruments",
                columns: new[] { "BaseAssetId", "QuoteAssetId", "ExchangeId", "InstrumentTypeId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "instrument_property_values");

            migrationBuilder.DropTable(
                name: "ohlcv_data");

            migrationBuilder.DropTable(
                name: "PropertyPossibleValues");

            migrationBuilder.DropTable(
                name: "timeframes");

            migrationBuilder.DropTable(
                name: "trading_instruments");

            migrationBuilder.DropTable(
                name: "instrument_properties");

            migrationBuilder.DropTable(
                name: "Exchanges");

            migrationBuilder.DropTable(
                name: "InstrumentTypes");

            migrationBuilder.DropTable(
                name: "assets");

            migrationBuilder.DropTable(
                name: "AssetTypes");
        }
    }
}
