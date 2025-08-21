namespace TradingAssistant.Core.Entities.Exchanges;

public class TradingInstrument
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public long BaseAssetId { get; set; }
    public long QuoteAssetId { get; set; }
    public int ExchangeId { get; set; }
    public int InstrumentTypeId { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Навигационные свойства
    public Asset BaseAsset { get; set; } = null!;
    public Asset QuoteAsset { get; set; } = null!;
    public Exchange Exchange { get; set; } = null!;
    public InstrumentType InstrumentType { get; set; } = null!;
    public ICollection<InstrumentPropertyValue> PropertyValues { get; set; } = new List<InstrumentPropertyValue>();
    public ICollection<OhlcvData> OhlcvData { get; set; } = new List<OhlcvData>();
}