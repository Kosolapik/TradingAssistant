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

// Добавляем сервисы Infrastructure (БД)
builder.Services.AddInfrastructure();

// Настройка OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TradingAssistant API", Version = "v1" });
});

var app = builder.Build();

// Включаем Swagger UI всегда (можно обернуть в if (app.Environment.IsDevelopment()))
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TradingAssistant API v1");
});

app.UseHttpsRedirection();

// Ваши эндпоинты
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
            : Results.NotFound("Активные торговые пары не найдены");
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: $"Ошибка при получении списка символов: {ex.Message}",
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
            : Results.NotFound("Активные торговые пары не найдены");
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: $"Ошибка при получении списка символов: {ex.Message}",
            statusCode: StatusCodes.Status500InternalServerError);
    }
});

app.Run();