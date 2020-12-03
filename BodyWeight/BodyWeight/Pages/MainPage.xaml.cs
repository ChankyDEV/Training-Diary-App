using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BodyWeight.Pages
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }
        DisplayInfo mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            double width = mainDisplayInfo.Width;
            EmailEntry.TranslationX = -(width);
            PasswordEntry.TranslationX = -(width);
            LoginButton.TranslationX = -(width);
            HaveAccText.TranslationX = -(width);
        }

        

        private void GoToLoginButton_Clicked(object sender, EventArgs e)
        {
            double width = mainDisplayInfo.Width;

            Animation parentAnimation = new Animation();

            
            
            parentAnimation.Add(0.00, 0.20, LoginButtonAnimation());
            parentAnimation.Add(0.10, 0.30, RegisterButtonAnimation());
            
            parentAnimation.Add(0.20, 0.40, EmailAnimation());
            parentAnimation.Add(0.30, 0.50, PasswordAnimation());
            parentAnimation.Add(0.40, 0.60, LoginAnimation());
            parentAnimation.Add(0.50, 0.70, LabelAnimation());

            parentAnimation.Commit(this, "LoginAnimation", 16, 2000);
        }

        private Animation LabelAnimation()
        {
            double width = mainDisplayInfo.Width;
            var translationAnimation = new Animation(v => {

                HaveAccText.TranslationX = v;

            }, -width, 0, Easing.SinOut);


            return translationAnimation;
        }

        private Animation LoginAnimation()
        {
            double width = mainDisplayInfo.Width;
            var translationAnimation = new Animation(v => {


                LoginButton.TranslationX = v;
              

            }, -width, 0, Easing.SinOut);


            return translationAnimation;
        }

        private Animation PasswordAnimation()
        {
            double width = mainDisplayInfo.Width;
            var translationAnimation = new Animation(v => {

     
                PasswordEntry.TranslationX = v;


            }, -width, 0, Easing.SinOut);


            return translationAnimation;
        }

        private Animation EmailAnimation()
        {
            double width = mainDisplayInfo.Width;
            var translationAnimation = new Animation(v => {

                EmailEntry.TranslationX = v;


            }, -width, 0, Easing.SinOut);


            return translationAnimation;
        }

        private Animation RegisterButtonAnimation()
        {
            double width = mainDisplayInfo.Width;
            var translationAnimation = new Animation(v => {

                GoToRegisterButton.TranslationX = v;            
                RegisterButtonFrame.TranslationX = v;

            }, 0, width, Easing.SinIn);


            return translationAnimation;
        }
        private Animation LoginButtonAnimation()
        {
            double width = mainDisplayInfo.Width;
            var translationAnimation = new Animation(v => {

                GoToLoginButton.TranslationX = v;

            }, 0, width, Easing.SinIn);


            return translationAnimation;
        }
    }
}
