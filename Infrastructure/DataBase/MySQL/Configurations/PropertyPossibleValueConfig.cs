using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.MySQL.Configurations;

public class PropertyPossibleValueConfig : IEntityTypeConfiguration<PropertyPossibleValue>
{
    public void Configure(EntityTypeBuilder<PropertyPossibleValue> builder)
    {
        builder.ToTable("PropertyPossibleValues");

        builder.HasKey(ppv => ppv.Id);
        builder.Property(ppv => ppv.Id).ValueGeneratedOnAdd();

        builder.Property(ppv => ppv.Value)
              .IsRequired()
              .HasMaxLength(255)
              .HasColumnName("value");

        builder.Property(ppv => ppv.NumericValue)
              .HasDefaultValue(0)
              .HasColumnName("numeric_value");

        builder.Property(ppv => ppv.Description)
              .HasMaxLength(255)
              .HasColumnName("description");

        builder.Property(ppv => ppv.CreatedAt)
              .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
              .HasColumnName("created_at");

        builder.Property(ppv => ppv.UpdatedAt)
              .HasColumnName("updated_at");

        // Внешний ключ
        builder.HasOne(ppv => ppv.Property)
              .WithMany(ip => ip.PossibleValues)
              .HasForeignKey(ppv => ppv.PropertyId)
              .OnDelete(DeleteBehavior.Cascade);

        // Индексы
        builder.HasIndex(ppv => new { ppv.PropertyId, ppv.Value })
              .IsUnique()
              .HasDatabaseName("UX_PropertyPossibleValues_UniqueComposite");

        builder.HasComment("Возможные значения свойств");
    }
}