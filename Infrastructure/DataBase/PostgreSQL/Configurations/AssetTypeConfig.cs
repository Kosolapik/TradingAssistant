using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.PostgreSQL.Configurations;

public class AssetTypeConfig : IEntityTypeConfiguration<AssetType>
{
    public void Configure(EntityTypeBuilder<AssetType> builder)
    {
        builder.ToTable("AssetTypes");

        builder.HasKey(ac => ac.Id);
        builder.Property(ac => ac.Id).ValueGeneratedOnAdd();

        builder.Property(ac => ac.Code)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("code");

        builder.Property(ac => ac.Description)
            .HasMaxLength(255)
            .HasColumnName("description");

        builder.Property(ac => ac.IsActive)
            .HasColumnType("boolean")
            .HasDefaultValue(true)
            .HasColumnName("is_active");

        builder.Property(ac => ac.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
            .HasColumnName("created_at");

        builder.Property(ac => ac.UpdatedAt)
            .HasColumnName("updated_at");

        // Индексы
        builder.HasIndex(ac => ac.Code)
            .IsUnique()
            .HasDatabaseName("IX_AssetTypes_Code");

        builder.HasComment("Типы активов");
    }
}