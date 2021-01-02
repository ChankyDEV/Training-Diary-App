using Xamarin.Forms;
using BodyWeight.Helpers;

namespace BodyWeight.PageModels
{
    public partial class MainPageModel
    {
        public Command LogInCommand => new Command(async() =>
        {
            if (formIsValid())
            {
                string email = TextConverter.PrepareEmail(Email);
                await authenticate.LoginWithEmailAndPassword(email, Password);
                await CoreMethods.PushPageModel<StartingPageModel>();
            }
            
        });

    }
}
