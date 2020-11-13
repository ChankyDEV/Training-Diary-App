using System;
using System.Collections.Generic;
using System.Text;
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
            await CoreMethods.PushPageModel<MainPageModel>();

        });
    }
}
