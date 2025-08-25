namespace TradingAssistant.Core.Entities.Exchanges;

public class PropertyPossibleValue
{
    public long Id { get; set; }
    public long PropertyId { get; set; }
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Навигационные свойства
    public InstrumentProperty Property { get; set; } = null!;
    public ICollection<InstrumentPropertyValue> PropertyValues { get; set; } = new List<InstrumentPropertyValue>();
}