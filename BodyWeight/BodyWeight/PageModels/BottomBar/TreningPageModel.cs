using BodyWeight.Models;
using BodyWeight.PageModels.PlansAndTrainings;
using BodyWeight.Pages;
using Firebase.Auth;
using FreshMvvm;
using Newtonsoft.Json;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BodyWeight.PageModels
{
    [AddINotifyPropertyChangedInterface]
    public partial class StartingPageModel : FreshBasePageModel
    {
        #region Properties

             public bool TextVisible { get; set; } = true;
             public int Day { get; set; } = DateTime.Now.Day;
             public int Month { get; set; } = DateTime.Now.Month;
             public int Year { get; set; } = DateTime.Now.Year;
            public int DateIterator { get; set; } = 0;
            public DayOfWeek DayName { get; set; } = DateTime.Now.DayOfWeek;
             public string ActualDateText { get; set; } = DateTime.Now.ToShortDateString();
             public string ActualDayText { get; set; } = "Today";
             public string PreviousDayText { get; set; } = "";
             public string NextDayText { get; set; } = "";
             public Account User { get; set; }
             public string PlanText { get; set; } = "";
             public ObservableCollection<Plan> Plans { get; set; }
             public ObservableCollection<Excercise> Exs { get; set; }

             public Thickness HeaderPadding { get; set; } = new Thickness(20,5,20,5);

        #endregion

        #region Commands

        public Command NextDayCommand => new Command(() =>
        {
            //Day += 1;
            DateIterator++;
            UpdateDate();

        });
        public Command PreviousDayCommand => new Command(() =>
        {
            //Day -= 1;
            DateIterator--;
            UpdateDate();

        });
        public Command AddPlanCommand => new Command(async() =>
        {
            await CoreMethods.PushPageModel<AddPlanPageModel>();
            
        });
        #endregion

    

        private void UpdateDate()
        {

            DateTime dateTime = DateTime.Now.AddDays(DateIterator);

            if (dateTime.ToShortDateString()==DateTime.Now.ToShortDateString())
            {
                ActualDateText = dateTime.ToShortDateString();
                ActualDayText = "Today";
            }
            else
            {
                ActualDateText = dateTime.ToShortDateString();
                ActualDayText = dateTime.DayOfWeek.ToString();
            }

            if (Session.LoggedUser.Plans != null)
            {
                GetPlanForSpecficDay();
            }

        }

        #region Methods for data comunication
        public override void ReverseInit(object returnedData)
        {
           
            Plan p = returnedData as Plan;
            Training t = returnedData as Training;       

            if(p!=null)
            {
                Session.LoggedUser.Plans.Add(p);
                DatabaseMethods.AddPlanToDatabase(p);            
            }
            if(t!=null)
            {
                if (Session.LoggedUser.Trainings == null)
                {
                    Session.LoggedUser.Trainings = new List<Training>();
                }
                Session.LoggedUser.Trainings.Add(t);
                DatabaseMethods.AddTreningToDatabase(t);
            }

            GetPlanForSpecficDay();

        }
        

        #endregion

        private void GetPlanForSpecficDay( )
        {
            PlanText = "";
            string topBarDayText = ActualDayText.ToLower();
            if(topBarDayText=="today")
            {
                topBarDayText = DayName.ToString().ToLower();
            }
            Plans.Clear();
            string userDayText ="";

            foreach (var item in Session.LoggedUser.Plans)
            {
                string[] daysTab = item.DayOfRepeat.Split(' ');


                foreach(string day in daysTab)
                {
                    userDayText = day.ToLower();
                    if(userDayText == topBarDayText)
                    {
                        ShowPlanInLabel(item);
                        Exs = ConvertListToObservable(item.Excercises);
                    }
                }
               
            }

        }
        private ObservableCollection<Excercise> ConvertListToObservable(List<Excercise> excercises)
        {
            ObservableCollection<Excercise> list = new ObservableCollection<Excercise>();

            foreach(var item in excercises)
            {
                list.Add(item);
            }

            return list;
        }
        private void ShowPlanInLabel(Plan plan)
        {
            if(Plans.Contains(plan))
            {

            }
           else
            {
                Plans.Add(plan);   
            }
                  

        }
        public ICommand ItemClickedCommand
        {
            get
            {
                return new Command(async (item) =>
                {
                    DateTime dateTime = DateTime.Now.AddDays(DateIterator);
                    await CoreMethods.PushPageModel<DoTraningPageModel>(new
                    {
                        plan = item,
                        date = dateTime
                    });
                    
                });
            }
        }

    }
}
