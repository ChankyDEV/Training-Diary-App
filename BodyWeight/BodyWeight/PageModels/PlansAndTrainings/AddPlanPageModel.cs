using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BodyWeight.Models;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;




namespace BodyWeight.PageModels.PlansAndTrainings
{
    [AddINotifyPropertyChangedInterface]
    public class AddPlanPageModel : FreshBasePageModel
    {
        public string PlanName { get; set; }
        public string DayOfRepeat { get; set; }
        public Plan ActualCreatedPlan { get; set; }
        public Excercise ClickedItem { get; set; }
        public bool Monday { get; set; } = false;
        public bool Tuesday { get; set; } = false;
        public bool Wednesday { get; set; } = false;
        public bool Thursday { get; set; } = false;
        public bool Friday { get; set; } = false;
        public bool Saturday { get; set; } = false;
        public bool Sunday { get; set; } = false;

        public string XD { get; set; } = "";
        public ObservableCollection<Excercise> Excercises { get; set; }

 


        public AddPlanPageModel()
        {
            
            ActualCreatedPlan = new Plan();
            Excercises = new ObservableCollection<Excercise>();
        }
        public Command AddExcerciseCommand => new Command(async () =>
        {
            await CoreMethods.PushPageModel<AddExcercisePageModel>();

        });
        public Command RemoveCommand => new Command( () =>
        {
            Excercises.Remove(Excercises.Last());

        });
        public Command AddPlanCommand => new Command(async () =>
        {


            if(Monday==true)
            {
                DayOfRepeat += "Monday ";
            }
            if (Tuesday == true)
            {
                DayOfRepeat += " Tuesday ";
            }
            if (Wednesday == true)
            {
                DayOfRepeat += " Wednesday ";
            }
            if (Thursday == true)
            {
                DayOfRepeat += " Thursday ";
            }
            if (Friday == true)
            {
                DayOfRepeat += " Friday ";
            }
            if (Saturday == true)
            {
                DayOfRepeat += " Saturday ";
            }
            if (Sunday == true)
            {
                DayOfRepeat += " Sunday ";
            }
            int a = 0;

        if (int.TryParse(PlanName, out a) == true || PlanName == null || PlanName == "" || Excercises.Count == 0)
            {
                string error = "";
                if (Monday == false && Tuesday == false && Wednesday==false && Thursday==false && Friday==false && Saturday==false && Sunday==false && PlanName!="")
                {
                    error = "Choose day\n";
                }    
                if(Excercises.Count == 0)
                {
                    error += "Add some excercises\n";
                }
                if(String.IsNullOrWhiteSpace(PlanName))
                {
                    error += "Fill plan name";                    
                }
                await Application.Current.MainPage.DisplayAlert("Warning", error, "Agree");
            }
            else
            {
                 ActualCreatedPlan.Excercises = Excercises.ToList();
                 ActualCreatedPlan.DayOfRepeat = DayOfRepeat;
                 ActualCreatedPlan.PlanName = PlanName;
                 await CoreMethods.PopPageModel(ActualCreatedPlan);
            }

            

        });

        #region Checkboxes


        public Command TextClickMonday => new Command(() =>
        {
            if(Monday==true)
            {
                Monday = false;
            }
            else
            {
                Monday = true;
            }         
        });
     
        public Command TextClickTuesday => new Command(() =>
        {
            if (Tuesday == true)
            {
                Tuesday = false;
            }
            else
            {
                Tuesday = true;
            }
        });
        public Command TextClickWednesday => new Command(() =>
        {
            if (Wednesday == true)
            {
                Wednesday = false;
            }
            else
            {
                Wednesday = true;
            }
        });
        public Command TextClickThursday => new Command(() =>
        {
            if (Thursday == true)
            {
                Thursday = false;
            }
            else
            {
                Thursday = true;
            }
        });
        public Command TextClickFriday => new Command(() =>
        {
            if (Friday == true)
            {
                Friday = false;
            }
            else
            {
                Friday = true;
            }
        });
        public Command TextClickSaturday => new Command(() =>
        {
            if (Saturday == true)
            {
                Saturday = false;
            }
            else
            {
                Saturday = true;
            }
        });
        public Command TextClickSunday => new Command(() =>
        {
            if (Sunday == true)
            {
                Sunday = false;
            }
            else
            {
                Sunday = true;
            }
        });

        #endregion
        public override void ReverseInit(object returnedData)
        {
            Excercise e = returnedData as Excercise;
            ExcerciseChanged ech = returnedData as ExcerciseChanged;

            if(e!=null)
            {
                AddExcerciseToPlan(e); 
            }
            if(ech!=null)
            {
                UpdateExcercise(ech);
            }
               
            
                   
        }

        private void UpdateExcercise(ExcerciseChanged ech)
        {
            int index=Excercises.IndexOf(ClickedItem);
            Excercises[index] = ech.ChangedExcercise;
        }

        public ICommand ItemClickedCommand
        {
            get
            {
                return new Command(async(item) =>
                {
                    await CoreMethods.PushPageModel<AddExcercisePageModel>(new ExcerciseChanged() { 
                    ChangedExcercise=item as Excercise,
                    isChanged=true
                    });
                    ClickedItem = item as Excercise;
                  
                });
            }
        }

        private void AddExcerciseToPlan(Excercise e)
        {
            try
            {
             
                Excercises.Add(e);
            
            }
            catch(Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Alert", ex.Message, "ok");
            }

            
         
        }

        public Command GoBackCommand => new Command(async() =>
        {
            await CoreMethods.PopPageModel();

        });
    }
}
