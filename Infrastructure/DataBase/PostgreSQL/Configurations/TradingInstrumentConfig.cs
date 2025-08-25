using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL.Configurations;

public class TradingInstrumentConfig : IEntityTypeConfiguration<TradingInstrument>
{
    public void Configure(EntityTypeBuilder<TradingInstrument> builder)
    {
        builder.ToTable("trading_instruments"); // snake_case

        builder.HasKey(ti => ti.Id);
        builder.Property(ti => ti.Id)
              .ValueGeneratedOnAdd()
              .UseIdentityAlwaysColumn();

        builder.Property(ti => ti.Code)
              .IsRequired()
              .HasMaxLength(255)
              .HasColumnName("code");

        builder.Property(ti => ti.IsActive)
              .HasColumnType("boolean") // PostgreSQL boolean
              .HasDefaultValue(true)
              .HasColumnName("is_active");

        builder.Property(ti => ti.Description)
              .HasMaxLength(255)
              .HasColumnName("description");

        builder.Property(ti => ti.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP")
              .HasColumnName("created_at");

        builder.Property(ti => ti.UpdatedAt)
              .HasColumnName("updated_at");

        // Внешние ключи
        builder.HasOne(ti => ti.BaseAsset)
              .WithMany(a => a.BaseInstruments)
              .HasForeignKey(ti => ti.BaseAssetId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ti => ti.QuoteAsset)
              .WithMany(a => a.QuoteInstruments)
              .HasForeignKey(ti => ti.QuoteAssetId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ti => ti.Exchange)
              .WithMany(e => e.TradingInstruments)
              .HasForeignKey(ti => ti.ExchangeId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ti => ti.InstrumentType)
              .WithMany(it => it.TradingInstruments)
              .HasForeignKey(ti => ti.InstrumentTypeId)
              .OnDelete(DeleteBehavior.Restrict);

        // Основной уникальный индекс
        builder.HasIndex(ti => new { ti.BaseAssetId, ti.QuoteAssetId, ti.ExchangeId, ti.InstrumentTypeId })
              .IsUnique()
              .HasDatabaseName("ux_trading_instruments_unique");

        // Дополнительные индексы (обновляем имена)
        builder.HasIndex(ti => new { ti.ExchangeId, ti.InstrumentTypeId })
              .HasDatabaseName("ix_trading_instruments_exchange_instrument");

        builder.HasIndex(ti => new { ti.ExchangeId, ti.InstrumentTypeId, ti.QuoteAssetId })
              .HasDatabaseName("ix_trading_instruments_exchange_instrument_quote");

        builder.HasIndex(ti => ti.Code)
              .HasDatabaseName("ix_trading_instruments_code");

        builder.HasIndex(ti => new { ti.IsActive, ti.ExchangeId })
              .HasDatabaseName("ix_trading_instruments_active_exchange");

        builder.HasComment("Торговые инструменты");
    }
}