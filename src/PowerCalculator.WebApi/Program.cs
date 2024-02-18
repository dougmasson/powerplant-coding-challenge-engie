using PowerCalculator.Application;
using PowerCalculator.Infrastructure;
using PowerCalculator.WebApi.Configurations;
using PowerCalculator.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Layers of Clean Architecture
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Host.AddSerilog();

builder.Services.AddControllers()
                .AddJsonOptions(JsonOptionsConfiguration.JsonOptions());

builder.Services.AddFluentValidation();

builder.Services.AddApiVersioningOptions();

builder.Services.AddResponseCompressionOptions();

builder.Services.AddSwaggerConfiguration();

builder.Services.AddExceptionHandlers();

var app = builder.Build();

app.UseSerilogRequest();

app.UseSwaggerConfiguration();

app.UseCustomMiddleware();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();


/// <summary>
/// Class PowerCalculator.WebApi for IntegrationTests.
/// </summary>
public partial class Program { }
