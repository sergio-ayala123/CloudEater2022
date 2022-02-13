using Microsoft.AspNetCore.Mvc;
using MobileEater.Models;

namespace EaterAPI2022.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoveBotController : ControllerBase
    {
        private readonly IStateService state;
        private readonly HttpClient httpClient;

        public MoveBotController(IStateService state, HttpClient httpClient)
        {
            this.state = state;
            this.httpClient = httpClient;
        }

        [HttpGet(Name ="MoveBot")]
        public async Task<MoveResult> MoveBot(string direction)
        {
            return  await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/{direction}/?token={state.Token}");

        }
    }
}
