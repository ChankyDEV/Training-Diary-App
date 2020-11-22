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
using OxyPlot;
using OxyPlot.Axes;

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

            Model = new PlotModel();
            XAXIS = new DateTimeAxis();
            YAXIS = new LinearAxis();
            CreateAxes();


        }
        async private void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                var savedFirebaseAuth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var RefreshedContent = await authProvider.RefreshAuthAsync(savedFirebaseAuth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));

                DatabaseMethods.authID = savedFirebaseAuth.User.LocalId;
                await UserFunAsync();


            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "GET PROFILE " + e.Message, "ok");
            }

        }
        private async System.Threading.Tasks.Task UserFunAsync()
        {
           
            Session.LoggedUser = await DatabaseMethods.GetUserbyEmail();
            Session.LoggedUser.Plans = await DatabaseMethods.GetPlans();
            
 
            if (Session.LoggedUser.Plans != null)
            {
                GetPlanForSpecficDay();
            }
            else
            {
                Session.LoggedUser.Plans = new List<Plan>();
               
            }

            UpdateDays();
        }


       
    }
}
