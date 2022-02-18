namespace boss
{
    public class BossLogic
    {
        public BossLogic(ILogger<BossLogic> logger, IConfiguration config)
        {
            this.logger = logger;
            this.httpClient = new HttpClient();
            this.config = config;
        }

        public ILogger<BossLogic> logger { get; }
        public IConfiguration config { get; }
        private readonly HttpClient httpClient;

        internal async Task StartRunning(string password)
        {
            logger.LogInformation("BossLogic got call to start running");

            if(password != config["PASSWORD"])
            {
                logger.LogWarning("Wrong Password");
                return;
            }
            var server = config["SERVER"] ?? "https://hungrygame.azurewebsites.net";

            var board = await httpClient.GetFromJsonAsync<List<Cell>>($"{server}/board");
            // call server/getBoard here
        }
    }

    public class Cell
    {
        public Location location { get; set; }
        public bool isPillAvailable { get; set; }
        public OccupiedBy occupiedBy { get; set; }
    }

    public class OccupiedBy
    {
        public int id { get; set; }
        public string name { get; set; }
        public int score { get; set; }
    }
    public class Location
    {
        public int row { get; set; }
        public int column { get; set; }

    }
}
