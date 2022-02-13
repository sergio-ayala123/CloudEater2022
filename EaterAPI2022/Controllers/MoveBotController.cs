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
        public IList<Board> list { get; set; }

        public MoveBotController(IStateService state, HttpClient httpClient)
        {
            this.state = state;
            this.httpClient = httpClient;
        }

        [HttpGet(Name ="MoveBot")]
        public async Task<Board> MoveBot(string botName, string password)
        {
            MoveResult botmove = new MoveResult(); 

            positions = await httpClient.GetFromJsonAsync<IEnumerable<Board>>("https://hungrygame.azurewebsites.net/board");
            list = positions.ToList();
            

            Board current = list.Where(x => x.occupiedBy != null && x.occupiedBy.name == botName).FirstOrDefault();

            Board down = list.Where(x => x.location.column == current.location.column && x.location.row == current.location.row + 1).FirstOrDefault();
            Board up = list.Where(x => x.location.column == current.location.column && x.location.row == current.location.row - 1).FirstOrDefault();
            Board left = list.Where(x => x.location.column == current.location.column - 1 && x.location.row == current.location.row).FirstOrDefault();
            Board right = list.Where(x => x.location.column == current.location.column + 1 && x.location.row == current.location.row).FirstOrDefault();

            if (up!= null && up.isPillAvailable == true & (up.location.row > 0 & up.location.row < 29) & (up.location.column > 0 & up.location.column < 45))
            {
                botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/up/?token={state.Token}");

                return current;
            }
            else if (down!= null && down.isPillAvailable == true & (down.location.row > 0 & down.location.row < 29) & (down.location.column > 0 & down.location.column < 45))
            {
                botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/down/?token={state.Token}");

                return current;
            }
            else if (left!=null && left.isPillAvailable == true & (left.location.row > 0 & left.location.row < 29) & (left.location.column > 0 & left.location.column < 45))
            {
                botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/left/?token={state.Token}");

                return current;
            }
            else if (right!= null && right.isPillAvailable == true & (right.location.row > 0 & right.location.row < 29) & (right.location.column > 0 & right.location.column < 45))
            {
                botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/right/?token={state.Token}");

                return current;
            }



            list = null;
            //Score = current.occupiedBy.score;
            return new Board();
            


            

        }
    }
}
