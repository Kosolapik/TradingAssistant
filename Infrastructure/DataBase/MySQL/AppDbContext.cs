using Microsoft.EntityFrameworkCore;
using TradingAssistant.Core.Entities;

namespace TradingAssistant.Infrastructure.DataBase.MySQL;
public class AppDbContext : DbContext
{
    // Конструктор для DI
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Exchange> Exchanges { get; set; }
    public DbSet<Symbol> Symbols { get; set; }
    public DbSet<MarketType> MarketTypes { get; set; }
    public DbSet<Timeframe> Timeframes { get; set; }
    public DbSet<OhlcvData> OhlcvData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Уникальные индексы
        modelBuilder.Entity<Exchange>()
            .HasIndex(e => e.Name)
            .IsUnique();

        modelBuilder.Entity<Symbol>()
            .HasIndex(s => new { s.BaseAsset, s.QuoteAsset })
            .IsUnique();

        modelBuilder.Entity<MarketType>()
            .HasIndex(m => m.Type)
            .IsUnique();

        modelBuilder.Entity<Timeframe>()
            .HasIndex(t => new { t.Value, t.Unit })
            .IsUnique();

        // Составной индекс для быстрого поиска исторических данных
        modelBuilder.Entity<OhlcvData>()
            .HasIndex(o => new { o.ExchangeId, o.SymbolId, o.MarketTypeId, o.TimeframeId, o.Timestamp });

        // Внешние ключи
        modelBuilder.Entity<OhlcvData>()
            .HasOne(o => o.Exchange)
            .WithMany(e => e.OhlcvData)
            .HasForeignKey(o => o.ExchangeId);

        modelBuilder.Entity<OhlcvData>()
            .HasOne(o => o.Symbol)
            .WithMany(s => s.OhlcvData)
            .HasForeignKey(o => o.SymbolId);

        modelBuilder.Entity<OhlcvData>()
            .HasOne(o => o.MarketType)
            .WithMany(m => m.OhlcvData)
            .HasForeignKey(o => o.MarketTypeId);

        modelBuilder.Entity<OhlcvData>()
            .HasOne(o => o.Timeframe)
            .WithMany(t => t.OhlcvData)
            .HasForeignKey(o => o.TimeframeId);
    }
}

