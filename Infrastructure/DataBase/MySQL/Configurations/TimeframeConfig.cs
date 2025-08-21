using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.MySQL.Configurations;

public class TimeframeConfig : IEntityTypeConfiguration<Timeframe>
{
    public void Configure(EntityTypeBuilder<Timeframe> builder)
    {
        builder.ToTable("Timeframes");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        builder.Property(t => t.Value)
              .IsRequired()
              .HasMaxLength(10)
              .HasColumnName("value");

        builder.Property(t => t.Unit)
              .HasConversion<string>() // Для работы с enum в коде
              .HasColumnType(Timeframe.GetStringJoinEnumMySql()) // MySQL ENUM тип
              .IsRequired()
              .HasColumnName("unit");

        builder.Property(t => t.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
              .HasColumnName("created_at");

        builder.Property(t => t.UpdatedAt)
              .HasColumnName("updated_at");

        // Индексы
        builder.HasIndex(t => new { t.Value, t.Unit })
              .IsUnique()
              .HasDatabaseName("UX_Timeframes_UniqueComposite");

        builder.HasComment("Таймфреймы");
    }
}