using System;
using System.Collections.Generic;
using System.Text;
using FreshMvvm;
using Xamarin.Forms;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using BodyWeight.Models;
using BodyWeight.Helpers;

namespace BodyWeight.PageModels
{
    public class LoginPageModel : FreshBasePageModel
    {
        public string webApiKey = "AIzaSyDjGLLGY1sWENpq0S07OGvkDm6WyetxyJA";
        public Thickness LoginMargin { get; set; } = new Thickness(50, 0, 50, 0);

        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";

        public Command LogInCommand => new Command(async() =>
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            try
            {
                string email = TextConverter.PrepareEmail(Email);              
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email,Password);
                
                var content = await auth.GetFreshAuthAsync();
                var serializedContent = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedContent);
                await CoreMethods.PushPageModel<StartingPageModel>();
            }
            catch (Exception e)
            {

                await App.Current.MainPage.DisplayAlert("Allert", e.Message, "Ok");
            }



            
        });

        

       





    }
}
