using MobileEater.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileEater.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BotPage : ContentPage
    {
        public BotPage()
        {
            InitializeComponent();
            BindingContext = new BotViewModel();
        }
    }
}