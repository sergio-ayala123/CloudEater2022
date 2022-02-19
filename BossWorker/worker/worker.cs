using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Shared;
using worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHostedService<WorkerBackgroundService>();
builder.Services.AddSingleton<WorkerState>();
builder.Services.AddSingleton<MoveLogic>();

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

app.MapPost("/move", async ([FromBody] Location destination, ILogger<Program> logger, MoveLogic movelogic) =>
{
    logger.LogInformation("Going to this destination: row: {row}, column {column}", destination.row, destination.column);
    await movelogic.Move(destination);
});




app.Run();

