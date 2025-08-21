using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.MySQL.Configurations;

public class InstrumentPropertyConfig : IEntityTypeConfiguration<InstrumentProperty>
{
    public void Configure(EntityTypeBuilder<InstrumentProperty> builder)
    {
        builder.ToTable("InstrumentProperties");

        builder.HasKey(ip => ip.Id);
        builder.Property(ip => ip.Id).ValueGeneratedOnAdd();

        builder.Property(ip => ip.Code)
              .IsRequired()
              .HasMaxLength(255)
              .HasColumnName("code");

        builder.Property(ip => ip.DataType)
              .HasConversion<string>() // Для работы с enum в коде
              .HasColumnType(InstrumentProperty.GetStringJoinEnumMySql()) // MySQL ENUM тип
              .IsRequired()
              .HasColumnName("data_type");

        builder.Property(ip => ip.Description)
              .HasMaxLength(255)
              .HasColumnName("description");

        builder.Property(ip => ip.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
              .HasColumnName("created_at");

        builder.Property(ip => ip.UpdatedAt)
              .HasColumnName("updated_at");

        // Индексы
        builder.HasIndex(ip => ip.Code)
              .IsUnique()
              .HasDatabaseName("IX_InstrumentProperties_Code");

        builder.HasComment("Свойства инструментов");
    }
}