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
using BodyWeight.Helpers;
using System.Threading;

namespace BodyWeight.PageModels
{

    public partial class StartingPageModel : FreshBasePageModel
    {

        public List<string> Activites { get; set; }
        public string Gender { get; set; } = "Male";
        public string Result { get; set; } = "";
        public string ResultSubText { get; set; } = "calories";
        public string Weight { get; set; } = "";
        public string Height { get; set; } = "";
        public string Age { get; set; } = "";
        public string ActivityTitle { get; set; } = "Choose your activity";
        public string PickedActivity { get; set; }
        public string ArrowExpanderSource { get; set; } = "down_arrow";
        public Dictionary<string, double> ActivityDictionary { get; set;} 




        public Command GenderChangeCommand => new Command(() =>
        {
            if (Gender == "Male")
                Gender = "Female";
            else
                Gender = "Male";
        });
        public ICommand ExpanderItemClickedCommand
        {
            get
            {
                return new Command( (item) =>
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
        public Command CalculateCaloriesCommand => new Command(() =>
        {
        if (ActivityTitle == "Choose your activity")
        {
                ResultSubText = "Choose your activity";
        }
        else
        {
            if(formIsValid())
            {
                    ResultSubText = "calories";
                    Device.StartTimer(TimeSpan.FromMilliseconds(150), () => {

                        double ppm = CalculatePPM(Weight, Height, Age, Gender);
                        double factor = ActivityFactor();
                        double cpm = Math.Round(ppm * factor);
                        Result = $"{cpm}";

                        return false;
                    });
            }
            else
            {
                Result = "";
                ResultSubText = "Fill all the entries";
            }
            
            
         }
          

        });

        private bool formIsValid()
        {
            double weight = 0;
            double height = 0;
            double age = 0;
            if (Double.TryParse(Weight,out weight) && Double.TryParse(Height, out height) && Double.TryParse(Age, out age))
            {
                return true;
            }
            else
            {
                
                return false;
            }
        }

        private double ActivityFactor()
        {
            
            return ActivityDictionary[ActivityTitle];
        }

        private double CalculatePPM(string w, string h, string a,string gender)
        {
            var weight = double.Parse(w);
            var height = double.Parse(h);
            var age = double.Parse(a);
            int constVal = Gender == "Male" ? 5 : -161;

            var result = (10 * weight) + (6.25 * height) - (5 * age) + constVal;

            return result;
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
        private void CreateActivitesCollection()
        {
            Activites = new List<string>();
            Activites.Add("No activity");
            Activites.Add("Low activity");
            Activites.Add("Medium activity");
            Activites.Add("Active lifestyle");
            Activites.Add("Really active lifestyle");
            Activites.Add("Extreamly active lifestyle");

            ActivityDictionary = new Dictionary<string, double>();
            ActivityDictionary.Add(Activites[0], 1.0);
            ActivityDictionary.Add(Activites[1], 1.2);
            ActivityDictionary.Add(Activites[2], 1.375);
            ActivityDictionary.Add(Activites[3], 1.55);
            ActivityDictionary.Add(Activites[4], 1.725);
            ActivityDictionary.Add(Activites[5], 2.0);

        }


    }
}
