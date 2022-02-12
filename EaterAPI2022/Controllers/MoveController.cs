using Microsoft.AspNetCore.Mvc;

namespace EaterAPI2022.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoveController : ControllerBase
    {

        private readonly HttpClient httpClient;
        private readonly IStateService state;
        public MoveController(HttpClient httpClient, IStateService state)
        {
            this.httpClient = httpClient;
            this.state = state;
        }



        [HttpGet(Name = "Direction")]
        public async Task<int> Move(string direction, string password)
        {
            
            if(password != state.password)
            {
                return state.EatenPills;
            }
            else
            {
            string test = await httpClient.GetStringAsync($"https://hungrygame.azurewebsites.net/move/{direction}/?token={state.Token}");
            if (test.Contains("true"))
            {
                state.EatenPills += 1;
                return state.EatenPills;
            }
            else return state.EatenPills;
            }
        
            }
    }
}