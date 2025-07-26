namespace TradingAssistant.Core.Entities;

public class MarketType
{
    public int Id { get; set; }
    public string Type { get; set; }  // Spot, PerpetualLinear и т.д.
    public string? Description { get; set; }

    // Связь только с Symbol
    public ICollection<Symbol> Symbols { get; set; } = new List<Symbol>();
}