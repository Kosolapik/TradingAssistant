using Microsoft.EntityFrameworkCore;
using TradingAssistant.Core.Entities;

namespace TradingAssistant.Infrastructure.DataBase.MySQL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Exchange> Exchanges { get; set; }
    public DbSet<Symbol> Symbols { get; set; }
    public DbSet<MarketType> MarketTypes { get; set; }
    public DbSet<Timeframe> Timeframes { get; set; }
    public DbSet<OhlcvData> OhlcvData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Установка кодировки и collation для всей БД
        modelBuilder.HasCharSet("utf8mb4")
                   .UseCollation("utf8mb4_unicode_ci");

        // Конфигурация Exchange
        modelBuilder.Entity<Exchange>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.Property(e => e.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

            entity.HasIndex(e => e.Name)
                  .IsUnique();
        });

        // Конфигурация MarketType
        modelBuilder.Entity<MarketType>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).ValueGeneratedOnAdd();

            entity.Property(m => m.Type)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(m => m.Description)
                  .HasMaxLength(255);

            entity.HasIndex(m => m.Type)
                  .IsUnique();

            // Seed данные
            entity.HasData(
                new MarketType { Id = 1, Type = "Spot", Description = "Spot trading" },
                new MarketType { Id = 2, Type = "PerpetualLinear", Description = "Linear perpetual contracts" },
                new MarketType { Id = 3, Type = "DeliveryLinear", Description = "Linear delivery contracts" },
                new MarketType { Id = 4, Type = "PerpetualInverse", Description = "Inverse perpetual contracts" },
                new MarketType { Id = 5, Type = "DeliveryInverse", Description = "Inverse delivery contracts" }
            );
        });

        // Конфигурация Timeframe
        modelBuilder.Entity<Timeframe>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Id).ValueGeneratedOnAdd();

            entity.Property(t => t.Unit)
                  .IsRequired()
                  .HasMaxLength(10);

            entity.HasIndex(t => new { t.Value, t.Unit })
                  .IsUnique();

            // Seed данные
            entity.HasData(
                new Timeframe { Id = 1, Value = 1, Unit = "minute" },
                new Timeframe { Id = 2, Value = 5, Unit = "minute" },
                new Timeframe { Id = 3, Value = 15, Unit = "minute" },
                new Timeframe { Id = 4, Value = 1, Unit = "hour" },
                new Timeframe { Id = 5, Value = 4, Unit = "hour" },
                new Timeframe { Id = 6, Value = 1, Unit = "day" }
            );
        });

        // Конфигурация Symbol
        modelBuilder.Entity<Symbol>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Id).ValueGeneratedOnAdd();

            entity.Property(s => s.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(s => s.BaseAsset)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(s => s.QuoteAsset)
                  .IsRequired()
                  .HasMaxLength(20);

            // Настройка точности для decimal полей
            entity.Property(s => s.MinTradeQuantity)
                  .HasPrecision(18, 8);

            entity.Property(s => s.MinNotionalValue)
                  .HasPrecision(18, 8);

            entity.Property(s => s.MaxTradeQuantity)
                  .HasPrecision(18, 8);

            entity.Property(s => s.QuantityStep)
                  .HasPrecision(18, 8);

            entity.Property(s => s.PriceStep)
                  .HasPrecision(18, 8);

            entity.Property(s => s.ContractSize)
                  .HasPrecision(18, 8);

            entity.Property(s => s.MaxShortLeverage)
                  .HasPrecision(18, 2);

            entity.Property(s => s.MaxLongLeverage)
                  .HasPrecision(18, 2);

            // Внешние ключи
            entity.HasOne(s => s.Exchange)
                  .WithMany(e => e.Symbols)
                  .HasForeignKey(s => s.ExchangeId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(s => s.MarketType)
                  .WithMany(m => m.Symbols)
                  .HasForeignKey(s => s.MarketTypeId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(s => new { s.Name, s.ExchangeId })
                  .IsUnique();
        });

        // Конфигурация OhlcvData
        modelBuilder.Entity<OhlcvData>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.Id).ValueGeneratedOnAdd();

            // Настройка точности
            entity.Property(o => o.Open)
                  .HasPrecision(18, 8);

            entity.Property(o => o.High)
                  .HasPrecision(18, 8);

            entity.Property(o => o.Low)
                  .HasPrecision(18, 8);

            entity.Property(o => o.Close)
                  .HasPrecision(18, 8);

            entity.Property(o => o.Volume)
                  .HasPrecision(18, 8);

            // Внешние ключи
            entity.HasOne(o => o.Symbol)
                  .WithMany(s => s.OhlcvData)
                  .HasForeignKey(o => o.SymbolId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.Timeframe)
                  .WithMany(t => t.OhlcvData)
                  .HasForeignKey(o => o.TimeframeId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Составной индекс
            entity.HasIndex(o => new { o.SymbolId, o.TimeframeId, o.Timestamp })
                  .IsUnique();

            entity.Property(o => o.CreatedAt)
                  .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        });
    }
}