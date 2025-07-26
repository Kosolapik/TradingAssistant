namespace TradingAssistant.Core.Entities;

public class OhlcvData
{
    public long Id { get; set; }

    // Связи
    public int SymbolId { get; set; }
    public Symbol Symbol { get; set; }

    public int TimeframeId { get; set; }
    public Timeframe Timeframe { get; set; }

    // Данные свечи
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    public double Volume { get; set; }
    public DateTime Timestamp { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}