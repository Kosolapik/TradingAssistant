namespace TradingAssistant.Core.Entities.Exchanges;

public class Exchange
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Навигационные свойства
    public ICollection<TradingInstrument> TradingInstruments { get; set; } = new List<TradingInstrument>();
}