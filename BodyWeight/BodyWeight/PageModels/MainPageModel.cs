using System;
using System.Collections.Generic;
using System.Text;
using BodyWeight.Helpers;
using FreshMvvm;
using Xamarin.Forms;

namespace BodyWeight.PageModels
{
    public partial class MainPageModel : FreshBasePageModel     
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";



    }
}
