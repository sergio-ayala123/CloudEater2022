using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MobileEater.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MobileEater.Models;

namespace MobileEater.ViewModels
{
    
    public partial class BossViewModel:ObservableObject
    {
        private readonly IGameService gameService;
        public BossViewModel(IGameService gameService=null)
        {
            this.gameService = gameService ?? DependencyService.Get<IGameService>();
            Workers = new ObservableCollection<Status>();
        }

        public ObservableCollection<Status> Workers { get; set; } = new();


        [ObservableProperty]
        public string state;


        [ICommand]
        public async Task Start()
        {
            State = await gameService.Start();
        }


        [ICommand]
        public async Task Status()
        {
            Workers.Clear();
            var workers = await gameService.Status();
            foreach(var worker in workers)
            {           
                Workers.Add(worker);           
            }
        }
    }
}
