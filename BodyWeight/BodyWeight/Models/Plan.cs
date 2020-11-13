using System;
using System.Collections.Generic;
using System.Text;

namespace BodyWeight.Models
{
    public class Plan
    {
        public string PlanName { get; set; }
        public string DayOfRepeat { get; set; }       
        public List<Excercise> Excercises { get; set; }

        public Plan()
        {

        }
        public Plan(string name,string day, List<Excercise> ex)
        {
            PlanName = name;
            DayOfRepeat=day;
            Excercises = AddToList(ex);

        }

        private List<Excercise> AddToList(List<Excercise> ex)
        {
            List<Excercise> list = new List<Excercise>();

            foreach (var item in ex)
            {
                list.Add(item);
            }

            return list;
        }

      
    }
}
