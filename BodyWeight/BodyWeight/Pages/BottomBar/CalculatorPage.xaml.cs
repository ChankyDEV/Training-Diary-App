using BodyWeight.Events;
using BodyWeight.PageModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BodyWeight.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculatorPage : ContentPage
    {

        public CalculatorPage()
        {
            InitializeComponent();

            ActivityExpander.Tapped += ActivityExpander_Tapped;
        }

        private void ActivityExpander_Tapped(object sender, EventArgs e)
        {
            MessagingCenter.Send(new ExpanderEvent(),"Expander size changed");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<ExpanderEvent>(this, "Expander item clicked",CollapseExpander);
           

        }

        private void CollapseExpander(ExpanderEvent obj)
        {
            ActivityExpander.IsExpanded = false;
        }



        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<ExpanderEvent>(this, "Expander item clicked");
        }

        private void SegmentedFrame_Tapped(object sender, EventArgs e)
        {
            double backgroundWidth = SegmentedFrameBackground.Width;
            double translationWidth = backgroundWidth / 2;


            if(SegmentedFrame.TranslationX == translationWidth)
            {
                SegmentedFrame.TranslateTo(0, SegmentedFrame.TranslationY, 200);
                FemaleText.TextColor = Color.DarkGray;
                MaleText.TextColor = Color.FromHex("#FFC21C");
                
            }
            else
            {
                SegmentedFrame.TranslateTo(translationWidth, SegmentedFrame.TranslationY,200);
                MaleText.TextColor = Color.DarkGray;
                FemaleText.TextColor = Color.FromHex("#FFC21C");
            }
        }

        
    }
}