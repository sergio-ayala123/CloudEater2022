using Microsoft.AspNetCore.Mvc;

namespace EaterAPI2022.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JoinGameController : ControllerBase
    {
        private readonly HttpClient httpClient;
        private readonly IStateService state;
        public JoinGameController(HttpClient httpClient, IStateService state)
        {
            this.httpClient = httpClient;
            this.state = state;
        }

        [HttpGet(Name = "JoinGame")]
        public async Task<string> Get(string playerName, string password)
        {
            state.password = password;
            state.Token = await httpClient.GetStringAsync($"https://hungrygame.azurewebsites.net/join?playerName={playerName}");
            return "Joined Successfully";
            
        }
    }
}
