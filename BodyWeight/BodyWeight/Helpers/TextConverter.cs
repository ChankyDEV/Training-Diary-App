using System;
using System.Collections.Generic;
using System.Text;

namespace BodyWeight.Helpers
{
    public static class TextConverter
    {
        public static string PrepareEmail(string unCorrectEmail)
        {
            string correctEmail = unCorrectEmail.ToLower();
            if(correctEmail.EndsWith(" "))
            {
                correctEmail.Trim();
            }

            return correctEmail;
        }
    }
}
