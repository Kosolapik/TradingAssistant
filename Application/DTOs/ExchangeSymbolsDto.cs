namespace TradingAssistant.Application.DTOs;

public record ExchangeSymbolsDto(int Count, IReadOnlyCollection<SymbolDto> Symbols);