using Microsoft.EntityFrameworkCore;
using TradingAssistant.Core.Entities.Exchanges;
using TradingAssistant.Infrastructure.DataBase.MySQL.Configurations;

namespace TradingAssistant.Infrastructure.DataBase.MySQL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Установка кодировки и collation для всей БД
        modelBuilder.HasCharSet("utf8mb4").UseCollation("utf8mb4_unicode_ci");

        // ========== КОНФИГУРАЦИЯ ОСНОВНЫХ СУЩНОСТЕЙ ==========
        ApplyConfigurations(modelBuilder);

        // ========== SEED ДАННЫЕ ==========
        SeedData(modelBuilder);
    }

    private void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        // Автоматическое применение всех конфигураций из сборки
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed данных для тестирования
        modelBuilder.Entity<AssetType>().HasData(
            new AssetType { Id = 1, Code = "CRYPTO", Description = "Cryptocurrency", CreatedAt = DateTime.UtcNow },
            new AssetType { Id = 2, Code = "FIAT", Description = "Fiat currency", CreatedAt = DateTime.UtcNow },
            new AssetType { Id = 3, Code = "STOCK", Description = "Stock", CreatedAt = DateTime.UtcNow }
        );

        modelBuilder.Entity<InstrumentType>().HasData(
            new InstrumentType { Id = 1, Code = "SPOT", Description = "Spot trading", CreatedAt = DateTime.UtcNow },
            new InstrumentType { Id = 2, Code = "PERPETUAL", Description = "Perpetual futures", CreatedAt = DateTime.UtcNow },
            new InstrumentType { Id = 3, Code = "DELIVERY", Description = "Delivery futures", CreatedAt = DateTime.UtcNow }
        );

        modelBuilder.Entity<Timeframe>().HasData(
            new Timeframe { Id = 1, Value = "1", Unit = TimeframeUnit.MINUTE, CreatedAt = DateTime.UtcNow },
            new Timeframe { Id = 2, Value = "5", Unit = TimeframeUnit.MINUTE, CreatedAt = DateTime.UtcNow },
            new Timeframe { Id = 3, Value = "15", Unit = TimeframeUnit.MINUTE, CreatedAt = DateTime.UtcNow },
            new Timeframe { Id = 4, Value = "1", Unit = TimeframeUnit.HOUR, CreatedAt = DateTime.UtcNow },
            new Timeframe { Id = 5, Value = "4", Unit = TimeframeUnit.HOUR, CreatedAt = DateTime.UtcNow },
            new Timeframe { Id = 6, Value = "1", Unit = TimeframeUnit.DAY, CreatedAt = DateTime.UtcNow }
        );
    }
}