var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddOpenApi();

// 🔹 Add CORS to allow frontend requests
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://red-forest-009063e00.4.azurestaticapps.net") // your frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// 🔹 Enable CORS middleware before other middleware
app.UseCors();

// Enable Swagger only in Development
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Weather summaries
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", 
    "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// Minimal API endpoint for Azure
app.MapGet("/api/WeatherForecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

// Record type for WeatherForecast
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}