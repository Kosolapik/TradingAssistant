namespace TradingAssistant.Core.Entities;

public class OhlcvData
{
    public long Id { get; set; }

    // Внешние ключи
    public int ExchangeId { get; set; }
    public int SymbolId { get; set; }
    public int MarketTypeId { get; set; }
    public int TimeframeId { get; set; }

    // Данные свечи
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Close { get; set; }
    public double Volume { get; set; }
    public DateTime Timestamp { get; set; } // Время открытия свечи
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Навигационные свойства
    public Exchange Exchange { get; set; }
    public Symbol Symbol { get; set; }
    public MarketType MarketType { get; set; }
    public Timeframe Timeframe { get; set; }
}