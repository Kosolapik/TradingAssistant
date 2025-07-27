using TradingAssistant.Core.Entities;

namespace TradingAssistant.Core.Interfaces.Repositories;

public interface IExchangeRepository
{
    // Получение ID биржи по названию
    Task<int> GetExchangeIdAsync(string exchangeName, CancellationToken cancellationToken = default);

    // Получение ID типа рынка (Spot/Futures)
    Task<int> GetMarketTypeIdAsync(string tradingType, CancellationToken cancellationToken = default);

    // Получение символов из БД
    Task<IEnumerable<Symbol>> GetSymbolsAsync(
        string? exchangeName = null,
        string? tradingType = null,
        CancellationToken cancellationToken = default);

    // Добавление/обновление символов
    Task UpsertSymbolsAsync(IEnumerable<Symbol> symbols, CancellationToken cancellationToken = default);
}