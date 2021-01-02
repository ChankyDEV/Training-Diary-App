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
                    ToObservable(temporaryList);
                }
                else
                {
                    TrainingHistory = new ObservableCollection<Training>();
                }
                
            }
            catch(Exception ex)
            {
                TrainingHistory = new ObservableCollection<Training>();
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
