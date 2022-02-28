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
        public IEnumerable<Cell> positions { get; set; }
        public IList<Cell> list { get; set; }

        public MoveBotController(IStateService state, HttpClient httpClient)
        {
            this.state = state;
            this.httpClient = httpClient;
        }




        [HttpGet(Name = "MoveBot")]
        public async Task<Cell> MoveBot(string botName, string password)
        {
            
            Cell finalPosition  = new Cell();
            for (int i = 0; i < 10100; i++)
            {
            MoveResult botmove = new MoveResult();


                positions = await httpClient.GetFromJsonAsync<IEnumerable<Cell>>("https://hungrygame.azurewebsites.net/board");
                list = positions.ToList();


                Cell current = list.FirstOrDefault(x => x.occupiedBy != null && x.occupiedBy.name == botName);

                if (current == null)
                {
                    return current;
                    break;
                }
                else
                {
                    Cell down = list.FirstOrDefault(x => x.location.column == current.location.column && x.location.row == current.location.row + 1);
                    Cell up = list.FirstOrDefault(x => x.location.column == current.location.column && x.location.row == current.location.row - 1);
                    Cell left = list.FirstOrDefault(x => x.location.column == current.location.column - 1 && x.location.row == current.location.row);
                    Cell right = list.FirstOrDefault(x => x.location.column == current.location.column + 1 && x.location.row == current.location.row);

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

                    else if ((up == null ||up.isPillAvailable == false) && (down == null || down.isPillAvailable == false) && (left == null ||left.isPillAvailable == false) )
                    {
                        botmove = await httpClient.GetFromJsonAsync<MoveResult>($"https://hungrygame.azurewebsites.net/move/right/?token={state.Token}");
                        list = null;

                        //return current;
                    }
                    else if((up == null || up.isPillAvailable == false) && (down == null || down.isPillAvailable == false) && (right == null || right.isPillAvailable == false) && ((current.location.row == 0 && current.location.column == 44) || (current.location.row == 29 && current.location.column == 44)))
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
