namespace TradingAssistant.Core.Entities.Exchanges;

public enum TimeframeUnit
{
    Tick,      // Было: tick ❌ (лучше PascalCase)
    Second,    // Было: second ❌
    Minute,    // Было: minute ❌
    Hour,      // Было: hour ❌
    Day,       // Было: day ❌
    Week,      // Было: week ❌
    Month,     // Было: month ❌
    Year       // Было: year ❌
}

public class Timeframe
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public TimeframeUnit Unit { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Навигационные свойства
    public ICollection<OhlcvData> OhlcvData { get; set; } = new List<OhlcvData>();

    public static IEnumerable<TimeframeUnit> GetEnumValues()
    {
        return Enum.GetValues<TimeframeUnit>();
    }

    public static string GetStringJoinEnumMySql()
    {
        var values = Enum.GetValues<TimeframeUnit>().Select(e => $"'{e}'");
        return $"ENUM({string.Join(",", values)})";
    }
}