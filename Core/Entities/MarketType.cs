namespace TradingAssistant.Core.Entities;

public class MarketType
{
    public int Id { get; set; }
    public string Type { get; set; }  // spot, perpetual, futures
    public string? Description { get; set; }

    // Навигационные свойства
    public ICollection<OhlcvData> OhlcvData { get; set; } = new List<OhlcvData>();
}