using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using MobileEater.Models;
namespace MobileEater.Services
{
    public interface IGameService
    {

        Task<string> JoinGame(string playerName, string password);
        Task<string> Move(string direction, string password);
        Task<IEnumerable<Board>> GetBoard();
    }
    public class JoinGameService : IGameService
    {
        private readonly HttpClient httpClient = new HttpClient();
        private const string ServerUrl = "https://cloudeater2022.azurewebsites.net/";
        

        public async Task<string> JoinGame(string name, string password)
        {
            return await httpClient.GetStringAsync($"{ServerUrl}/JoinGame?playerName={name}&&password={password}");

        }

        public async Task<string> Move(string direction, string password)
        {
            return await httpClient.GetStringAsync($"{ServerUrl}/Move?direction={direction}&&password={password}");
        }


        public Task<IEnumerable<Board>> GetBoard()
        {
             return httpClient.GetFromJsonAsync<IEnumerable<Board>>("https://hungrygame.azurewebsites.net/board");
           
        }

    }
    public class Player
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Score { get; set; }
    }
}
