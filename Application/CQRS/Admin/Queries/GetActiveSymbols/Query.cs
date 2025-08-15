using CryptoExchange.Net.SharedApis;
using TradingAssistant.Application.DTOs;

namespace TradingAssistant.Application.CQRS.Admin.Queries.GetActiveSymbols;
public record Query (string exchange, string marketType) : IQuery<IEnumerable<SharedSpotSymbol>>;