namespace TradingAssistant.Core.Entities.Exchanges;

public class Asset
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public int AssetTypeId { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Навигационные свойства
    public AssetType AssetType { get; set; } = null!;
    public ICollection<TradingInstrument> BaseInstruments { get; set; } = new List<TradingInstrument>();
    public ICollection<TradingInstrument> QuoteInstruments { get; set; } = new List<TradingInstrument>();
}