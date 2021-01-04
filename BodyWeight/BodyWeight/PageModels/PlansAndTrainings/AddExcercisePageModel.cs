using System;
using System.Collections.Generic;
using System.Text;
using BodyWeight.Models;
using BodyWeight.Pages.PlansAndTrainings;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace BodyWeight.PageModels.PlansAndTrainings
{
    [AddINotifyPropertyChangedInterface]
    public class AddExcercisePageModel : FreshBasePageModel
    {
        public Excercise ex { get; set; }
        public ExcerciseChanged ReturnedEx { get; set; }
        public string ExcerciseName { get; set; } = "";
        public string NumberOfSeries { get; set; } = "";
        public string Repetitions { get; set; } = "";
        public int ID { get; set; } = 0;

        public Command AddExcerciseCommand => new Command(async () =>
        {
            if(String.IsNullOrWhiteSpace(ExcerciseName) || String.IsNullOrWhiteSpace(NumberOfSeries) || String.IsNullOrWhiteSpace(Repetitions))
            {

            }
            else
            {
                if (isFormValid())
                {
                    if (ReturnedEx == null)
                    {
                        ex = new Excercise();
                        ex.ExcerciseName = ExcerciseName;
                        ex.NumberOfSeries = int.Parse(NumberOfSeries);
                        ex.Repetitions = int.Parse(Repetitions);
                        await CoreMethods.PopPageModel(ex);
                    }
                    else
                    {
                        ReturnedEx.ChangedExcercise.ExcerciseName = ExcerciseName;
                        ReturnedEx.ChangedExcercise.NumberOfSeries = int.Parse(NumberOfSeries);
                        ReturnedEx.ChangedExcercise.Repetitions = int.Parse(Repetitions);
                        ReturnedEx.isChanged = true;
                        await CoreMethods.PopPageModel(ReturnedEx);
                    }
                }
                
            }
           
            
     
        });

        private bool isFormValid()
        {
            double numberOfSeries = 0;
            double repetitions = 0;

            bool canConvert = double.TryParse(NumberOfSeries,out numberOfSeries) && double.TryParse(Repetitions, out repetitions);

            if (canConvert)
            {
                return true;
            }
            return false;
        }

        public Command GoBackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel();

        });
        public override void Init(object returnedData)
        {
            if(returnedData==null)
            {

            }
            else
            {
                 ReturnedEx = returnedData as ExcerciseChanged;
                 ExcerciseName = ReturnedEx.ChangedExcercise.ExcerciseName;
                 NumberOfSeries = ReturnedEx.ChangedExcercise.NumberOfSeries.ToString();
                 Repetitions = ReturnedEx.ChangedExcercise.Repetitions.ToString();
            }
            }
            
    }
}
