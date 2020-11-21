using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using BodyWeight.Events;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup;
using BodyWeight.Pages.PopUps;
using Rg.Plugins.Popup.Services;

namespace BodyWeight.PageModels
{

    public partial class StartingPageModel : FreshBasePageModel
    {

        public List<string> Activites { get; set; }
        public string CaloriesText { get; set; } = "Male";
        public string ActivityTitle { get; set; } = "Choose your activity";
        public string PickedActivity { get; set; }
        public string ArrowExpanderSource { get; set; } = "down_arrow";

        public Command SexChangeCommand => new Command(() =>
        {
            if (CaloriesText == "Male")
                CaloriesText = "Female";
            else
                CaloriesText = "Male";
        });
        public ICommand ExpanderItemClickedCommand
        {
            get
            {
                return new Command(async (item) =>
                {
                    ActivityTitle = item.ToString();
                    MessagingCenter.Send<ExpanderEvent>(new ExpanderEvent(), "Expander item clicked");
                    ChangeExpanderIcon(new ExpanderEvent());
                });
            }
        }
        public Command InfoPopUpCommand => new Command(async () =>
        {
            await PopupNavigation.Instance.PushAsync(new ActivityInfoPopUp());
        });



        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            MessagingCenter.Subscribe<ExpanderEvent>(this, "Expander size changed", ChangeExpanderIcon);
        }

        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            MessagingCenter.Unsubscribe<ExpanderEvent>(this, "Expander size changed");
        }
        private void ChangeExpanderIcon(ExpanderEvent obj)
        {
            if(ArrowExpanderSource == "down_arrow")
            {
                ArrowExpanderSource = "up_arrow";
            }
            else
            {
                ArrowExpanderSource = "down_arrow";
            }
            
        }

        
    }
}
