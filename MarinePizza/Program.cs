using MarinePizza.Services;
using MarinePizza.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add PizzaContext
builder.Services.AddSqlite<PizzaContext>("Data Source=MarinePizza.db");

// Add PromotionsContext

// PizzaService is registered with the ASP.NET Core dependency injection system
builder.Services.AddScoped<PizzaService>();

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

// Add the CreateDbIfNotExists method call

app.MapGet("/", () => @"Pizza management API. Navigate to /swagger to open the Swagger test UI.");

app.Run();

