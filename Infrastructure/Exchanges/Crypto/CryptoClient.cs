using CryptoClients.Net;
using CryptoClients.Net.Interfaces;
using CryptoClients.Net.Models;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.SharedApis;

namespace TradingAssistant.Infrastructure.Exchanges.Crypto
{
    public class CryptoClient : ICryptoClient
    {
        public readonly IExchangeRestClient _client;

        public CryptoClient ()
        {
            _client = new ExchangeRestClient(
                // Global options
                options =>
                {
                    options.ApiCredentials = new ExchangeCredentials
                    {
                        Binance = new ApiCredentials("YyvboAPdAnJf4CRA6LJKQrDNKaTFcJM6w6Mirh9xit5xbNhLaPdtZa5Lsba6EyAJ", "jOcMzFtffJHN4jdukppHZ5EpkkTytZYsTUX7yzHqRgIsNe9sgVpjrPlKD2ZHk2ij"),
                        Bybit = new ApiCredentials("0KfQ79J593XRDIk5Gn", "IFyLmrl903kGcJrBanqVc2dea73vOsNg6B0U")
                    };
                    options.OutputOriginalData = true;
                }
            );
        }

        public IExchangeRestClient cl()
        {
            return _client;
        }

        public async Task<IEnumerable<SharedSpotSymbol>> GetSpotSymbolsAsync(string exchange)
        {
            var request = new GetSymbolsRequest(TradingMode.Spot);
            var result = await _client.GetSpotSymbolsAsync(exchange, request);
            return result.Data.Where(s => s.QuoteAsset == "USDT" && s.Trading == true);
        }

        public async Task<IEnumerable<SharedFuturesSymbol>> GetFuturesSymbolsAsync(string exchange)
        {
            var request = new GetSymbolsRequest(TradingMode.PerpetualLinear);
            var result = await _client.GetFuturesSymbolsAsync(exchange, request);
            return result.Data.Where(s => s.QuoteAsset == "USDT" && s.Trading == true);
        }
    }
}
