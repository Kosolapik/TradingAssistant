using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL.Configurations;

public class AssetConfig : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("assets"); // lowercase для PostgreSQL

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
              .ValueGeneratedOnAdd()
              .UseIdentityAlwaysColumn(); // Для PostgreSQL

        builder.Property(a => a.Code)
              .IsRequired()
              .HasMaxLength(255)
              .HasColumnName("code");

        builder.Property(a => a.IsActive)
              .HasColumnType("boolean") // PostgreSQL boolean
              .HasDefaultValue(true)
              .HasColumnName("is_active");

        builder.Property(a => a.Description)
              .HasMaxLength(255)
              .HasColumnName("description");

        builder.Property(a => a.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP") // Без (6)
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
              .HasDatabaseName("ix_assets_code"); // lowercase

        builder.HasComment("Активы");
    }
}