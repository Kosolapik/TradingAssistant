using CryptoExchange.Net.SharedApis;

namespace TradingAssistant.Application.CQRS.Admin.Queries.GetActiveSymbols;
public record Query (string exchange, string marketType) : IQuery<IEnumerable<SharedSpotSymbol>>;