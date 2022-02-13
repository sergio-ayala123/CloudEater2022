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
    public partial class BotViewModel : ObservableObject
    {
        private readonly IGameService gameService;

        public BotViewModel(IGameService gameService = null)
        {
            this.gameService = gameService ?? DependencyService.Get<IGameService>();
        }

        [ObservableProperty]
        public string botName;

        [ObservableProperty]
        public string botPassword;
        [ObservableProperty]
        public string labelText;
        [ObservableProperty]
        public int score;
        [ObservableProperty]
        public int moves;

        public int position { get; set; }
        public bool flag = false;


        [ICommand]
        public async Task JoinGameAsBot()
        {
            await gameService.JoinGame(BotName, "secretpassword");

            for(int i = 0; i < 20; i++)
            {

            var result = await gameService.MoveBot(BotName,"secretpassword");
            Score = result.occupiedBy.score;
            }

            

        }


    }
              
    }
             
           
            
         

    

