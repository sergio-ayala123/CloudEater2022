using MobileEater.Services;
using MobileEater.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileEater
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<IGameService, JoinGameService>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
