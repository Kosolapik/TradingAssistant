namespace TradingAssistant.Core.Entities.Exchanges;

public class OhlcvData
{
    public long Id { get; set; }
    public long InstrumentId { get; set; }
    public int TimeframeId { get; set; }
    public DateTime Timestamp { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Навигационные свойства
    public TradingInstrument Instrument { get; set; } = null!;
    public Timeframe Timeframe { get; set; } = null!;
}