using CryptoClients.Net.Interfaces;
using CryptoExchange.Net.SharedApis;
using TradingAssistant.Application.DTOs;

namespace TradingAssistant.Application.CQRS.Admin.Queries.TEST;
public record Query () : IQuery<IEnumerable<IAssetsRestClient>>;