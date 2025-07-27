using CryptoExchange.Net.SharedApis;
using MediatR;
using TradingAssistant.Core.Interfaces.Repositories;
using TradingAssistant.Infrastructure.Exchanges.Crypto;
using TradingAssistant.Infrastructure.Repositories;

namespace TradingAssistant.Application.CQRS.Exchange.Queries.GetActiveSymbols;

public class Handler : IRequestHandler<Query, ExchangeSymbolsDto>
{
    private readonly ICryptoClient _client;
    private readonly IExchangeRepository _repositoryExchange;
    private readonly IMarketTypeRepository _repositoryMarketType;

    public Handler(
        ICryptoClient client,
        IExchangeRepository repositoryExchange,
        IMarketTypeRepository repositoryMarketType)
    {
        _client = client;
        _repositoryExchange = repositoryExchange;
        _repositoryMarketType = repositoryMarketType;
    }

    public async Task<ExchangeSymbolsDto> Handle(
        Query query,
        CancellationToken cancellationToken)
    {
        // Получаем символы с биржи
        var symbols = query.marketType switch
        {
            "spot" or "spots" => await _client.GetSpotSymbolsAsync(query.exchange),
            "future" or "futures" => await _client.GetFuturesSymbolsAsync(query.exchange),
            _ => throw new NotSupportedException($"Unsupported type: {query.marketType}")
        };

        // Получаем ID асинхронно один раз
        var exchangeId = await _repositoryExchange.GetExchangeIdAsync(query.exchange, cancellationToken);
        var marketTypeId = await _repositoryMarketType.GetMarketTypeIdAsync(query.marketType, cancellationToken);

        // Конвертируем в DTO
        var symbolDtos = symbols.Select(s =>
        {
            var futuresSymbol = s as SharedFuturesSymbol;

            return new SymbolDto(
                Id: 0,
                Name: s.Name,
                BaseAsset: s.BaseAsset,
                QuoteAsset: s.QuoteAsset,
                ExchangeId: exchangeId, // Используем уже полученное значение
                MarketTypeId: marketTypeId, // Используем уже полученное значение
                                            // Основные параметры
                MinTradeQuantity: s.MinTradeQuantity ?? 0.001m,
                MinNotionalValue: s.MinNotionalValue ?? 10.0m,
                MaxTradeQuantity: s.MaxTradeQuantity,
                QuantityStep: s.QuantityStep ?? 0.0001m,
                PriceStep: s.PriceStep ?? 0.01m,
                QuantityDecimals: s.QuantityDecimals ?? 8,
                PriceDecimals: s.PriceDecimals ?? 2,
                IsActive: true,
                // Фьючерсные параметры
                ContractSize: futuresSymbol?.ContractSize,
                DeliveryTime: futuresSymbol?.DeliveryTime,
                MaxShortLeverage: futuresSymbol?.MaxShortLeverage,
                MaxLongLeverage: futuresSymbol?.MaxLongLeverage
            );
        }).ToList();

        return new ExchangeSymbolsDto(symbolDtos.Count, symbolDtos);
    }
}