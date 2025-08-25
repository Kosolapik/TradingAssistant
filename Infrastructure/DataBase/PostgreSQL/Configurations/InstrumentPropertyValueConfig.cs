using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL.Configurations;

public class InstrumentPropertyValueConfig : IEntityTypeConfiguration<InstrumentPropertyValue>
{
    public void Configure(EntityTypeBuilder<InstrumentPropertyValue> builder)
    {
        builder.ToTable("instrument_property_values");

        builder.HasKey(ipv => ipv.Id);
        builder.Property(ipv => ipv.Id)
              .ValueGeneratedOnAdd()
              .UseIdentityAlwaysColumn();

        // Настройка точности с явным указанием типа
        builder.Property(ipv => ipv.DecimalValue)
              .HasPrecision(18, 8)
              .HasColumnType("numeric(18,8)") // Явный тип для PostgreSQL
              .HasColumnName("decimal_value");

        builder.Property(ipv => ipv.IntegerValue)
              .HasColumnName("integer_value");

        builder.Property(ipv => ipv.StringValue)
              .HasMaxLength(255)
              .HasColumnName("string_value");

        builder.Property(ipv => ipv.BooleanValue)
              .HasColumnType("boolean") // PostgreSQL boolean
              .HasColumnName("boolean_value");

        builder.Property(ipv => ipv.DateTimeValue)
              .HasColumnType("timestamp with time zone") // Для временных зон
              .HasColumnName("datetime_value");

        builder.Property(ipv => ipv.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP")
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

        // Уникальный индекс (без possible_value_id, т.к. он nullable)
        builder.HasIndex(ipv => new { ipv.InstrumentId, ipv.PropertyId })
              .IsUnique()
              .HasDatabaseName("ux_instrument_property_values_unique");

        builder.HasComment("Значения свойств инструментов");
    }
}