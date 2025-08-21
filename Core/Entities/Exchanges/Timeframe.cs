namespace TradingAssistant.Core.Entities.Exchanges;

public enum TimeframeUnit
{
    TICK,
    SECOND,
    MINUTE,
    HOUR,
    DAY,
    WEEK,
    MONTH,
    YEAR
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