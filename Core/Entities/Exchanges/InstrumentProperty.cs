using System;

namespace TradingAssistant.Core.Entities.Exchanges;

public enum PropertyDataType
{
    Decimal,
    Integer,
    Boolean,
    String,
    DateTime
}

public class InstrumentProperty
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public PropertyDataType DataType { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Навигационные свойства
    public ICollection<PropertyPossibleValue> PossibleValues { get; set; } = new List<PropertyPossibleValue>();
    public ICollection<InstrumentPropertyValue> PropertyValues { get; set; } = new List<InstrumentPropertyValue>();

    public static IEnumerable<PropertyDataType> GetEnumValues()
    {
        return Enum.GetValues<PropertyDataType>();
    }

    public static string GetStringJoinEnumMySql()
    {
        var values = Enum.GetValues<PropertyDataType>().Select(e => $"'{e}'");
        return $"ENUM({string.Join(",", values)})";
    }
}