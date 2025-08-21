using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.MySQL.Configurations;

public class OhlcvDataConfig : IEntityTypeConfiguration<OhlcvData>
{
    public void Configure(EntityTypeBuilder<OhlcvData> builder)
    {
        builder.ToTable("OHLCV");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).ValueGeneratedOnAdd();

        // Настройка точности
        builder.Property(o => o.Open)
              .HasPrecision(28, 8)
              .HasColumnName("open");

        builder.Property(o => o.High)
              .HasPrecision(28, 8)
              .HasColumnName("high");

        builder.Property(o => o.Low)
              .HasPrecision(28, 8)
              .HasColumnName("low");

        builder.Property(o => o.Close)
              .HasPrecision(28, 8)
              .HasColumnName("close");

        builder.Property(o => o.Volume)
              .HasPrecision(36, 18)
              .HasColumnName("volume");

        builder.Property(o => o.Timestamp)
              .HasColumnName("timestamp");

        builder.Property(o => o.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
              .HasColumnName("created_at");

        builder.Property(o => o.UpdatedAt)
              .HasColumnName("updated_at");

        // Внешние ключи
        builder.HasOne(o => o.Instrument)
              .WithMany(ti => ti.OhlcvData)
              .HasForeignKey(o => o.InstrumentId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Timeframe)
              .WithMany(t => t.OhlcvData)
              .HasForeignKey(o => o.TimeframeId)
              .OnDelete(DeleteBehavior.Restrict);

        // Индексы
        builder.HasIndex(o => new { o.InstrumentId, o.TimeframeId, o.Timestamp })
              .IsUnique()
              .HasDatabaseName("UX_OHLCV_UniqueComposite");

        builder.HasComment("OHLCV данные");
    }
}