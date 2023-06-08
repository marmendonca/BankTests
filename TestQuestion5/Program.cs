using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Data.SqlClient;
using TestQuestion5.Application.Handlers;
using TestQuestion5.Domain.Interfaces.Repositories;
using TestQuestion5.Infrastructure.Database;
using TestQuestion5.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "API de Movimentação de Conta Corrente", Version = "v1" });
});

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));
builder.Services.AddScoped<IDbConnection>(provider => 
    new SqlConnection(provider.GetRequiredService<IOptions<DatabaseSettings>>()
        .Value
        .DefaultConnection));

builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblies(typeof(MovimentarContaCorrenteCommandHandler).Assembly);
});

builder.Services.AddScoped(typeof(IContaCorrenteRepository), typeof(ContaCorrenteRepository));
builder.Services.AddScoped(typeof(IMovimentoRepository), typeof(MovimentoRepository));
builder.Services.AddScoped(typeof(IIdempotenciaRepository), typeof(IdempotenciaRepository));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
