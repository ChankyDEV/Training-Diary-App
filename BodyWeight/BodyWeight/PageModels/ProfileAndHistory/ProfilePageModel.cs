using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using BodyWeight.Models;
using Firebase.Storage;
using FreshMvvm;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace BodyWeight.PageModels.ProfileAndHistory
{

  
    public class ProfilePageModel : FreshBasePageModel
    {

        public string ChoosenImage { get; set; } = "";
        public string Firstname { get; set; } = "";
        public string Email { get; set; } = "";
        public int PlansCount { get; set; } = 0;
        public int TrainingsCount { get; set; } = 0;
        public int MeasurmentsCount { get; set; } = 0;

        public ProfilePageModel()
        {
            GetProfileImage();
            GetUserInformation();
        }

        private async void GetUserInformation()
        {
            var plans = await DatabaseMethods.GetPlans();
            var trainings = await DatabaseMethods.GetTrainings();
            var measurments = await DatabaseMethods.GetMeasurements();
            var info = await DatabaseMethods.GetAccount();

            Account userInfo = info[0];

            PlansCount = plans.Count;
            TrainingsCount = trainings.Count;
            MeasurmentsCount = measurments.Count;
            Firstname = userInfo.Name;
            Email = userInfo.Email;

        }

        private async void GetProfileImage()
        {
            try
            {
                var image = await GetProfileImageAsync();
                if(image!=null)
                {
                    ChoosenImage = image.ToString();
                }
            }
            catch(Exception e)
            {
                ChoosenImage = "face2.jpg";
            }
            
            
         
        }
        private async Task<string> GetProfileImageAsync()
        {
            return await DatabaseMethods.GetProfileImage();
    
        }

       private async Task<string> StoreImages(System.IO.Stream stream)
        {
            return await DatabaseMethods.StoreImage(stream);
        }


        public Command GoBackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel();

        });
        public Command AddFromDiskCommand => new Command(async () =>
        {
            await CrossMedia.Current.Initialize();

            MediaFile file;

            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                    return;

                ChoosenImage = await StoreImages(file.GetStream());

            }
            catch (Exception ex)
            {
                Debug.WriteLine("XDDDDDDDDDDDD"+ex.Message);
            }

        });

        public Command ResetPasswordCommand => new Command(async () =>
        {
            DatabaseMethods.ResetPassword();
        });


    }
}
