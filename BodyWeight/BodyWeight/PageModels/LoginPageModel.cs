using Xamarin.Forms;
using BodyWeight.Helpers;
using System;
using FreshMvvm;

namespace BodyWeight.PageModels
{
    public partial class MainPageModel
    {
        public Command LogInCommand => new Command(async() =>
        {
            if (formIsValid())
            {
                string email = TextConverter.PrepareEmail(Email);
                try
                {
                    await FreshIOC.Container.Resolve<IAuth>().LoginWithEmailAndPassword(email, Password);
                    await CoreMethods.PushPageModel<StartingPageModel>();                   
                    CoreMethods.RemoveFromNavigation<MainPageModel>();
                }
                catch(Exception e)
                {
                    if(e.Message.Contains("EMAIL_NOT_FOUND") || e.Message.Contains("INVALID_PASSWORD"))
                    {
                        await App.Current.MainPage.DisplayAlert("Warning", "Invalid email or password", "OK");
                    }
                    if (e.Message.Contains("TOO_MANY_ATTEMPTS_TRY_LATER"))
                    {
                        await App.Current.MainPage.DisplayAlert("Warning", "Too many attemps. Try again later", "OK");
                    }
                    
                }
                
            }
           

        });

    }
}
