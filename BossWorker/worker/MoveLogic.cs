using Shared;
using System.Linq;
namespace worker
{
    public class MoveLogic
    {
        private readonly HttpClient httpClient;
        private readonly WorkerState workerstate;

        public MoveLogic(HttpClient httpClient, WorkerState workerstate)
        {
            this.httpClient = httpClient;
            this.workerstate = workerstate;
        }



        internal async Task Move(Location destination)
        {

            var allCells = await httpClient.GetFromJsonAsync<List<Cell>>($"https://hungrygame.azurewebsites.net/board");
            Cell current = allCells.FirstOrDefault(a => a.occupiedBy != null && a.occupiedBy.name == "localhost");

            int currRow = current.location.row;
            int currCol = current.location.column;


            if (currRow < destination.row)
            {
                for (int x = 0; x < destination.row - currRow; x++)
                {
                    await httpClient.GetStringAsync($"https://hungrygame.azurewebsites.net/move/down/?token={workerstate.Token}");
                }
            }
            else
            {
                for (int x = 0; x < currRow - destination.row; x++)
                {
                    await httpClient.GetStringAsync($"https://hungrygame.azurewebsites.net/move/up/?token={workerstate.Token}");

                }
            }

            if (currCol < destination.column)
            {
                for (int x = 0; x < destination.column - currCol; x++)
                {
                    await httpClient.GetStringAsync($"https://hungrygame.azurewebsites.net/move/right/?token={workerstate.Token}");

                }
            }
            else
            {
                for (int x = 0; x < currCol - destination.column; x++)
                {
                    await httpClient.GetStringAsync($"https://hungrygame.azurewebsites.net/move/left/?token={workerstate.Token}");

                }
            }


        }
        
    }
}
