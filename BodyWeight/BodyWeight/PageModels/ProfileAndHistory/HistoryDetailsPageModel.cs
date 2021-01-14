using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BodyWeight.Models;
using FreshMvvm;
using Xamarin.Forms;

namespace BodyWeight.PageModels.ProfileAndHistory
{
    public class HistoryDetailsPageModel : FreshBasePageModel
    {
        public Training TrainingDetails { get; set; }
        public string TrainingDate { get; set; } = "";

        public int AllRowsHeight { get; set; } = 150;
        public ObservableCollection<Excercise> Excercises { get; set; }



        public override void Init(object initialData)
        {
            if(initialData is Training && initialData != null)
            {
                TrainingDetails = initialData as Training;
                TrainingDate = TrainingDetails.Date.ToShortDateString();
                Excercises = GetExcerciseList(TrainingDetails.TodayExcercises);
                CapitalizeExcerciseNames();
                AllRowsHeight = GetHeightFromRows();
            }            
        }

        private void CapitalizeExcerciseNames()
        {
            foreach (var item in Excercises)
            {
                item.ExcerciseName = item.ExcerciseName.ToUpper();
            }
        }

        private int GetHeightFromRows()
        {
            
            int sum = 0;
            foreach(var item in Excercises)
            {
                sum += item.Height;

            }
         
            return sum;
        }

        private ObservableCollection<Excercise> GetExcerciseList(List<Excercise> todayExcercises)
        {
            var excercises = new ObservableCollection<Excercise>();

            foreach(var excercise in todayExcercises)
            {
                excercises.Add(excercise);
            }

            return excercises;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            var a = Excercises;
        }

        public Command GoBackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel();

        });
    }
}
