using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TradingAssistant.Core.Entities;
using TradingAssistant.Infrastructure;
using TradingAssistant.Infrastructure.DataBase.MySQL;

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
app.MapGet("/", () => "TradingAssistant API is running!");

app.MapGet("/exchanges", async ([FromServices] AppDbContext dbContext) =>
{
    var exchanges = await dbContext.Exchanges.ToListAsync();
    return Results.Ok(exchanges);
});

app.Run();