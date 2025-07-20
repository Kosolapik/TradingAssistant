using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradingAssistant.Core.Entities;
using TradingAssistant.Infrastructure;
using TradingAssistant.Infrastructure.DataBase.MySQL;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������� Infrastructure (��)
builder.Services.AddInfrastructure();

// ��������� OpenAPI
builder.Services.AddOpenApi();  // ������ AddSwaggerGen() � .NET 9

var app = builder.Build();

// �������� OpenAPI � Development
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();  // ������ UseSwaggerUI() � .NET 9
}

app.UseHttpsRedirection();

// ���� ���������
app.MapGet("/", () => "TradingAssistant API is running!");

app.MapGet("/exchanges", async ([FromServices] AppDbContext dbContext) =>
{
    var exchanges = await dbContext.Exchanges.ToListAsync();
    return Results.Ok(exchanges);
});

app.Run();