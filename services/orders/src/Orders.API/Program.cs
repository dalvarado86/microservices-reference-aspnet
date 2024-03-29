using Ordering.Infrastructure.Persistence;
using Orders.API.Endpoints;
using Orders.API.Extensions;
using Orders.Application;
using Orders.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);
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

app.UseAuthorization();
app.MapControllers();

// Add migration and seed data
app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetRequiredService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
}, retry: 5);

// Order Endpoints definition
app.ConfigureOrderEndpoints();
app.UseHttpsRedirection();

app.Run();
