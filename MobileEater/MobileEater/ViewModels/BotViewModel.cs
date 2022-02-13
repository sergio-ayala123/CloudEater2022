using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MobileEater.Models;
using MobileEater.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileEater.ViewModels
{
    public partial class BotViewModel:ObservableObject
    {
        private readonly IGameService gameService;

        public BotViewModel(IGameService gameService = null)
        {
            this.gameService = gameService??DependencyService.Get<IGameService>();
        }

        [ObservableProperty]
        public string botName;

        [ObservableProperty]
        public string botPassword;
        public IList<Board> positions { get; set; }

        [ICommand]
        public async Task JoinGameAsBot()
        {
            if (botPassword != "secretpassword")
            {

            }
            else
            {
                await gameService.JoinGame(BotName, BotPassword);

                foreach(var item in await gameService.GetBoard())
                {
                    positions.Add(item);
                }
                await BotMovement(BotName, BotPassword);
            }
        }
        public async Task BotMovement(string botname, string password)
        {
            for(int x = 0; x < 10; x++)
            {
                await gameService.Move("left", "secretpassword");
                await gameService.Move("up", "secretpassword");
                await gameService.Move("left", "secretpassword");
            }
        }

    }
}
