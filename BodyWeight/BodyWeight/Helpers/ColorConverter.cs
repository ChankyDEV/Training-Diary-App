using BodyWeight.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace BodyWeight.Helpers
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                
                Changings m = value as Changings;
                double change = m.Change;
                if (change < 0)
                {
                    return Color.Green;
                }
                else if(change > 0)
                {    
                    return Color.Red;
                }
                else
                {
                    return Color.Default;
                }

            }
            return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
