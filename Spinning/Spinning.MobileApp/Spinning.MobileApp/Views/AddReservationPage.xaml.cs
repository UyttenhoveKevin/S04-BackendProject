using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spinning.MobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddReservationPage : ContentPage
	{
		public AddReservationPage ()
		{
			InitializeComponent ();
            LogoVipFit.Source = FileImageSource.FromResource("Spinning.MobileApp.Images.LogoVipFit2.jpg");
            ProfileIcon.Source = FileImageSource.FromResource("Spinning.MobileApp.Images.ProfileIcon.png");
            HouseIcon.Source = FileImageSource.FromResource("Spinning.MobileApp.Images.HouseIcon.png");
            AddIcon.Source = FileImageSource.FromResource("Spinning.MobileApp.Images.AddIcon.png");
        }
	}
}