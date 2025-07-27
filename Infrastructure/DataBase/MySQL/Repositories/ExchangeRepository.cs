using Microsoft.EntityFrameworkCore;
using TradingAssistant.Core.Entities;
using TradingAssistant.Core.Interfaces.Repositories;
using TradingAssistant.Infrastructure.DataBase.MySQL;

namespace TradingAssistant.Infrastructure.Repositories;

public class ExchangeRepository : IExchangeRepository
{
    private readonly AppDbContext _context;

    public ExchangeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> GetExchangeIdAsync(string exchangeName, CancellationToken cancellationToken = default)
    {
        var exchange = await _context.Exchanges
            .FirstOrDefaultAsync(e => e.Name == exchangeName, cancellationToken);

        if (exchange == null)
        {
            exchange = new Exchange { Name = exchangeName };
            _context.Exchanges.Add(exchange);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return exchange.Id;
    }

    public async Task<int> GetMarketTypeIdAsync(string tradingType, CancellationToken cancellationToken = default)
    {
        // Предполагаем, что в БД есть таблица MarketTypes с предзаполненными значениями:
        // 1 - "Spot", 2 - "Futures"
        return tradingType.ToLower() switch
        {
            "spot" or "spots" => 1,
            "future" or "futures" => 2,
            _ => throw new ArgumentException($"Unsupported trading type: {tradingType}")
        };
    }

    public async Task<IEnumerable<Symbol>> GetSymbolsAsync(
        string? exchangeName = null,
        string? tradingType = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Symbols
            .Include(s => s.Exchange)
            .Include(s => s.MarketType)
            .AsQueryable();

        if (!string.IsNullOrEmpty(exchangeName))
            query = query.Where(s => s.Exchange.Name == exchangeName);

        if (!string.IsNullOrEmpty(tradingType))
        {
            var marketTypeId = await GetMarketTypeIdAsync(tradingType, cancellationToken);
            query = query.Where(s => s.MarketTypeId == marketTypeId);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task UpsertSymbolsAsync(
        IEnumerable<Symbol> symbols,
        CancellationToken cancellationToken = default)
    {
        foreach (var symbol in symbols)
        {
            var existingSymbol = await _context.Symbols
                .FirstOrDefaultAsync(s =>
                    s.Name == symbol.Name &&
                    s.ExchangeId == symbol.ExchangeId &&
                    s.MarketTypeId == symbol.MarketTypeId,
                    cancellationToken);

            if (existingSymbol == null)
                _context.Symbols.Add(symbol);
            else
                _context.Entry(existingSymbol).CurrentValues.SetValues(symbol);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}