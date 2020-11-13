using BodyWeight.Models;
using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BodyWeight
{
    public static class Session
    {
        public static Account LoggedUser {get;set;}
       public static string UserID { get; set; }
    }
}
