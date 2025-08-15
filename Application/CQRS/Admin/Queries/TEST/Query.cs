using CryptoExchange.Net.SharedApis;

namespace TradingAssistant.Application.CQRS.Admin.Queries.TEST;
public record Query () : IQuery<IEnumerable<IAssetsRestClient>>;