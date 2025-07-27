namespace TradingAssistant.Application.CQRS.Exchange.Queries.GetActiveSymbols;

public record ExchangeSymbolsDto(int Count, IReadOnlyCollection<SymbolDto> Symbols);