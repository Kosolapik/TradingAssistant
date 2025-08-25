using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL.Configurations;

public class InstrumentPropertyConfig : IEntityTypeConfiguration<InstrumentProperty>
{
    public void Configure(EntityTypeBuilder<InstrumentProperty> builder)
    {
        builder.ToTable("instrument_properties"); // snake_case

        builder.HasKey(ip => ip.Id);
        builder.Property(ip => ip.Id)
            .ValueGeneratedOnAdd()
            .UseIdentityAlwaysColumn();

        builder.Property(ip => ip.Code)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("code");

        builder.Property(ip => ip.DataType)
            .HasConversion(
                v => v.ToString().ToLower(), // Enum -> lowercase string
                v => (PropertyDataType)Enum.Parse(typeof(PropertyDataType), v, true) // string -> Enum (ignore case)
            )
            .HasColumnType("property_data_type") // Указываем тип БД
            .IsRequired()
            .HasColumnName("data_type");

        builder.Property(ip => ip.Description)
            .HasMaxLength(255)
            .HasColumnName("description");

        builder.Property(ip => ip.IsActive)
            .HasColumnType("boolean")
            .HasDefaultValue(true)
            .HasColumnName("is_active");

        builder.Property(ip => ip.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP")
            .HasColumnName("created_at");

        builder.Property(ip => ip.UpdatedAt)
             .HasColumnName("updated_at");

        // Индексы
        builder.HasIndex(ip => ip.Code)
            .IsUnique()
            .HasDatabaseName("ix_instrument_properties_code");

        builder.HasComment("Свойства инструментов");
    }
}