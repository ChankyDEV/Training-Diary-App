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

namespace BodyWeight
{
    public static class DatabaseMethods
    {
        public static FirebaseClient mDatabase { get; set; }= new FirebaseClient("https://trainingnoteapp.firebaseio.com");
        public static string authID;
       

        // Method for getting data
        async static public  Task<Account> GetUserbyEmail()
        {
            List<Account> accList = await GetAccount();

            Account acc = accList.First(); 
        
            return acc;
        }
        async static private Task<List<Account>> GetAccount()
        {
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

        // Method for posting data
        async static public void WriteUserToDataBase(string id, string email, string password, string name, string surname, List<Plan> plans, List<Training> trainings)
        {
            authID = id;
            await mDatabase.Child("users").Child(id).Child("info").PostAsync(new Account()
            {
                Name = name,
                Surname = surname,
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
        async static public void AddMeasurementToDatabase(Measurement measurement)
        {
            await mDatabase.Child("users").Child(authID).Child("measurements").PostAsync(measurement);
        }
    }
}
