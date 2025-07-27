using Microsoft.OpenApi.Models;
using TradingAssistant.Application;
using TradingAssistant.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// ��������� ������� Infrastructure (��)
builder.Services.AddApplication();

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
app.MapAdminEndpoints();
app.MapUserEndpoints();

app.Run();