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
            resultStack.TranslationX = 450;

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

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(resultStack.TranslationX!=0)
            {
                resultStack.TranslateTo(0, 0, 200, Easing.SpringOut);
            }
            else
            {
                AnimateLabel();
            } 
        }

        private void AnimateLabel()
        {
            Animation parentAnimation = new Animation();

            parentAnimation.Add(0.00, 0.50, LabelTranslationUpAnimation());
            parentAnimation.Add(0.20, 0.50, LabelOpacityToZeroAnimation());
            parentAnimation.Add(0.50, 1.00, LabelTranslationDownAnimation());
            parentAnimation.Add(0.70, 1.00, LabelOpacityToOneAnimation());

            parentAnimation.Commit(this, "LabelAnimate", 16,300);
        }

        private Animation LabelTranslationDownAnimation()
        {
            var translationAnimation = new Animation(v => {

                resultNumber.TranslationY = v;

            }, 20, 0, Easing.SinIn);


            return translationAnimation;
        }

        private Animation LabelTranslationUpAnimation()
        {        

           var translationAnimation = new Animation(v => {

                resultNumber.TranslationY = v;
            
            }, 0, -20, Easing.SinIn);
            
            return translationAnimation;
        }
        private Animation LabelOpacityToZeroAnimation()
        {

            var opacityAnimation = new Animation(v => {

                resultNumber.Opacity = v;

            }, 1, 0, Easing.SinIn);


            return opacityAnimation;
        }
        private Animation LabelOpacityToOneAnimation()
        {

            var opacityAnimation = new Animation(v => {

                resultNumber.Opacity = v;

            }, 0, 1, Easing.SinIn);


            return opacityAnimation;
        }

    }
}