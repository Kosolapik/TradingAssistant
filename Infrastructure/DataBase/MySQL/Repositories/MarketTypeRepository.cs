using Microsoft.EntityFrameworkCore;
using TradingAssistant.Core.Entities;
using TradingAssistant.Core.Interfaces.Repositories;
using TradingAssistant.Infrastructure.DataBase.MySQL;

namespace TradingAssistant.Infrastructure.Repositories;

public class MarketTypeRepository : IMarketTypeRepository
{
    private readonly AppDbContext _context;

    public MarketTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetMarketTypeIdAsync(string tradingType, CancellationToken cancellationToken = default)
    {
        var normalizedType = tradingType.ToLower() switch
        {
            "spot" or "spots" => "Spot",
            "future" or "futures" => "PerpetualLinear",
            _ => throw new ArgumentException($"Unsupported trading type: {tradingType}")
        };

        var marketType = await _context.MarketTypes
            .FirstOrDefaultAsync(mt => mt.Type.ToLower() == normalizedType, cancellationToken);

        if (marketType == null)
            throw new KeyNotFoundException($"Market type '{tradingType}' not found in database");

        return marketType.Id;
    }

    public async Task<IEnumerable<MarketType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.MarketTypes
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}