namespace TradingAssistant.Core.Entities;

public class Symbol
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string BaseAsset { get; set; }
    public string QuoteAsset { get; set; }

    // Связи
    public int ExchangeId { get; set; }
    public Exchange Exchange { get; set; }

    public int MarketTypeId { get; set; }
    public MarketType MarketType { get; set; }

    // Общие параметры
    public decimal MinTradeQuantity { get; set; }
    public decimal MinNotionalValue { get; set; }
    public decimal? MaxTradeQuantity { get; set; }
    public decimal QuantityStep { get; set; }
    public decimal PriceStep { get; set; }
    public int? QuantityDecimals { get; set; }
    public int? PriceDecimals { get; set; }
    public bool IsActive { get; set; }

    // Параметры для фьючерсов
    public decimal? ContractSize { get; set; }
    public DateTime? DeliveryTime { get; set; }
    public decimal? MaxShortLeverage { get; set; }
    public decimal? MaxLongLeverage { get; set; }

    public ICollection<OhlcvData> OhlcvData { get; set; } = new List<OhlcvData>();
}