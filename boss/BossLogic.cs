using Shared;


namespace boss
{
    public class BossLogic
    {
        public ILogger<BossLogic> logger { get; }
        public IConfiguration config { get; }
        private readonly HttpClient httpClient;

        public List<string> Workers { get; set; } = new();

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
                return null;
            }
            var server = config["SERVER"];

            var allCells = await httpClient.GetFromJsonAsync<List<Cell>>($"{server}/board");
            return allCells;
        }
        internal async Task<string> Join(string workerName,string password)
        {

            logger.LogInformation("worker: {worker} wants to join", workerName);
            var server = config["SERVER"];
            string token = await httpClient.GetStringAsync($"{server}/join?playerName={workerName}");

            Workers.Add(workerName);
            return token;
        }
    }
    


    
}
