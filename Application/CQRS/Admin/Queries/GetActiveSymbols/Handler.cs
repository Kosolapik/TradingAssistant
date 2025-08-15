using CryptoExchange.Net.SharedApis;
using MediatR;
using TradingAssistant.Infrastructure.Exchanges.Crypto;

namespace TradingAssistant.Application.CQRS.Admin.Queries.GetActiveSymbols;

public class Handler : IRequestHandler<Query, IEnumerable<SharedSpotSymbol>>
{
    private readonly ICryptoClient _client;

    public Handler(
        ICryptoClient client
    ){
        _client = client;
    }

    public async Task<IEnumerable<SharedSpotSymbol>> Handle(
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

        return symbols;
    }
}