using BancoApis.IoC;
using BancoApis.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var host = Host.CreateDefaultBuilder(args);

// Add services to the container.
builder.Services.AddDependenciesIoC(configuration);
builder.Services.AddDependenciesIoCAsync();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseErrorHandlerMiddleware();

app.MapControllers();

app.Run();
