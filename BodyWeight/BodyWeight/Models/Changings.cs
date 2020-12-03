using System;
using System.Collections.Generic;
using System.Text;

namespace BodyWeight.Models
{
    public class Changings : Measurement, IComparable<Changings>
    {
        public Changings(double change,double weight, DateTime date) : base (weight,date)
        {
            Change = change;
        }

        public double Change { get; set; }

        public int CompareTo(Changings other)
        {
            return CompareTo(other);
        }
    }
}
