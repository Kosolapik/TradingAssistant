namespace TradingAssistant.Core.Entities;

public class Timeframe
{
    public int Id { get; set; }
    public int Value { get; set; }      // 1, 5, 15
    public string Unit { get; set; }    // minute, hour, day

    // Навигационные свойства
    public ICollection<OhlcvData> OhlcvData { get; set; } = new List<OhlcvData>();

    public string GetTimeframeString() => $"{Value}{Unit[0]}"; // 1m, 5m, 1h
}