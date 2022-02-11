using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MobileEater.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileEater.ViewModels
{
    public partial class GameViewModel : ObservableObject
    {
        private readonly IGameService gameService;

        public GameViewModel(IGameService gameService = null)
        {
            this.gameService = gameService ?? DependencyService.Get<IGameService>(); ;
        }

        [ObservableProperty]
        public bool success = true;

        [ObservableProperty]
        public bool failure = false;
        [ObservableProperty]
        public string playerName;

        [ObservableProperty]
        public string result;

        [ObservableProperty]
        public string serverAddress;

        [ObservableProperty]
        public bool showControls = false;

        [ObservableProperty]
        public string eatenPills;

        [ICommand]
        public async Task JoinGame()
        {
            if (ServerAddress != "https://hungrygame.azurewebsites.net/")
            {
                Failure = true;
                ServerAddress = string.Empty;
            }
            else
            {
                Result = await gameService.JoinGame(PlayerName) + $" As: {PlayerName}";
                Failure = false;
                ShowControls = true; 
                Success = false;
                Players.Add(new Player { Id = 1, Name = PlayerName, Score = 0 });
            }
        }
        public ObservableCollection<object> Players{ get; private set; } = new();

        [ICommand]
        public async Task MoveLeft()
        {
            EatenPills = await gameService.Move("left", "secretpassword");
          
        }
        [ICommand]
        public async Task MoveRight()
        {
            EatenPills = await gameService.Move("right", "secretpassword");
        }
        [ICommand]
        public async Task MoveDown()
        {
            EatenPills = await gameService.Move("down", "secretpassword");
        }

        [ICommand]
        public async Task MoveUp()
        {
            EatenPills = await gameService.Move("up", "secretpassword");
        }
    }
}
    public class Player
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Score { get; set; }
    }
