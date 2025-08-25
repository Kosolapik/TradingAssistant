using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL.Configurations;

public class ExchangeConfig : IEntityTypeConfiguration<Exchange>
{
    public void Configure(EntityTypeBuilder<Exchange> builder)
    {
        builder.ToTable("Exchanges");

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();

        builder.Property(e => e.Code)
              .IsRequired()
              .HasMaxLength(255)
              .HasColumnName("code");

        builder.Property(e => e.Description)
              .HasMaxLength(255)
              .HasColumnName("description");

        builder.Property(e => e.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
              .HasColumnName("created_at");

        builder.Property(e => e.UpdatedAt)
              .HasColumnName("updated_at");

        // Индексы
        builder.HasIndex(e => e.Code)
              .IsUnique()
              .HasDatabaseName("IX_Exchanges_Code");

        builder.HasComment("Таблица бирж");
    }
}