﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Spinning.MobileApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReservationsOverviewPage : ContentPage
	{
		public ReservationsOverviewPage ()
		{
			InitializeComponent ();
            LogoVipFit.Source = FileImageSource.FromResource("Spinning.MobileApp.Images.LogoVipFit2.jpg");
            ProfileIcon.Source = FileImageSource.FromResource("Spinning.MobileApp.Images.ProfileIcon.png");
            HouseIcon.Source = FileImageSource.FromResource("Spinning.MobileApp.Images.HouseIcon.png");
            AddIcon.Source = FileImageSource.FromResource("Spinning.MobileApp.Images.AddIcon.png");
            LocationIcon.Source = FileImageSource.FromResource("Spinning.MobileApp.Images.LocationIcon.png");
            CalenderIcon.Source = FileImageSource.FromResource("Spinning.MobileApp.Images.CalenderIcon.png");
            ClockIcon.Source = FileImageSource.FromResource("Spinning.MobileApp.Images.ClockIcon.png");
        }
	}
}