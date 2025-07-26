using Microsoft.AspNetCore.Mvc;
using TradingAssistant.Infrastructure.Exchanges.Crypto;

namespace TradingAssistant.WebApi.Endpoints
{
    public static class AdminEndpoints
    {
        public static void MapAdminEndpoints(this WebApplication app)
        {
            var adminGroup = app.MapGroup("/admin").WithTags("Admin endpoints");

            adminGroup.MapGet("/crypto-test/{exchange:alpha}/{type:alpha}", async ([FromServices] ICryptoClient client, [FromRoute] string exchange, [FromRoute] string type) =>
            {
                try
                {
                    var symbols = type.ToLower() switch
                    {
                        "spot" or "spots" => (dynamic)await client.GetSpotSymbolsAsync(exchange),
                        "future" or "futures" => (dynamic)await client.GetFuturesSymbolsAsync(exchange),
                        _ => throw new NotSupportedException($"Unsupported trading type: {type}")
                    };

                    return symbols != null
                        ? Results.Ok(new
                        {
                            Count = symbols.Length,
                            Symbols = symbols,
                        })
                        : Results.NotFound("Активные торговые пары не найдены");
                }
                catch (Exception ex)
                {
                    return Results.Problem(
                        detail: $"Ошибка при получении списка символов: {ex.Message}",
                        statusCode: StatusCodes.Status500InternalServerError);
                }
            });
        }
    }
}
