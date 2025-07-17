namespace TradingAssistant.Core.Entities;

public class Symbol
{
    public int Id { get; set; }
    public string BaseAsset { get; set; }  // BTC, ETH
    public string QuoteAsset { get; set; } // USDT, USD

    // Навигационные свойства
    public ICollection<OhlcvData> OhlcvData { get; set; } = new List<OhlcvData>();

    public string GetSymbolString() => $"{BaseAsset}/{QuoteAsset}";
}