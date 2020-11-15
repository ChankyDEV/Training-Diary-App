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

namespace BodyWeight.PageModels
{

    public partial class StartingPageModel : FreshBasePageModel
    {

        public List<string> Activites { get; set; }
        public string CaloriesText { get; set; } = "Male";
        public string ActivityTitle { get; set; } = "Activity";
        public string PickedActivity { get; set; }

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
                    MessagingCenter.Send<ExpanderEvent>(new ExpanderEvent(),"Expander item clicked");
                });
            }
        }


       


    }
}
