using TradingAssistant.Core.Entities;

namespace TradingAssistant.Application.CQRS.Exchange.Queries.GetActiveSymbols;

public record SymbolDto(
    int Id,
    string Name,
    string BaseAsset,
    string QuoteAsset,
    int ExchangeId,
    int MarketTypeId,
    decimal MinTradeQuantity,
    decimal MinNotionalValue,
    decimal? MaxTradeQuantity,
    decimal QuantityStep,
    decimal PriceStep,
    int? QuantityDecimals,
    int? PriceDecimals,
    bool IsActive,
    // Фьючерсные параметры (nullable)
    decimal? ContractSize = null,
    DateTime? DeliveryTime = null,
    decimal? MaxShortLeverage = null,
    decimal? MaxLongLeverage = null)
{
    // Опционально: метод для маппинга из сущности
    public static SymbolDto FromEntity(Symbol entity) => new(
        entity.Id,
        entity.Name,
        entity.BaseAsset,
        entity.QuoteAsset,
        entity.ExchangeId,
        entity.MarketTypeId,
        entity.MinTradeQuantity,
        entity.MinNotionalValue,
        entity.MaxTradeQuantity,
        entity.QuantityStep,
        entity.PriceStep,
        entity.QuantityDecimals,
        entity.PriceDecimals,
        entity.IsActive,
        entity.ContractSize,
        entity.DeliveryTime,
        entity.MaxShortLeverage,
        entity.MaxLongLeverage);
}

