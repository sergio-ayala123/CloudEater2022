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

        Task<string> JoinGame(string playerName);
        Task<string> Move(string direction, string password);
        Task<IEnumerable<Player>> GetScore();
    }
    public class JoinGameService : IGameService
    {
        private readonly HttpClient httpClient = new HttpClient();
        private const string ServerUrl = "http://20.106.99.13";
        public async Task<IEnumerable<Player>> GetScore()
        {

            return await httpClient.GetFromJsonAsync<IEnumerable<Player>>("http://localhost:5080/GetScore");

        }

        public async Task<string> JoinGame(string name)
        {
            return await httpClient.GetStringAsync($"{ServerUrl}/JoinGame?playerName={name}");

        }

        public async Task<string> Move(string direction, string password)
        {
            return await httpClient.GetStringAsync($"{ServerUrl}/Move?direction={direction}&&password={password}");
        }
    }
}
