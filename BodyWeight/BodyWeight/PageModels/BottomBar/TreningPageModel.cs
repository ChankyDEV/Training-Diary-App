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
             public string WelcomeText { get; set; }
             public int Day { get; set; } = DateTime.Now.Day;
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
            Day += 1;
            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Day);
            if(dateTime.DayOfWeek == DateTime.Now.DayOfWeek && Day == DateTime.Now.Day)
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
            UpdateDays();

        });
        public Command PreviousDayCommand => new Command(() =>
        {
            Day -= 1;
            DateTime dateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Day);
            if (dateTime.DayOfWeek == DateTime.Now.DayOfWeek && Day==DateTime.Now.Day)
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
            UpdateDays();

        });
        public Command AddPlanCommand => new Command(async() =>
        {
            await CoreMethods.PushPageModel<AddPlanPageModel>();
            
        });
        #endregion

        async private void GetProfileInformationAndRefreshToken()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));        
            try
            {
                var savedFirebaseAuth = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var RefreshedContent = await authProvider.RefreshAuthAsync(savedFirebaseAuth);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(RefreshedContent));

                Session.UserID = savedFirebaseAuth.User.LocalId;
             
                
                Session.LoggedUser =await DatabaseMethods.GetUserbyEmail(savedFirebaseAuth.User.Email);

                if(Session.LoggedUser.Plans!=null)
                {
                    GetPlanForSpecficDay();
                }
                else
                {
                    Session.LoggedUser.Plans = new List<Plan>();
                    Session.LoggedUser.Trainings = new List<Training>();
                }
                WelcomeText = $"Hello {Session.LoggedUser.Name}";

                HideWelcomeText();
                UpdateDays();

            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "GET PROFILE "+e.Message, "ok");
            }
        }

        private void UpdateDays()
        {
            int localPreviousDay = Day-1;
            int localNextDay = Day+1;

            DateTime PreviousdateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, localPreviousDay);
            DateTime NextdateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, localNextDay);

            PreviousDayText = PreviousdateTime.DayOfWeek.ToString();
            NextDayText = NextdateTime.DayOfWeek.ToString();
        }



        #region Methods for data comunication
        public override void ReverseInit(object returnedData)
        {
           
            Plan p = returnedData as Plan;
            Training t = returnedData as Training;       

            if(p!=null)
            {
                Session.LoggedUser.Plans.Add(p);
                DatabaseMethods.AddPlanToDatabase();            
            }
            if(t!=null)
            {
                Session.LoggedUser.Trainings.Add(t);
                DatabaseMethods.AddPlanToDatabase();
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
                    await CoreMethods.PushPageModel<DoTraningPageModel>(item);
                    
                });
            }
        }


        private void HideWelcomeText()
        {
            Device.StartTimer(new TimeSpan(0, 0, 6), (() => {

                Device.BeginInvokeOnMainThread(() =>
                {
                    TextVisible = false;
                    WelcomeText = "Build your strength with us!";
                    TextVisible = true;
                  
                });
                return true;
            }) 
            );
            {
                
            }
        }

        


    }
}
