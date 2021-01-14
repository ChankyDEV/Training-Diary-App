using System;
using System.Collections.Generic;
using System.Text;

namespace BodyWeight.Models
{
    public class Training
    {
        public string Plan { get; set; }
        public DateTime Date { get; set; }
        public List<Excercise> TodayExcercises { get; set; }

    
        public Training(DateTime date, List<Excercise> todayExcercises)
        {
            Date = date;
            TodayExcercises = todayExcercises;
           
        }
        public Training()
        {

        }
    }
}
