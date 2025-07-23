using Binance.Net.Clients;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using TradingAssistant.Core.Interfaces;

namespace TradingAssistant.Infrastructure.Exchanges
{
    public interface IExchangeClient<T> where T : BaseRestClient
    {
        T GetClient();

        Task<List<string>> GetSpotSymbolsAsync();
        Task<List<string>> GetSpotSymbolsAsync(string? baseAsset = null, string? quoteAsset = null);
    }
}
