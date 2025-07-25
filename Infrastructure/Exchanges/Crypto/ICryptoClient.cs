

using CryptoExchange.Net.SharedApis;

namespace TradingAssistant.Infrastructure.Exchanges.Crypto
{
    public interface ICryptoClient
    {
        Task<SharedSpotSymbol[]> GetSpotSymbolsAsync(string exchange);
        Task<SharedFuturesSymbol[]> GetFuturesSymbolsAsync(string exchange);
    }
}
