using Binance.Net.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TradingAssistant.Core.Entities;
using TradingAssistant.Infrastructure;
using TradingAssistant.Infrastructure.DataBase.MySQL;
using TradingAssistant.Infrastructure.Exchanges.ByBit;
using TradingAssistant.Infrastructure.Exchanges.Binance;

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
app.MapGet("/binance-test", async ([FromServices] IBinanceClient symbolService) =>
{
    try
    {
        var symbols = await symbolService.GetSpotSymbolsAsync(null, "USDT");

        return symbols.Any()
            ? Results.Ok(new
            {
                Count = symbols.Count,
                Symbols = symbols
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

app.MapGet("/bybit-test", async ([FromServices] IByBitClient symbolService) =>
{
    try
    {
        var symbols = await symbolService.GetSpotSymbolsAsync(null, "USDT");

        return symbols.Any()
            ? Results.Ok(new
            {
                Count = symbols.Count,
                Symbols = symbols
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