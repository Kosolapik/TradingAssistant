using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL.Configurations;

public class TimeframeConfig : IEntityTypeConfiguration<Timeframe>
{
    public void Configure(EntityTypeBuilder<Timeframe> builder)
    {
        builder.ToTable("timeframes");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id)
              .ValueGeneratedOnAdd()
              .UseIdentityAlwaysColumn();

        builder.Property(t => t.Value)
              .IsRequired()
              .HasMaxLength(10)
              .HasColumnName("value");

        builder.Property(t => t.Unit)
              .HasConversion(
                  v => v.ToString().ToLower(), // Enum -> lowercase string
                  v => (TimeframeUnit)Enum.Parse(typeof(TimeframeUnit), v, true) // string -> Enum (ignore case)
              )
              .HasColumnType("timeframe_unit") // Указываем тип БД
              .IsRequired()
              .HasColumnName("unit");

        builder.Property(t => t.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP")
              .HasColumnName("created_at");

        builder.Property(t => t.UpdatedAt)
              .HasColumnName("updated_at");

        // Индексы
        builder.HasIndex(t => new { t.Value, t.Unit })
              .IsUnique()
              .HasDatabaseName("ux_timeframes_unique");

        builder.HasComment("Таймфреймы");
    }
}