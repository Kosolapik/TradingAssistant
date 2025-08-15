using CryptoClients.Net.Interfaces;
using CryptoExchange.Net.SharedApis;

namespace TradingAssistant.Infrastructure.Exchanges.Crypto
{
    public interface ICryptoClient
    {
        IExchangeRestClient cl();
        Task<IEnumerable<SharedSpotSymbol>> GetSpotSymbolsAsync(string exchange);
        Task<IEnumerable<SharedFuturesSymbol>> GetFuturesSymbolsAsync(string exchange);
    }
}
