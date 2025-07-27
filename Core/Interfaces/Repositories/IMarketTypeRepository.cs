using TradingAssistant.Core.Entities;

namespace TradingAssistant.Core.Interfaces.Repositories;

public interface IMarketTypeRepository
{
    /// <summary>
    /// Получает ID типа рынка по его названию (spot, futures)
    /// </summary>
    Task<int> GetMarketTypeIdAsync(string tradingType, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает все доступные типы рынков из БД
    /// </summary>
    Task<IEnumerable<MarketType>> GetAllAsync(CancellationToken cancellationToken = default);
}