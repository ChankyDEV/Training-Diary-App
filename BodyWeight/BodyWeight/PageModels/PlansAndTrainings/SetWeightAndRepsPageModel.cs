using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
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
        public string Weight { get; set; } = "0";
        public double WeightDouble { get; set; } = 0;
        public string Reps { get; set; } = "0";
        public int RepsInt { get; set; } = 0;
        public string ExName { get; set; } = "";

        public ICommand RemoveCommand { get; set; }
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
            RemoveCommand = new Command(RemoveAction);
        }

        private void RemoveAction(object obj)
        {
            Serie serie = obj as Serie;

            if (serie != null)
            {
                Series.Remove(serie);
            }
        }

        public Command AddWeightCommand => new Command(() =>
         {

             WeightDouble = ConvertStringToDouble(Weight) + 2.5;
             Weight = WeightDouble.ToString();
         });

        private double ConvertStringToDouble(string input)
        {
            return double.Parse(input);
        }
        private int ConvertStringToInt(string input)
        {
            return int.Parse(input);
        }

        public Command SubtractWeightCommand => new Command(() =>
        {
            WeightDouble = ConvertStringToDouble(Weight) - 2.5;
            Weight = WeightDouble.ToString();
        });

        public Command AddRepsCommand => new Command(() =>
        {
            RepsInt = ConvertStringToInt(Reps) + 1;
            Reps = RepsInt.ToString();
        });

        public Command SubtractRepsCommand => new Command(() =>
        {
            RepsInt = ConvertStringToInt(Reps) - 1;
            Reps = RepsInt.ToString();
        });

        public Command SaveExcerciseCommand => new Command(async() =>
        {
            if(Series.Count!=0)
            {
                Excercise.Series = Series.ToList();
                Excercise.PrepareHeight();
                await CoreMethods.PopPageModel(Excercise);
            }
            else
            {

            }
            
            
        });


        public Command AddSerie => new Command(() =>
        {
            if (String.IsNullOrWhiteSpace(Weight))
            {
                Weight = "0";
            }
            if (String.IsNullOrWhiteSpace(Reps))
            {
                Reps = "0";
            }
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
