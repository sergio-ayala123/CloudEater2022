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

app.MapPost("/enlist", async (EnlistRequest enlist, ILogger<Program> logger,HttpClient httpClient, BossLogic bosslogic, HttpRequest request) =>
{
    var sender = request.HttpContext.Connection.RemoteIpAddress.ToString().Split(':').Last();
    var senderHost = $"http://{sender}";
    SenderHostInfo senderInfo = new SenderHostInfo();


    logger.LogInformation($"Received {senderHost}");
    var token = await bosslogic.Join(senderHost, "secretpassword");
    senderInfo.senderHost = senderHost;
    senderInfo.Token = token;

    return senderInfo;
});

app.MapGet("/start", async (string password, BossLogic bosslogic, HttpClient httpClient, IConfiguration config) =>
{
    var server = config["SERVER"];
    var state = await httpClient.GetStringAsync($"{server}/state");

    List<Cell> cells = await bosslogic.StartRunning(password);

    Random rnd = new Random();

    foreach(var item in bosslogic.Workers)
    {
        int randLocation = rnd.Next(0, 22500);
        await httpClient.PostAsJsonAsync($"{item.WorkerName}/move", cells[randLocation].location);
    }
    return state;
});


app.MapGet("/status",(BossLogic bosslogic, HttpClient httpClient) =>
{
    return bosslogic.Workers;
});


app.MapGet("/done", async (string workerName, BossLogic bosslogic, HttpClient httpClient)=>
{
    await bosslogic.Done(workerName);
});



app.Run();