namespace TradingAssistant.Core.Entities.Exchanges;

public class InstrumentPropertyValue
{
    public long Id { get; set; }
    public long InstrumentId { get; set; }
    public long PropertyId { get; set; }
    public long? PossibleValueId { get; set; }
    public decimal? DecimalValue { get; set; }
    public int? IntegerValue { get; set; }
    public string? StringValue { get; set; }
    public bool? BooleanValue { get; set; }
    public DateTime? DateTimeValue { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Навигационные свойства
    public TradingInstrument Instrument { get; set; } = null!;
    public InstrumentProperty Property { get; set; } = null!;
    public PropertyPossibleValue? PossibleValue { get; set; }
}