using boss;
using Shared;
using System.Net.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<BossLogic>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapPost("/enlist", async (EnlistRequest enlist, ILogger<Program> logger,HttpClient httpClient, BossLogic bosslogic) =>
{
    logger.LogInformation($"Received {enlist}");
    var token = await bosslogic.Join(enlist.host, "secretpassword");
    return token;
});

app.MapGet("/start", async (string password, BossLogic bosslogic, HttpClient httpClient, IConfiguration config) =>
{
    List<Cell> cells = await bosslogic.StartRunning(password);
    var test = await httpClient.PostAsJsonAsync($"http://localhost:5289/move", cells[0].location);
});



app.Run();

record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}