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




        [HttpGet(Name = "MoveBot")]
        public async Task<Board> MoveBot(string botName, string password)
        {

            Board finalPosition  = new Board();
            for (int i = 0; i < 1350; i++)
            {
            MoveResult botmove = new MoveResult();
                //var result = await gameService.MoveBot(BotName, "secretpassword");


                positions = await httpClient.GetFromJsonAsync<IEnumerable<Board>>("https://hungrygame.azurewebsites.net/board");
                list = positions.ToList();


                Board current = list.FirstOrDefault(x => x.occupiedBy != null && x.occupiedBy.name == botName);

                if (current == null)
                {
                    return current;
                    break;
                }
                else
                {
                    Board down = list.FirstOrDefault(x => x.location.column == current.location.column && x.location.row == current.location.row + 1);
                    Board up = list.FirstOrDefault(x => x.location.column == current.location.column && x.location.row == current.location.row - 1);
                    Board left = list.FirstOrDefault(x => x.location.column == current.location.column - 1 && x.location.row == current.location.row);
                    Board right = list.FirstOrDefault(x => x.location.column == current.location.column + 1 && x.location.row == current.location.row);

                    if (up != null && up.isPillAvailable == true)
                    {
                        botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/up/?token={state.Token}");
                        list = null;

                        //return current;
                    }
                    else if (down != null && down.isPillAvailable == true)
                    {
                        botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/down/?token={state.Token}");
                        list = null;

                        //return current;
                    }
                    else if (left != null && left.isPillAvailable == true)
                    {
                        botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/left/?token={state.Token}");
                        list = null;

                        //return current;
                    }

                    else if ((up == null ||up.isPillAvailable == false) && (down == null || down.isPillAvailable == false) && (left == null ||left.isPillAvailable == false) && (current.location.row!= 0 || current.location.row!= 29) && (current.location.column!=44 || current.location.column !=44))
                    {
                        botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/right/?token={state.Token}");
                        list = null;

                        //return current;
                    }
                    else if((up == null || up.isPillAvailable == false) && (down == null || down.isPillAvailable == false) && (right == null || right.isPillAvailable == false))
                    {
                        botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/left/?token={state.Token}");
                        list = null;
                    }


                    list = null;
                    //Score = current.occupiedBy.score;
                    //&(right.location.row > 0 & right.location.row < 29) & (right.location.column > 0 & right.location.column < 45)

                }
                finalPosition = current;
            }
            return finalPosition;
                

        }
    }
}
