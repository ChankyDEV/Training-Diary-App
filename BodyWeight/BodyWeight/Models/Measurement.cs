using System;
using System.Collections.Generic;
using System.Text;

namespace BodyWeight.Models
{
    public class Measurement
    {
        public double Weight { get; set; }
        public DateTime MeasurementDate { get; set; }
        public double Change { get; set; }
    }
}
