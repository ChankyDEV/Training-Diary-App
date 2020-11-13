using System;
using System.Collections.Generic;
using System.Text;
using BodyWeight.Helpers;
using FreshMvvm;
using Xamarin.Forms;

namespace BodyWeight.PageModels
{
    public class MainPageModel : FreshBasePageModel     
    {
        public string ClickMeText { get; set; } = "Move!";
        public Command GoToLogInCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<LoginPageModel>();
        });
        public Command SignUpCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<SignPageModel>();
        });



        public Thickness LoginMargin { get; set; } = new Thickness(50, 0, 50, 0);
        public Thickness MarginUser { get; set; } = new Thickness(20, 0, 0, 0);
        public Thickness MarginLabel { get; set; } = new Thickness(0, 0, 20, 0);
    }
}
