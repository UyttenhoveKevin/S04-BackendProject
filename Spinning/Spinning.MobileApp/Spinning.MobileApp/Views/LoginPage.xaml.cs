using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Spinning.MobileApp.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            LogoVipFit.Source = FileImageSource.FromResource("Spinning.MobileApp.Images.LogoVipFit.jpg");
        }
    }
}
