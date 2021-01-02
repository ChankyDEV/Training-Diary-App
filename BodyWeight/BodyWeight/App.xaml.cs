using FreshMvvm;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BodyWeight.PageModels;
using Xamarin.Essentials;

namespace BodyWeight
{
    public partial class App : Application
    {
       
        public App()
        {
            InitializeComponent();
           
            if(!String.IsNullOrEmpty(Preferences.Get("MyFirebaseRefreshToken", "")))
            {
                MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<StartingPageModel>());
            }
            else
            {
                MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainPageModel>(Id,new MainPageModel(new AuthRepository())));
            }
           
           
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
