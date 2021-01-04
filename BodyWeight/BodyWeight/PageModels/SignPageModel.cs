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
using System.Net.Mail;
using BodyWeight.Exceptions;
using BodyWeight.Events;

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
            if (formIsValid())
            {
                try
                {
                    var uid = await FreshIOC.Container.Resolve<IAuth>().RegisterWithEmailAndPassword(Email, Password);

                    MessagingCenter.Send<CorrectRegisterEvent>(new CorrectRegisterEvent(), "Correct register");
                    List<Plan> listOfPlans = new List<Plan>();
                    List<Training> listOfTrainings = new List<Training>();                 
                    DatabaseMethods.WriteUserToDataBase(uid, Email, Password, Name, listOfPlans, listOfTrainings);

                    await FreshIOC.Container.Resolve<IAuth>().LoginWithEmailAndPassword(Email, Password);
                    await CoreMethods.PushPageModel<StartingPageModel>();
                    CoreMethods.RemoveFromNavigation<MainPageModel>();
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("EMAIL_EXISTS"))
                    {
                        await App.Current.MainPage.DisplayAlert("Warning","Email is already taken", "Ok");
                    }
                    if (e.Message.Contains("WeakPassword"))
                    {
                        await App.Current.MainPage.DisplayAlert("Warning", "Password is too short", "Ok");
                    }
                
                }
            }
            else
            {                
               // await App.Current.MainPage.DisplayAlert("Warning", "Password has to be longer than 6 characters or You tried to register with invalid email", "Ok");
            }
            

        });

        private bool formIsValid()
        {
            bool emailIsValid = validateEmail();
            bool passwordIsValid = validatePassword();

            if(emailIsValid && passwordIsValid)
            {
                return true;
            }
            return false;
        }

        private bool validatePassword()
        {
            try
            {
                if(Password.Length >= 6)
                {
                    return true;
                }
                else
                {
                    throw new ShortPasswordException();                 
                }
            
            }
            catch (ShortPasswordException s)
            {              
                App.Current.MainPage.DisplayAlert("Warning","Password is too short","OK");
                return false;
            }
            
        }

        private bool validateEmail()
        {
            try
            {
                MailAddress mail = new MailAddress(Email);
            }
            catch (FormatException e)
            {
                App.Current.MainPage.DisplayAlert("Warning", "Email format is not correct", "OK");
                return false;
            }
            catch(ArgumentException e)
            {
                return false;
            }

            return true;

        }

        public Command GoBackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel();
        });
 


         
        public Thickness MarginThreeZero = new Thickness(30, 0, 0, 0);
        public Thickness MarginEntry = new Thickness(50, 0, 50, 0);
    }
}
