using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.MySQL.Configurations;

public class AssetConfig : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("Assets");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedOnAdd();

        builder.Property(a => a.Code)
              .IsRequired()
              .HasMaxLength(255)
              .HasColumnName("code");

        builder.Property(a => a.IsActive)
              .HasColumnType("tinyint(1)")
              .HasDefaultValue(true)
              .HasColumnName("is_active");

        builder.Property(a => a.Description)
              .HasMaxLength(255)
              .HasColumnName("description");

        builder.Property(a => a.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
              .HasColumnName("created_at");

        builder.Property(a => a.UpdatedAt)
              .HasColumnName("updated_at");

        // Внешний ключ
        builder.HasOne(a => a.AssetType)
              .WithMany(ac => ac.Assets)
              .HasForeignKey(a => a.AssetTypeId)
              .OnDelete(DeleteBehavior.Restrict);

        // Индексы
        builder.HasIndex(a => a.Code)
              .IsUnique()
              .HasDatabaseName("IX_Assets_Code");

        builder.HasComment("Активы");
    }
}