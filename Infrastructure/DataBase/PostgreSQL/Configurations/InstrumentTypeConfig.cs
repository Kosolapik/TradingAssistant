using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL.Configurations;

public class InstrumentTypeConfig : IEntityTypeConfiguration<InstrumentType>
{
    public void Configure(EntityTypeBuilder<InstrumentType> builder)
    {
        builder.ToTable("InstrumentTypes");

        builder.HasKey(it => it.Id);
        builder.Property(it => it.Id).ValueGeneratedOnAdd();

        builder.Property(it => it.Code)
              .IsRequired()
              .HasMaxLength(255)
              .HasColumnName("code");

        builder.Property(it => it.Description)
              .HasMaxLength(255)
              .HasColumnName("description");

        builder.Property(it => it.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
              .HasColumnName("created_at");

        builder.Property(it => it.UpdatedAt)
              .HasColumnName("updated_at");

        // Индексы
        builder.HasIndex(it => it.Code)
              .IsUnique()
              .HasDatabaseName("IX_InstrumentTypes_Code");

        builder.HasComment("Типы инструментов");
    }
}