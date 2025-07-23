using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using System.Threading.Tasks;

namespace TradingAssistant.Infrastructure.Exchanges.Binance
{
    public class BinanceClient : IBinanceClient
    {
        private readonly BinanceRestClient _client;

        public BinanceClient()
        {
            _client = new BinanceRestClient(options => {
                options.ApiCredentials = new ApiCredentials("YyvboAPdAnJf4CRA6LJKQrDNKaTFcJM6w6Mirh9xit5xbNhLaPdtZa5Lsba6EyAJ", "jOcMzFtffJHN4jdukppHZ5EpkkTytZYsTUX7yzHqRgIsNe9sgVpjrPlKD2ZHk2ij");
            });
        }

        public BinanceRestClient GetClient ()
        {
            return _client;
        }

        /// <summary>
        /// Получает список всех активных спотовых торговых пар на Binance
        /// </summary>
        /// <returns>Список символов (например "BTCUSDT")</returns>
        public async Task<List<string>> GetSpotSymbolsAsync()
        {
            try
            {
                var response = await _client.SpotApi.ExchangeData.GetExchangeInfoAsync();

                if (!response.Success)
                {
                    Console.WriteLine($"Ошибка при получении данных: {response.Error}");
                    return new List<string>();
                }

                return response.Data.Symbols
                    .Where(s => s.Status == SymbolStatus.Trading)
                    .Select(s => s.Name)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение при запросе символов: {ex.Message}");
                return new List<string>();
            }
        }

        /// <summary>
        /// Получает список торговых пар с фильтрацией по базовому/котируемому активу
        /// </summary>
        /// <param name="baseAsset">Базовый актив (например "BTC")</param>
        /// <param name="quoteAsset">Котируемый актив (например "USDT")</param>
        public async Task<List<string>> GetSpotSymbolsAsync(string? baseAsset = null, string? quoteAsset = null)
        {
            var symbols = await GetSpotSymbolsAsync();

            if (baseAsset != null)
                symbols = symbols.Where(s => s.StartsWith(baseAsset)).ToList();

            if (quoteAsset != null)
                symbols = symbols.Where(s => s.EndsWith(quoteAsset)).ToList();

            return symbols;
        }

        /// <summary>
        /// Получает полную информацию о торговых парах
        /// </summary>
        public async Task<List<BinanceSymbol>> GetFullSpotSymbolInfoAsync()
        {
            var response = await _client.SpotApi.ExchangeData.GetExchangeInfoAsync();
            return response.Success
                ? response.Data.Symbols.Where(s => s.Status == SymbolStatus.Trading).ToList()
                : new List<BinanceSymbol>();
        }
    }
}
