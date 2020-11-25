using System;
using System.Collections.Generic;
using System.Text;

namespace BodyWeight.Events
{
    public class WeightEvent
    {
        public DateTime Date { get; set; }
        public double Weight { get; set; }

        public WeightEvent(DateTime date, double weight)
        {
            this.Date = date;
            this.Weight = weight;
        }

    }
}
