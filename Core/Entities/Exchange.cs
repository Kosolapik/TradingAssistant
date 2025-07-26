namespace TradingAssistant.Core.Entities;

public class Exchange
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Symbol> Symbols { get; set; } = new List<Symbol>();
}