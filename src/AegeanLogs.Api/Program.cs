using AegeanLogs.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddInfrastruction(builder.Configuration);
var app = builder.Build();
