using CryptoClients.Net;
using CryptoClients.Net.Enums;
using CryptoClients.Net.Interfaces;
using CryptoClients.Net.Models;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TradingAssistant.Core.Entities;

namespace TradingAssistant.Infrastructure.Exchanges.Crypto
{
    internal class CryptoClient : ICryptoClient
    {
        private readonly IExchangeRestClient _client;

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

        public async Task<Dictionary<string, List<object>>> GetSpotSymbolsAsync(string exchange)
        {
            var request = new GetSymbolsRequest(TradingMode.Spot);
            var result = await _client.GetSpotSymbolsAsync(exchange, request);

            var groupedResult = new Dictionary<string, List<object>>();

            // Проверяем успешность запроса и наличие данных
            if (!result.Success || result.Data == null)
            {
                return groupedResult; // Возвращаем пустой словарь в случае ошибки
            }

            foreach (var symbol in result.Data)
            {

                groupedResult[symbol.Name] = new List<object>();
                groupedResult[symbol.Name].Add(new
                {
                    Pair = symbol.Name,
                    BaseAsset = symbol.BaseAsset,
                    QuoteAsset = symbol.QuoteAsset,
                });
            }

            return groupedResult;
        }
    }
}
