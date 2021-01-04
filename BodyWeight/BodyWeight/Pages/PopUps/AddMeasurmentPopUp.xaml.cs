using BodyWeight.Events;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BodyWeight.Pages.PopUps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddMeasurmentPopUp : Rg.Plugins.Popup.Pages.PopupPage
    {
        public AddMeasurmentPopUp()
        {
            InitializeComponent();
           
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if (isFormValid())
            {
                double weightdob = 0;
                if (Double.TryParse(weightEntry.Text, out weightdob))
                {
                    PopupNavigation.Instance.PopAsync();
                    WeightEvent weight = new WeightEvent(datePicker.Date, double.Parse(weightEntry.Text));
                    weight.Weight = Math.Round(weight.Weight,1);
                    MessagingCenter.Send<WeightEvent>(weight, "Added weight");
                }
                else
                {

                }
            }
            else
            {

            }
            
             
        }

        private bool isFormValid()
        {
            if (!String.IsNullOrWhiteSpace(weightEntry.Text))
            {
                var weight = 0.0;
                try
                {
                    weight = double.Parse(weightEntry.Text);
                    if (weight < 0 || weight > 500)
                    {
                        return false;
                    }
                    return true;
                }
                catch(Exception e)
                {

                }
               
               
            }
            return false;
            
        }
    }
}