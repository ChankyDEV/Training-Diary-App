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
            DontHaveAccText.TranslationX = -(width);

         
            NameEntry.TranslationX = -(width);
            HaveAccText.TranslationY = (width / 2);


            RegisterButton.TranslationY = (width/2);
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
            double starts = 0;
            double ends = 0;
            Easing easing;
            if(DontHaveAccText.TranslationX==0)
            {
                starts = 0;
                ends = width;
                easing = Easing.SinIn;
            }
            else
            {
                starts = -width;
                ends = 0;
                easing = Easing.SinOut;
            }

            var translationAnimation = new Animation(v => {

                DontHaveAccText.TranslationX = v;

            }, starts, ends, easing);


            return translationAnimation;
        }

        private Animation LoginAnimation()
        {
            double width = mainDisplayInfo.Width;

            double starts = 0;
            double ends = 0;
            Easing easing;
            if (LoginButton.TranslationX == 0)
            {
                starts = 0;
                ends = width;
                easing = Easing.SinIn;
            }
            else
            {
                starts = -width;
                ends = 0;
                easing = Easing.SinOut;
            }

            var translationAnimation = new Animation(v => {


                LoginButton.TranslationX = v;
              

            }, starts, ends, easing);


            return translationAnimation;
        }

        private Animation PasswordAnimation()
        {
            double width = mainDisplayInfo.Width;

            double starts = 0;
            double ends = 0;
            Easing easing;
            if (PasswordEntry.TranslationX == 0)
            {
                starts = 0;
                ends = width;
                easing = Easing.SinIn;
            }
            else
            {
                starts = -width;
                ends = 0;
                easing = Easing.SinOut;
            }

            var translationAnimation = new Animation(v => {

     
                PasswordEntry.TranslationX = v;


            }, starts, ends, easing);


            return translationAnimation;
        }

        private Animation EmailAnimation()
        {
            double width = mainDisplayInfo.Width;
            double starts = 0;
            double ends = 0;
            Easing easing;
            if (EmailEntry.TranslationX == 0)
            {
                starts = 0;
                ends = width;
                easing = Easing.SinIn;
            }
            else
            {
                starts = -width;
                ends = 0;
                easing = Easing.SinOut;
            }

            var translationAnimation = new Animation(v => {

                EmailEntry.TranslationX = v;


            }, starts, ends, easing);


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

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Animation parentAnimation = new Animation();

            parentAnimation.Add(0.00, 0.20, LabelAnimation());
            parentAnimation.Add(0.10, 0.30, LoginAnimation());


            parentAnimation.Add(0.20, 0.40, NameAnimation());
            parentAnimation.Add(0.30, 0.50, RegisterAnimation());
            parentAnimation.Add(0.40, 0.60, AlreadyHaveAccAnimation());


            parentAnimation.Commit(this, "LoginAnimation", 16, 2000);


        }

        private Animation AlreadyHaveAccAnimation()
        {
            double width = mainDisplayInfo.Width;
            double starts = 0;
            double ends = 0;
            Easing easing;
            var translationAnimation = new Animation();
            if (HaveAccText.TranslationY == 0)
            {
                easing = Easing.SinIn;
                if (HaveAccText.TranslationX == 0)
                {
                    starts = 0;
                    ends = width;
                    translationAnimation = new Animation(v => {

                        HaveAccText.TranslationX = v;


                    }, starts, ends, easing);

                }
            }
            else if (HaveAccText.TranslationY != 0)
            {
                if (HaveAccText.TranslationX != 0)
                {
                    HaveAccText.TranslationX = 0;
                }
                starts = (width / 2);
                ends = 0;
                easing = Easing.SinOut;

                translationAnimation = new Animation(v => {

                    HaveAccText.TranslationY = v;


                }, starts, ends, easing);
            }




            return translationAnimation;
        }

        private Animation RegisterAnimation()
        {
            double width = mainDisplayInfo.Width;
            double starts = 0;
            double ends = 0;
            Easing easing;
            var translationAnimation = new Animation();
            if (RegisterButton.TranslationY == 0 )
            {                
                easing = Easing.SinIn;
                if (RegisterButton.TranslationX == 0)
                {
                    starts = 0;
                    ends = width;
                    translationAnimation = new Animation(v => {

                        RegisterButton.TranslationX = v;


                    }, starts, ends, easing);

                } 
            }
            else if(RegisterButton.TranslationY != 0)
            {
                if(RegisterButton.TranslationX != 0)
                {
                    RegisterButton.TranslationX = 0;
                }
                starts = (width/2);
                ends = 0;
                easing = Easing.SinOut;

                translationAnimation = new Animation(v => {

                    RegisterButton.TranslationY = v;


                }, starts, ends, easing);
            }

          


            return translationAnimation;
        }

        private Animation NameAnimation()
        {
            double width = mainDisplayInfo.Width;
            double starts = 0;
            double ends = 0;
            Easing easing;
            if (NameEntry.TranslationX == 0)
            {
                starts = 0;
                ends = width;
                easing = Easing.SinIn;
            }
            else
            {
                starts = -width;
                ends = 0;
                easing = Easing.SinOut;
            }

            var translationAnimation = new Animation(v => {

                NameEntry.TranslationX = v;


            }, starts, ends, easing);


            return translationAnimation;
        }

        private void HaveAccText_Tapped(object sender, EventArgs e)
        {

            Animation parentAnimation = new Animation();

            parentAnimation.Add(0.00, 0.20, AlreadyHaveAccAnimation());
            parentAnimation.Add(0.10, 0.30, AlreadyHaveAccGoesUpAnimation());
            parentAnimation.Add(0.10, 0.30, RegisterAnimation());
            parentAnimation.Add(0.20, 0.40, RegisterGoesUpAnimation());
            parentAnimation.Add(0.20, 0.40, NameAnimation());

            parentAnimation.Add(0.30, 0.50, LoginAnimation());
            parentAnimation.Add(0.40, 0.60, LabelAnimation());
        

            parentAnimation.Commit(this, "LoginAnimation", 16, 2000);
        }

        private Animation AlreadyHaveAccGoesUpAnimation()
        {
            double width = mainDisplayInfo.Width;

            double starts = 0;
            double ends = (width / 2);
            Easing easing = Easing.SinIn;

            var translationAnimation = new Animation(v => {


                HaveAccText.TranslationY = v;


            }, starts, ends, easing);


            return translationAnimation;
        }

        private Animation RegisterGoesUpAnimation()
        {
            double width = mainDisplayInfo.Width;

            double starts = 0;
            double ends = (width / 2);
            Easing easing = Easing.SinIn;
          
            var translationAnimation = new Animation(v => {


                RegisterButton.TranslationY = v;


            }, starts, ends, easing);


            return translationAnimation;
        }

        private void GoToRegisterButton_Clicked(object sender, EventArgs e)
        {
            Animation parentAnimation = new Animation();

            
            parentAnimation.Add(0.00, 0.20, RegisterButtonAnimation());
            parentAnimation.Add(0.10, 0.30, LoginButtonAnimation());

            parentAnimation.Add(0.20, 0.40, EmailAnimation());
            parentAnimation.Add(0.30, 0.50, PasswordAnimation());
            parentAnimation.Add(0.40, 0.60, NameAnimation());
            parentAnimation.Add(0.50, 0.70, RegisterAnimation());
            parentAnimation.Add(0.60, 0.80, AlreadyHaveAccAnimation());

            parentAnimation.Commit(this, "LoginAnimation", 16, 2000);
        }

       
    }
}
