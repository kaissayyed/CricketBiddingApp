using CricketBiddingApp.Api.Data;
using CricketBiddingApp.Api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IAuctionService, AuctionService>();
builder.Services.AddScoped<CricketBiddingService>();


// PostgreSQL configuration
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection");
builder.Services.AddDbContext<CricketBiddingDbContext>(options =>
    options.UseNpgsql(connectionString));
var app = builder.Build();

// Apply any pending migrations on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<CricketBiddingDbContext>();
        context.Database.Migrate(); // This applies pending migrations
    }
    catch (Exception ex)
    {
        // Log or handle errors during migration
        Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
    }
}


// Swagger setup in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
