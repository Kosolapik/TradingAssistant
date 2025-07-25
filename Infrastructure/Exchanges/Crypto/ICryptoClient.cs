

using CryptoExchange.Net.SharedApis;

namespace TradingAssistant.Infrastructure.Exchanges.Crypto
{
    public interface ICryptoClient
    {
        Task<ExchangeWebResult<SharedSpotSymbol[]>> GetSpotSymbolsAsync(string exchange);
        Task<ExchangeWebResult<SharedFuturesSymbol[]>> GetFuturesSymbolsAsync(string exchange);
    }
}
