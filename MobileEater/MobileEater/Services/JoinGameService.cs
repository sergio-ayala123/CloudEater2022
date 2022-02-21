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
        Task<Cell> MoveBot(string botName, string password);
        Task<List<Cell>> GetBoard();
        Task<List<Cell>> Start();
        Task<List<Status>> Status();
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


        public Task<List<Cell>> GetBoard()
        {
             return httpClient.GetFromJsonAsync<List<Cell>>($"{ServerUrl}/GetBoard");
           
        }

        public Task<Cell> MoveBot(string botName, string password)
        {
            return httpClient.GetFromJsonAsync<Cell>($"{ServerUrl}/MoveBot?botName={botName}&&password={password}");
        }

        public Task<List<Cell>> Start()
        {
            return httpClient.GetFromJsonAsync<List<Cell>>($"http://localhost:5162/start?password=secretpassword");
        }

        public Task<List<Status>> Status()
        {
            return httpClient.GetFromJsonAsync<List<Status>>($"http://localhost:5162/status");

        }
    }
    public class Player
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Score { get; set; }
    }
}
