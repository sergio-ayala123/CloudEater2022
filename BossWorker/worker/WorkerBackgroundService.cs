using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Shared;


namespace worker
{
    public class WorkerBackgroundService : BackgroundService
    {
        private readonly IServer server;
        private readonly IConfiguration config;
        private readonly WorkerState workerstate;
        private readonly ILogger<WorkerBackgroundService> logger;
        private readonly IHostApplicationLifetime lifetime;

        public WorkerBackgroundService(IServer server, IConfiguration config, WorkerState workerstate, ILogger<WorkerBackgroundService> logger, IHostApplicationLifetime lifetime)
        {
            this.server = server;
            this.config = config;
            this.workerstate = workerstate;
            this.logger = logger;
            this.lifetime = lifetime;
            logger.LogInformation("In worker background service constructor");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("In worker background service execute async");
            var isStarted = await WaitForAppStartup(lifetime, stoppingToken);
            if (!isStarted)
            {
                return;
            }
            await enlistWithBoss();
        }

        static async Task<bool> WaitForAppStartup(IHostApplicationLifetime lifetime, CancellationToken stoppingToken)
        {
            var startedSource = new TaskCompletionSource();
            lifetime.ApplicationStarted.Register(() => startedSource.SetResult());
            var cancelledSource = new TaskCompletionSource();
            stoppingToken.Register(() => cancelledSource.SetResult());

            Task completedTask = await Task.WhenAny(startedSource.Task, cancelledSource.Task);

            // If the completed tasks was the "app started" task, return true, otherwise false
            return completedTask == startedSource.Task;
        }

        private async Task enlistWithBoss()
        {
            var addressFeatures = server.Features.Get<IServerAddressesFeature>();

            var httpAddress = addressFeatures.Addresses.First(a => a.Contains("http://"));
            var uri = new Uri(httpAddress);
            var host = uri.Host;
            var port = uri.Port;

            var enlistRequest = new EnlistRequest(host, port);
            var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync($"{config["BOSS"]}/enlist", enlistRequest);
            workerstate.Token = await response.Content.ReadAsStringAsync();

            logger.LogInformation("Token is {token}", workerstate.Token);
        }
    }
}