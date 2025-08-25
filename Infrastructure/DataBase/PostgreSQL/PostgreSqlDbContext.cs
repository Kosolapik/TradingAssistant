using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using System.IO;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL;

public class PostgreSqlDbContext : DbContext
{
    public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options) { }

    // Таблицы биржевой структуры
    public DbSet<Exchange> Exchanges { get; set; }
    public DbSet<AssetType> AssetTypes { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<InstrumentType> InstrumentTypes { get; set; }
    public DbSet<TradingInstrument> TradingInstruments { get; set; }
    public DbSet<InstrumentProperty> InstrumentProperties { get; set; }
    public DbSet<PropertyPossibleValue> PropertyPossibleValues { get; set; }
    public DbSet<InstrumentPropertyValue> InstrumentPropertyValues { get; set; }
    public DbSet<Timeframe> Timeframes { get; set; }
    public DbSet<OhlcvData> OhlcvData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Только для DesignTime (миграций)
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = Config.ConnectionStringPostgreSQL();
            optionsBuilder.UseNpgsql(connectionString, npgsqlOptions =>
            {
                npgsqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorCodesToAdd: null // Добавляем третий параметр
                );
            });
        }

        // Отключаем предупреждение о динамических значениях
        optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Регистрируем enum тип в PostgreSQL
        modelBuilder.HasPostgresEnum<TimeframeUnit>();
        modelBuilder.HasPostgresEnum<PropertyDataType>();

        // Для PostgreSQL используем стандартную кодировку
        modelBuilder.UseCollation("en_US.utf8");

        // Автоматическое применение всех конфигураций
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlDbContext).Assembly);

        // Seed данные
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Фиксированные даты для seed данных
        var seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<AssetType>().HasData(
            new AssetType { Id = 1, Code = "CRYPTO", Description = "Cryptocurrency", CreatedAt = seedDate },
            new AssetType { Id = 2, Code = "FIAT", Description = "Fiat currency", CreatedAt = seedDate },
            new AssetType { Id = 3, Code = "STOCK", Description = "Stock", CreatedAt = seedDate }
        );

        modelBuilder.Entity<InstrumentType>().HasData(
            new InstrumentType { Id = 1, Code = "SPOT", Description = "Spot trading", CreatedAt = seedDate },
            new InstrumentType { Id = 2, Code = "PERPETUAL", Description = "Perpetual futures", CreatedAt = seedDate },
            new InstrumentType { Id = 3, Code = "DELIVERY", Description = "Delivery futures", CreatedAt = seedDate }
        );

        modelBuilder.Entity<Timeframe>().HasData(
            new Timeframe { Id = 1, Value = "1", Unit = TimeframeUnit.Minute, CreatedAt = seedDate },
            new Timeframe { Id = 2, Value = "5", Unit = TimeframeUnit.Minute, CreatedAt = seedDate },
            new Timeframe { Id = 3, Value = "15", Unit = TimeframeUnit.Minute, CreatedAt = seedDate },
            new Timeframe { Id = 4, Value = "1", Unit = TimeframeUnit.Hour, CreatedAt = seedDate },
            new Timeframe { Id = 5, Value = "4", Unit = TimeframeUnit.Hour, CreatedAt = seedDate },
            new Timeframe { Id = 6, Value = "1", Unit = TimeframeUnit.Day, CreatedAt = seedDate }
        );
    }
}