using Microsoft.OpenApi.Models;
using TradingAssistant.Application;
using TradingAssistant.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Добавляем сервисы Infrastructure (БД)
builder.Services.AddApplication();

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
app.MapAdminEndpoints();
app.MapUserEndpoints();

app.Run();