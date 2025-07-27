

namespace TradingAssistant.Application.CQRS.Exchange.Queries.GetActiveSymbols;
public record Query (string exchange, string marketType) : IQuery<ExchangeSymbolsDto>;
    

