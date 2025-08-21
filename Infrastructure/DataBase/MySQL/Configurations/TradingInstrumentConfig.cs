using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.MySQL.Configurations;

public class TradingInstrumentConfig : IEntityTypeConfiguration<TradingInstrument>
{
    public void Configure(EntityTypeBuilder<TradingInstrument> builder)
    {
        builder.ToTable("TradingInstruments");

        builder.HasKey(ti => ti.Id);
        builder.Property(ti => ti.Id).ValueGeneratedOnAdd();

        builder.Property(ti => ti.Code)
              .IsRequired()
              .HasMaxLength(255)
              .HasColumnName("code");

        builder.Property(ti => ti.IsActive)
              .HasColumnType("tinyint(1)")
              .HasDefaultValue(true)
              .HasColumnName("is_active");

        builder.Property(ti => ti.Description)
              .HasMaxLength(255)
              .HasColumnName("description");

        builder.Property(ti => ti.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
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

        // Индексы
        // ОСНОВНОЙ УНИКАЛЬНЫЙ ИНДЕКС
        builder.HasIndex(ti => new { ti.BaseAssetId, ti.QuoteAssetId, ti.ExchangeId, ti.InstrumentTypeId })
              .IsUnique()
              .HasDatabaseName("UX_TradingInstruments_UniqueComposite");

        // ИНДЕКСЫ ДЛЯ ЧАСТЫХ ЗАПРОСОВ:

        // 1. Для: ti.ExchangeId, ti.InstrumentTypeId
        builder.HasIndex(ti => new { ti.ExchangeId, ti.InstrumentTypeId })
              .HasDatabaseName("IX_TradingInstruments_ExchangeId_InstrumentTypeId");

        // 2. Для: ti.ExchangeId, ti.InstrumentTypeId, ti.QuoteAssetId  
        builder.HasIndex(ti => new { ti.ExchangeId, ti.InstrumentTypeId, ti.QuoteAssetId })
              .HasDatabaseName("IX_TradingInstruments_ExchangeId_InstrumentTypeId_QuoteAssetId");

        // 3. Для: ti.ExchangeId, ti.BaseAssetId, ti.QuoteAssetId
        builder.HasIndex(ti => new { ti.ExchangeId, ti.BaseAssetId, ti.QuoteAssetId })
              .HasDatabaseName("IX_TradingInstruments_ExchangeId_BaseAssetId_QuoteAssetId");

        // 4. Для: ti.BaseAssetId, ti.QuoteAssetId
        builder.HasIndex(ti => new { ti.BaseAssetId, ti.QuoteAssetId })
              .HasDatabaseName("IX_TradingInstruments_BaseAssetId_QuoteAssetId");

        // 5. Для: ti.BaseAssetId, ti.QuoteAssetId, ti.InstrumentTypeId
        builder.HasIndex(ti => new { ti.BaseAssetId, ti.QuoteAssetId, ti.InstrumentTypeId })
              .HasDatabaseName("IX_TradingInstruments_BaseAssetId_QuoteAssetId_InstrumentTypeId");

        // 6. Индекс для поиска по коду
        builder.HasIndex(ti => ti.Code)
              .HasDatabaseName("IX_TradingInstruments_Code");

        // 7. Индекс для фильтрации по активности (опционально)
        builder.HasIndex(ti => new { ti.IsActive, ti.ExchangeId })
              .HasDatabaseName("IX_TradingInstruments_IsActive_ExchangeId")
              .HasFilter("is_active = 1");

        builder.HasComment("Торговые инструменты");
    }
}