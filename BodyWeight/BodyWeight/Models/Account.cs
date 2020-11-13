using System;
using System.Collections.Generic;
using System.Text;

namespace BodyWeight.Models
{
    public class Account
    {
        public string ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<Plan> Plans { get; set; }
        public List<Training> Trainings { get; set; }


        public Account()
        {

        }
        public Account(string email, string password,string name,string surname)
        {
            Email = email;
            Password = password;
            Name = name;
            Surname = surname;

        }
    }
}
