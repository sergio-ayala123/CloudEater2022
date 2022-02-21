using Microsoft.AspNetCore.Mvc;
using MobileEater.Models;

namespace EaterAPI2022.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetBoardController : ControllerBase
    {
        private readonly HttpClient httpClient;

        private readonly IStateService stateService;

        public GetBoardController(IStateService stateService, HttpClient httpClient)
        {
            this.stateService = stateService;
            this.httpClient = httpClient;
        }

        [HttpGet(Name ="GetBoard")]
        public async Task<IEnumerable<Cell>> GetBoard()
        {
            return await httpClient.GetFromJsonAsync<IEnumerable<Cell>>("https://hungrygame.azurewebsites.net/board");
        }
    }
}
