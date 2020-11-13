using System;
using System.Collections.Generic;
using System.Text;
using FreshMvvm;
using Xamarin.Forms;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using BodyWeight.Models;
using System.Collections.ObjectModel;

namespace BodyWeight.PageModels
{
    public partial class StartingPageModel : FreshBasePageModel
    {

        public string webApiKey = "AIzaSyDjGLLGY1sWENpq0S07OGvkDm6WyetxyJA";

       
        public StartingPageModel()
        {
            GetProfileInformationAndRefreshToken();
        
            Plans = new ObservableCollection<Plan>();
            User = new Account();

        
        }

    }
}
