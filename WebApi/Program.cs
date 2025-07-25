using CryptoExchange.Net.SharedApis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using TradingAssistant.Infrastructure;
using TradingAssistant.Infrastructure.Exchanges.Crypto;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������� Infrastructure (��)
builder.Services.AddInfrastructure();

// ��������� OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TradingAssistant API", Version = "v1" });
});

var app = builder.Build();

// �������� Swagger UI ������ (����� �������� � if (app.Environment.IsDevelopment()))
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TradingAssistant API v1");
});

app.UseHttpsRedirection();

// ���� ���������

app.MapGet("/crypto-test/{exchange:alpha}/{type:alpha}", async ([FromServices] ICryptoClient client, [FromRoute] string exchange, [FromRoute] string type) =>
{
    try
    {
        var symbols = type.ToLower() switch
        {
            "spot" or "spots" => (dynamic)await client.GetSpotSymbolsAsync(exchange),
            "future" or "futures" => (dynamic)await client.GetFuturesSymbolsAsync(exchange),
            _ => throw new NotSupportedException($"Unsupported trading type: {type}")
        };

        return symbols.Success
            ? Results.Ok(new
            {
                Count = symbols.Data.Length,
                Symbols = symbols.Data
            })
            : Results.NotFound("�������� �������� ���� �� �������");
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: $"������ ��� ��������� ������ ��������: {ex.Message}",
            statusCode: StatusCodes.Status500InternalServerError);
    }
});

app.Run();