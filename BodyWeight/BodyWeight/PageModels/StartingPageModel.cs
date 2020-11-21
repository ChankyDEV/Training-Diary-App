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
using BodyWeight.Events;

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

            CreateActivitesCollection();

            
        
        }
        async private void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                var savedFirebaseAuth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var RefreshedContent = await authProvider.RefreshAuthAsync(savedFirebaseAuth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));


                await UserFunAsync(savedFirebaseAuth.User.Email);


            }
            catch (Exception e)
            {
                App.Current.MainPage.DisplayAlert("Alert", "GET PROFILE " + e.Message, "ok");
            }

        }
        private async System.Threading.Tasks.Task UserFunAsync(string email)
        {
           
            Session.LoggedUser = await DatabaseMethods.GetUserbyEmail(email);

            if (Session.LoggedUser.Plans != null)
            {
                GetPlanForSpecficDay();
            }
            else
            {
                Session.LoggedUser.Plans = new List<Plan>();
                Session.LoggedUser.Trainings = new List<Training>();
            }

            UpdateDays();
        }


        private void CreateActivitesCollection()
        {
            Activites = new List<string>();
            Activites.Add("No activity");
            Activites.Add("Low activity");
            Activites.Add("Medium activity");
            Activites.Add("Active lifestyle");
            Activites.Add("Really active lifestyle");
            Activites.Add("Extreamly active lifestyle");
        }
    }
}
