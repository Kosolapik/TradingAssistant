using Binance.Net.Objects.Models.Spot;
using Bybit.Net.Clients;
using Bybit.Net.Enums;
using Bybit.Net.Interfaces;
using Bybit.Net.Objects.Models.V5;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;

namespace TradingAssistant.Infrastructure.Exchanges.ByBit
{
    public class ByBitClient : IByBitClient
    {
        private readonly BybitRestClient _client;

        public ByBitClient ()
        {
            _client = new BybitRestClient (options => {
                options.ApiCredentials = new ApiCredentials("0KfQ79J593XRDIk5Gn", "IFyLmrl903kGcJrBanqVc2dea73vOsNg6B0U");
            });
        }

        public BybitRestClient GetClient()
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
                var response = await _client.V5Api.ExchangeData.GetSpotSymbolsAsync();

                if (!response.Success)
                {
                    Console.WriteLine($"Ошибка при получении данных: {response.Error}");
                    return new List<string>();
                }

                return response.Data.List
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
        public async Task<List<BybitSpotSymbol>> GetFullSpotSymbolInfoAsync()
        {
            var response = await _client.V5Api.ExchangeData.GetSpotSymbolsAsync();
            return response.Success
                ? response.Data.List.Where(s => s.Status == SymbolStatus.Trading).ToList()
                : new List<BybitSpotSymbol>();
        }
    }
}
