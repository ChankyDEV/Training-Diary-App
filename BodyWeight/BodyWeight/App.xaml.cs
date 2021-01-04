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

            FreshIOC.Container.Register<IAuth, AuthRepository>();

            if (!String.IsNullOrEmpty(Preferences.Get("MyFirebaseRefreshToken", "")))
            {
                MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<StartingPageModel>());
            }
            else
            {
                MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<MainPageModel>());
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
