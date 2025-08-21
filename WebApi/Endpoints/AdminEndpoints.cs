using CryptoExchange.Net.SharedApis;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GetActiveSymbolsQuery = TradingAssistant.Application.CQRS.Admin.Queries.GetActiveSymbols.Query;
using TestQuery = TradingAssistant.Application.CQRS.Admin.Queries.TEST.Query;

namespace TradingAssistant.WebApi.Endpoints
{
    public static class AdminEndpoints
    {
        public static void MapAdminEndpoints(this WebApplication app)
        {
            var adminGroup = app.MapGroup("/admin").WithTags("Admin endpoints");

            adminGroup.MapGet("/crypto-test/{exchange:alpha}/{type:alpha}",
                async (
                    [FromServices] IMediator mediator,
                    [FromRoute] string exchange,
                    [FromRoute] string type
                ) => {
                    var result = await mediator.Send(new GetActiveSymbolsQuery(exchange, type));
                    return Results.Ok(result);
                }
            ).Produces<IEnumerable<SharedSpotSymbol>>();

            adminGroup.MapGet("/test",
                async (
                    [FromServices] IMediator mediator
                ) => {
                    var result = await mediator.Send(new TestQuery());
                    return Results.Ok(result);
                }
            ).Produces<IEnumerable<IAssetsRestClient>>();
        }
    }
}