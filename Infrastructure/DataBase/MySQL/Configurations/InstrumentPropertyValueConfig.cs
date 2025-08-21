using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.MySQL.Configurations;

public class InstrumentPropertyValueConfig : IEntityTypeConfiguration<InstrumentPropertyValue>
{
    public void Configure(EntityTypeBuilder<InstrumentPropertyValue> builder)
    {
        builder.ToTable("InstrumentPropertyValues");

        builder.HasKey(ipv => ipv.Id);
        builder.Property(ipv => ipv.Id).ValueGeneratedOnAdd();

        // Настройка точности
        builder.Property(ipv => ipv.DecimalValue)
              .HasPrecision(18, 8)
              .HasColumnName("decimal_value");

        builder.Property(ipv => ipv.IntegerValue)
              .HasColumnName("integer_value");

        builder.Property(ipv => ipv.StringValue)
              .HasMaxLength(255)
              .HasColumnName("string_value");

        builder.Property(ipv => ipv.BooleanValue)
              .HasColumnType("tinyint(1)")
              .HasColumnName("boolean_value");

        builder.Property(ipv => ipv.DateTimeValue)
              .HasColumnName("datetime_value");

        builder.Property(ipv => ipv.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
              .HasColumnName("created_at");

        builder.Property(ipv => ipv.UpdatedAt)
              .HasColumnName("updated_at");

        // Внешние ключи
        builder.HasOne(ipv => ipv.Instrument)
              .WithMany(ti => ti.PropertyValues)
              .HasForeignKey(ipv => ipv.InstrumentId)
              .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ipv => ipv.Property)
              .WithMany(ip => ip.PropertyValues)
              .HasForeignKey(ipv => ipv.PropertyId)
              .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ipv => ipv.PossibleValue)
              .WithMany(ppv => ppv.PropertyValues)
              .HasForeignKey(ipv => ipv.PossibleValueId)
              .OnDelete(DeleteBehavior.SetNull);

        // Индексы
        builder.HasIndex(ipv => new { ipv.InstrumentId, ipv.PropertyId, ipv.PossibleValueId })
              .IsUnique()
              .HasDatabaseName("UX_InstrumentPropertyValues_UniqueComposite");

        builder.HasComment("Значения свойств инструментов");
    }
}