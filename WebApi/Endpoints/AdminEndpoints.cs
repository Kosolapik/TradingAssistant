using MediatR;
using Microsoft.AspNetCore.Mvc;
using TradingAssistant.Application.CQRS.Exchange.Queries.GetActiveSymbols;

namespace TradingAssistant.WebApi.Endpoints
{
    public static class AdminEndpoints
    {
        public static void MapAdminEndpoints(this WebApplication app)
        {
            var adminGroup = app.MapGroup("/admin").WithTags("Admin endpoints");

            adminGroup.MapGet(
                "/crypto-test/{exchange:alpha}/{type:alpha}",
                async (
                    [FromServices] IMediator mediator,
                    [FromRoute] string exchange,
                    [FromRoute] string type) =>
                {
                    var result = await mediator.Send(new Query(exchange, type));
                    return Results.Ok(result);
                })
                .Produces<ExchangeSymbolsDto>();
        }
    }
}