using System;
using System.Collections.Generic;
using System.Text;

namespace BodyWeight.Models
{
    public class Excercise
    {
        private int rowHeight = 60;

        public string ExcerciseName { get; set; } = " ";
        public int NumberOfSeries { get; set; } = 0;
        public int Repetitions { get; set; }
        public List<Serie> Series { get; set; }
        public int Height { get; set; } 

        public Excercise(string excerciseName,int numberOfSeries,int repetitions)
        {           
            ExcerciseName = excerciseName;
            NumberOfSeries = numberOfSeries;
            Repetitions = repetitions;
        }

        public Excercise(string excerciseName, int numberOfSeries, int repetitions,List<double> weights, List<int> reps)
        {
            ExcerciseName = excerciseName;
            NumberOfSeries = numberOfSeries;
            Repetitions = repetitions;
            
        }

        public Excercise()
        {

        }


        public void PrepareHeight()
        {
            Height = rowHeight * Series.Count;
        }

    }
}
