using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BodyWeight.Models;
using FreshMvvm;
using Xamarin.Forms;

namespace BodyWeight.PageModels.ProfileAndHistory
{
    public class HistoryPageModel : FreshBasePageModel
    { 
        public ObservableCollection<Training> TrainingHistory { get; set; }
        public bool IsLoading { get; set; } = true;
        public bool IsHistoryVisable { get; set; } = false;
        public string Placeholder { get; set; } = "";
        public HistoryPageModel()
        {
                 
        }
       

        public Command GoBackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel();

        });


        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            GetTrainingsHistory();
        }

        private async void GetTrainingsHistory()
        {
            try
            {
                var temporaryList = await DatabaseMethods.GetTrainings();
                if(temporaryList.Count>0)
                {
                    Placeholder = "";
                    ToObservable(temporaryList);
                }
                else
                {
                    TrainingHistory = new ObservableCollection<Training>();
                    Placeholder= "Ups... Its empty\nDo some trainings to see your progress here";
                }
                IsLoading = false;
                IsHistoryVisable = true;
                
            }
            catch(Exception ex)
            {
                Placeholder = "Ups... Its empty\nDo some trainings to see your progress here";
                TrainingHistory = new ObservableCollection<Training>();
                IsLoading = false;
                IsHistoryVisable = true;
            }
            
            
        }

        private void ToObservable(List<Training> temporaryList)
        {
            TrainingHistory = new ObservableCollection<Training>();
            temporaryList.Sort((x, y) => y.Date.CompareTo(x.Date));
            foreach (var item in temporaryList)
            {
                TrainingHistory.Add(item);
            }

            
        }
    }
}
