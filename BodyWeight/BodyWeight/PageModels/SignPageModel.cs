using System;
using System.Collections.Generic;
using System.Text;
using FreshMvvm;
using Xamarin.Forms;
using BodyWeight.Models;
using BodyWeight.Helpers;
using Firebase.Auth;
using Firebase;
using PropertyChanged;
using Firebase.Database;
using Firebase.Database.Query;

namespace BodyWeight.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public partial class MainPageModel 
    {
        public string webApiKey = "AIzaSyDjGLLGY1sWENpq0S07OGvkDm6WyetxyJA";

      

        public string Name { get; set; } = "";


        public Account Account { get; set; }

        public Command SignUpCommand => new Command(async () =>
        {

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
               
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email,Password);
                List<Plan> listOfPlans = new List<Plan>();
                List<Training> listOfTrainings = new List<Training>();
             
                
                DatabaseMethods.WriteUserToDataBase(auth.User.LocalId, Email, Password, Name, listOfPlans, listOfTrainings);              
                string getToken = auth.FirebaseToken;
                

            }
            catch (Exception e)
            {

                await App.Current.MainPage.DisplayAlert("Allert", e.Message, "Ok");
            }

        });

        

        public Command GoBackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel();
        });
 


         
        public Thickness MarginThreeZero = new Thickness(30, 0, 0, 0);
        public Thickness MarginEntry = new Thickness(50, 0, 50, 0);
    }
}
