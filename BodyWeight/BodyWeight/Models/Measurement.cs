using System;
using System.Collections.Generic;
using System.Text;

namespace BodyWeight.Models
{
    public class Measurement : IComparable<Measurement>
    {
        public double Weight { get; set; }
        public DateTime MeasurementDate { get; set; }

        public Measurement()
        {

        }

        public Measurement(double weight, DateTime measurementDate)
        {
            Weight = weight;
            MeasurementDate = measurementDate;
        }

        public int CompareTo(Measurement other)
        {
            return CompareTo(other);
        }
    }
}
