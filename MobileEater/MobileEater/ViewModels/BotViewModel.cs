using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MobileEater.Models;
using MobileEater.Services;
using System;
using System.Linq;
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
        [ObservableProperty]
        public string labelText;
        public int position { get; set; }
        public IEnumerable<Board> positions { get; set; }

        [ICommand]
        public async Task JoinGameAsBot()
        {

            
                await gameService.JoinGame(BotName, "secretpassword");

                LabelText = await BotMovement(BotName, "secretpassword");
        }
        public async Task<string> BotMovement(string botname, string password)
        {
            positions = await gameService.GetBoard();
            IList<Board> list = positions.ToList();


            //position = list[0].occupiedBy.score;
                


            for (int x = 0; x < 10; x++)
            {
                await gameService.Move("left", "secretpassword");
                await gameService.Move("up", "secretpassword");
                await gameService.Move("left", "secretpassword");
            }
            return "moving";
        }

    }
}
