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
        private double startExpanderHeight;
        public CalculatorPage()
        {
            InitializeComponent();
            startExpanderHeight = ActivityExpander.Height;


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<ExpanderEvent>(this, "Expander item clicked",CollapseExpander);
            ActivityExpander.SizeChanged += ActivityExpander_SizeChanged;

        }

        private void CollapseExpander(ExpanderEvent obj)
        {
            ActivityExpander.IsExpanded = false;
        }

        private void ActivityExpander_SizeChanged(object sender, EventArgs e)
        {

            var maxHeight = 186.56;
            var startHeight = 34.4;

            
            var percent = (ActivityExpander.Height-startHeight) / (maxHeight-startHeight);

            if(percent<0)
            {
                percent = 0;
            }

            ImageUnderExpander.Opacity = 1 -percent * 1.5;
            ImageUnderExpander.TranslationY = 0 + percent * 70;

            if (ActivityExpander.State == ExpanderState.Expanding)
            {
                
            }
            if(ActivityExpander.State == ExpanderState.Collapsing)
            {

            }
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