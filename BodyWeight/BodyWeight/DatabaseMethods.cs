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

namespace BodyWeight
{
    public static class DatabaseMethods
    {
        public static FirebaseClient mDatabase { get; set; }= new FirebaseClient("https://trainingnoteapp.firebaseio.com");

        async static public void WriteUserToDataBase(string id,string email, string password, string name, string surname,List<Plan> plans,List<Training> trainings)
        {

            await mDatabase.Child("users").PostAsync(new Account() { 
                Name=name,
                Surname=surname,
                Email=email,
                Password=password,
                Plans=plans,
                Trainings=trainings
            });
        }


        async static public Task<Account> GetUserbyEmail(string email)
        {
            var allAccounts = await GetAccounts();
            Account acc = new Account();
            foreach (var item in allAccounts)
            {
                var lowerEmail = item.Email.ToLower();
                if (lowerEmail == email)
                {
                    return item;
                }
            }
            return acc;
        }

        async static private Task<List<Account>> GetAccounts()
        {
            return (await mDatabase
              .Child("users")
              .OnceAsync<Account>()).Select(item => new Account
              {
                  Name = item.Object.Name,
                  Surname = item.Object.Surname,
                  Email = item.Object.Email,
                  Password = item.Object.Password,
                  Plans=item.Object.Plans,
                  Trainings=item.Object.Trainings,
                  ID=item.Key                
              }).ToList();
        }

        async static public void AddPlanToDatabase()
        {
            await mDatabase.Child("users/" + Session.LoggedUser.ID).PutAsync(Session.LoggedUser);
        }
        async static public void AddTreningToDatabase()
        {
            await mDatabase.Child("users/" + Session.LoggedUser.ID).PutAsync(Session.LoggedUser);
        }


    }
}
