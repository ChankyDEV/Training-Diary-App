using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using BodyWeight.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace BodyWeight.PageModels.PlansAndTrainings
{
    [AddINotifyPropertyChangedInterface]
    public class SetWeightAndRepsPageModel: FreshBasePageModel
    {
        public Excercise Excercise { get; set; } = new Excercise();
        public string Weight { get; set; } = "";
        public double WeightInt { get; set; } = 0;
        public string Reps { get; set; } = "";
        public int RepsInt { get; set; } = 0;
        public string ExName { get; set; } = "";
        
        public ObservableCollection<Serie> Series { get; set; }

        public override void Init(object initData)
        {
            Excercise = initData as Excercise;
            Excercise.Series = new List<Serie>();
             ExName = Excercise.ExcerciseName;
        }
        public SetWeightAndRepsPageModel()
        {
            Series = new ObservableCollection<Serie>();
        }


        public Command AddWeightCommand => new Command(() =>
         {
             WeightInt += 2.5;
             Weight = WeightInt.ToString();
         });

        public Command SubtractWeightCommand => new Command(() =>
        {
            WeightInt -= 2.5;
            Weight = WeightInt.ToString();
        });

        public Command AddRepsCommand => new Command(() =>
        {
            RepsInt += 1;
            Reps = RepsInt.ToString();
        });

        public Command SubtractRepsCommand => new Command(() =>
        {
            RepsInt -= 1;
            Reps = RepsInt.ToString();
        });

        public Command SaveExcerciseCommand => new Command(async() =>
        {
            Excercise.Series = Series.ToList();
            await CoreMethods.PopPageModel(Excercise);
        });


        public Command AddSerie => new Command(() =>
        {
            Excercise.Series.Add(new Serie() { Weight = double.Parse(Weight), Reps = int.Parse(Reps) });
            Series.Add(new Serie() { Weight = double.Parse(Weight), Reps = int.Parse(Reps) });
        });
        public Command RemoveSerie => new Command(() =>
        {
            if(Excercise.Series.Count>0)
            {
                Excercise.Series.Remove(Excercise.Series.Last());
                Series.Remove(Series.Last());
            }
            
        });
        public Command GoBackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel();

        });


    }
}
