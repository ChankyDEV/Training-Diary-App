using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Text;
using BodyWeight.Models;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace BodyWeight.PageModels.PlansAndTrainings
{
    [AddINotifyPropertyChangedInterface]
    public class DoTraningPageModel: FreshBasePageModel
    {


        public Plan TodaysPlan { get; set; }
        public string ExText { get; set; } = "";
        public Training TodaysTraining { get; set; }
        public ObservableCollection<Excercise> Excercises { get; set; }
        public ObservableCollection<Excercise> FinishedExcercises { get; set; }
        public List<Serie> Series { get; set; }

        public DoTraningPageModel()
        {
            TodaysPlan = new Plan();
            Series = new List<Serie>();
            Excercises = new ObservableCollection<Excercise>();
            FinishedExcercises = new ObservableCollection<Excercise>();
            TodaysTraining = new Training();
            TodaysTraining.TodayExcercises = new List<Excercise>();

        }
        public override void Init(object initData)
        {
            TodaysPlan = initData as Plan;
         
            foreach(var item in TodaysPlan.Excercises)
            {
               Excercises.Add(item);           
            }
            TodaysTraining.Plan = TodaysPlan.PlanName;
        }


        public Command AcceptTrainingCommand => new Command(async() =>
        {
            TodaysTraining.Date = DateTime.Now;
        
     
            await CoreMethods.PopPageModel(TodaysTraining);
        });

        public Command ItemListClickedCommand => new Command(async(item) =>
        {
            await CoreMethods.PushPageModel<SetWeightAndRepsPageModel>(item);
        });

        public override void ReverseInit(object returnedData)
        {
            Excercise ex = returnedData as Excercise;
            TodaysTraining.TodayExcercises.Add(ex);

            FinishedExcercises.Add(ex);
            ExText = "XD";
        }

    }
}
