using Microsoft.AspNetCore.Mvc;
using MobileEater.Models;

namespace EaterAPI2022.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoveBotController : Controller
    {
        private readonly IStateService state;
        private readonly HttpClient httpClient;
        public IEnumerable<Board> positions { get; set; }
        public IList<Board> list;

        public MoveBotController(IStateService state, HttpClient httpClient)
        {
            this.state = state;
            this.httpClient = httpClient;
        }

        [HttpGet(Name ="MoveBot")]
        public async Task<MoveResult> MoveBot(string direction, string password)
        {




            MoveResult botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/{direction}/?token={state.Token}");






            positions = await httpClient.GetFromJsonAsync<IEnumerable<Board>>("https://hungrygame.azurewebsites.net/board");
            list = positions.ToList();
            int currentRow = botmove.newLocation.row;
            int currentColumn = botmove.newLocation.column;

            Board current = list.Where(x => x.location.column == currentColumn && x.location.row == currentRow).FirstOrDefault();

            Board down = list.Where(x => x.location.column == current.location.column && x.location.row == current.location.row + 1).FirstOrDefault();
            Board up = list.Where(x => x.location.column == current.location.column && x.location.row == current.location.row - 1).FirstOrDefault();
            Board left = list.Where(x => x.location.column == current.location.column - 1 && x.location.row == current.location.row).FirstOrDefault();
            Board right = list.Where(x => x.location.column == current.location.column + 1 && x.location.row == current.location.row).FirstOrDefault();

            if (up.isPillAvailable == true & (up.location.row > 0 & up.location.row < 29) & (up.location.column > 0 & up.location.column < 45))
            {
                botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/{direction}/?token={state.Token}");
            }
            else if (down.isPillAvailable == true & (down.location.row > 0 & down.location.row < 29) & (down.location.column > 0 & down.location.column < 45))
            {
                botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/{direction}/?token={state.Token}");

            }
            else if (left.isPillAvailable == true & (left.location.row > 0 & left.location.row < 29) & (left.location.column > 0 & left.location.column < 45))
            {
                botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/{direction}/?token={state.Token}");

            }
            else if (right.isPillAvailable == true & (right.location.row > 0 & right.location.row < 29) & (right.location.column > 0 & right.location.column < 45))
            {
                botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/{direction}/?token={state.Token}");

            }
            
            //Score = current.occupiedBy.score;
            return  await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/{direction}/?token={state.Token}");
            


            

        }
    }
}
