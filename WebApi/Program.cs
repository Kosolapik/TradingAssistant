using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradingAssistant.Core.Entities;
using TradingAssistant.Infrastructure;
using TradingAssistant.Infrastructure.DataBase.MySQL;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы Infrastructure (БД)
builder.Services.AddInfrastructure();

// Настройка OpenAPI
builder.Services.AddOpenApi();  // Аналог AddSwaggerGen() в .NET 9

var app = builder.Build();

// Включаем OpenAPI в Development
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();  // Аналог UseSwaggerUI() в .NET 9
}

app.UseHttpsRedirection();

// Ваши эндпоинты
app.MapGet("/", () => "TradingAssistant API is running!");

app.MapGet("/exchanges", async ([FromServices] AppDbContext dbContext) =>
{
    var exchanges = await dbContext.Exchanges.ToListAsync();
    return Results.Ok(exchanges);
});

app.Run();