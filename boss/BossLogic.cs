using Shared;


namespace boss
{
    public class BossLogic
    {
        public ILogger<BossLogic> logger { get; }
        public IConfiguration config { get; }
        private readonly HttpClient httpClient;

        public List<Status> Workers { get; set; } = new();

        public BossLogic(ILogger<BossLogic> logger, IConfiguration config)
        {
            this.logger = logger;
            this.httpClient = new HttpClient();
            this.config = config;
        }

        internal async Task<List<Cell>> StartRunning(string password)
        {
            logger.LogInformation("BossLogic got call to start running");

            if(password != config["PASSWORD"])
            {
                logger.LogWarning("Wrong Password");
                //return null;
            }
            var server = config["SERVER"];

            return await httpClient.GetFromJsonAsync<List<Cell>>($"{server}/board");

        }
        internal async Task<string> Join(string workerName,string password)
        {
            Status newWorker = new Status();
            newWorker.WorkerName = workerName;
            logger.LogInformation("worker: {worker} wants to join", workerName);
            var server = config["SERVER"];
            string token = await httpClient.GetStringAsync($"{server}/join?playerName={workerName}");

            Workers.Add(newWorker);
            return token;
        }

        internal async Task Done(string workerName)
        {
            List<Cell> cells = await StartRunning("secretpassword");

            Random rnd = new Random();
            int randLocation = rnd.Next(0, 10100);
            var currentWorkerScore = cells.FirstOrDefault(b => b.occupiedBy != null && b.occupiedBy.name == workerName);

            var currentWorker = Workers.FirstOrDefault(a => a.WorkerName == workerName);

            currentWorker.Destination = cells[randLocation].location;
            currentWorker.Score = currentWorkerScore.occupiedBy.score;
            logger.LogInformation("worker: {worker} has arrived at destination", workerName);
            var newMove = await httpClient.PostAsJsonAsync($"{workerName}/move", cells[randLocation].location);
        }
    }   
}