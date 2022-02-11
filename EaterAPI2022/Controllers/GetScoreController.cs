using Microsoft.AspNetCore.Mvc;
using MobileEater.Models;

namespace EaterAPI2022.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetScoreController : Controller
    {
        private readonly HttpClient httpClient;
        private readonly IStateService state;
        public GetScoreController(HttpClient httpClient, IStateService state)
        {
            this.httpClient = httpClient;
            this.state = state;
        }

        [HttpGet(Name = "GetScore")]
        public async Task<IEnumerable<Player>> GetScore()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Player>>("https://hungrygame.azurewebsites.net/players");
        }
    }
}
