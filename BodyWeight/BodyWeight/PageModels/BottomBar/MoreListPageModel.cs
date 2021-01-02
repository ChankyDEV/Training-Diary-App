using System;
using System.Collections.Generic;
using System.Text;
using BodyWeight.PageModels.ProfileAndHistory;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BodyWeight.PageModels
{
   
    public partial class StartingPageModel : FreshBasePageModel
    {

        public Command LogOutCommand => new Command(async() =>
        {
            Preferences.Remove("MyFirebaseRefreshToken");
            await App.Current.MainPage.Navigation.PopToRootAsync();

        });

        public Command HistoryCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<HistoryPageModel>();

        });

        public Command ProfileCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<ProfilePageModel>();

        });
    }
}
