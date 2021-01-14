using BodyWeight.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Firebase.Database;
using Firebase.Database.Query;
using Java.Lang.Annotation;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json.Linq;
using Firebase.Storage;
using Firebase.Auth;

namespace BodyWeight
{
    public static class DatabaseMethods
    {
        private static string webApiKey = "AIzaSyDjGLLGY1sWENpq0S07OGvkDm6WyetxyJA";
        public static string authID;
        private static string storageID = "trainingnoteapp.appspot.com";
        private static string filename = "profile.jpg";
        private static FirebaseClient mDatabase { get; set; }= new FirebaseClient("https://trainingnoteapp.firebaseio.com");
        private static FirebaseStorage mStorage { get; set; } = new FirebaseStorage(storageID);
        

        

        // Method for getting data
        async static public  Task<Account> GetUserbyEmail()
        {
            List<Account> accList = await GetAccount();

            Account acc = accList.First(); 
        
            return acc;
        }
        async static public Task<List<Account>> GetAccount()
        {
            var a = authID;


            return (await mDatabase
              .Child("users")
              .Child(authID)
              .Child("info")
              .OnceAsync<Account>()).Select(item => new Account
              {
                  Name = item.Object.Name,
                  Surname = item.Object.Surname,
                  Email = item.Object.Email,
                  Password = item.Object.Password
              }).ToList();
        }
        async static public  Task<List<Plan>> GetPlans()
        {
             return (await mDatabase
            .Child("users")
            .Child(authID)
            .Child("plans")
            .OnceAsync<Plan>()).Select(i => new Plan
            {
                PlanName=i.Object.PlanName,
                DayOfRepeat=i.Object.DayOfRepeat,
                Excercises=i.Object.Excercises
            }).ToList();

        }
        async static public  Task<List<Training>> GetTrainings()
        {
            return (await mDatabase
           .Child("users")
           .Child(authID)
           .Child("trainings")
           .OnceAsync<Training>()).Select(i => new Training
           {
               Plan=i.Object.Plan,
               Date=i.Object.Date,
               TodayExcercises=i.Object.TodayExcercises             
           }).ToList();

        }
        async static public Task<List<Measurement>> GetMeasurements()
        {
            return (await mDatabase
           .Child("users")
           .Child(authID)
           .Child("measurements")
           .OnceAsync<Measurement>()).Select(i => new Measurement
           {
               MeasurementDate=i.Object.MeasurementDate,
               Weight=i.Object.Weight
           }).ToList();

        }
        public static async Task<string> GetProfileImage()
        {
            return await mStorage
                .Child(authID)
                .Child(filename)
                .GetDownloadUrlAsync();


        }

        // Method for posting data
        async static public void WriteUserToDataBase(string id, string email, string password, string name, List<Plan> plans, List<Training> trainings)
        {
            authID = id;
            await mDatabase.Child("users").Child(id).Child("info").PostAsync(new Account()
            {
                Name = name,
                Email = email,
                Password = password
            });
        }
        async static public void AddPlanToDatabase(Plan plan)
        {
            await mDatabase.Child("users").Child(authID).Child("plans").PostAsync(plan);
        }
        async static public void AddTreningToDatabase(Training training)
        {
            await mDatabase.Child("users").Child(authID).Child("trainings").PostAsync(training);
        }
        async static public Task AddMeasurementToDatabase(Measurement measurement)
        {
            await mDatabase.Child("users").Child(authID).Child("measurements").PostAsync(measurement);
        }
        async static public Task<string> StoreImage(System.IO.Stream imageStream)
        {
            var stroageImage = await mStorage
                .Child(authID)
                .Child("profile.jpg")
                .PutAsync(imageStream);
            string imgurl = stroageImage;
            return imgurl;

        }
    
    
        // User update
        async static public void ResetPassword()
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
            var profile = await GetAccount();
            var email = profile[0].Email;
            await authProvider.SendPasswordResetEmailAsync(email);
        }
    }
}
