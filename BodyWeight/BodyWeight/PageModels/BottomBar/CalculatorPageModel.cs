using System;
using System.Collections.Generic;
using System.Text;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BodyWeight.PageModels
{

    public partial class StartingPageModel : FreshBasePageModel
    {


        public string CaloriesText { get; set; } = "Male";


        
        public Command SexChangeCommand => new Command(() =>
        {
            if (CaloriesText == "Male")
                CaloriesText = "Female";
            else
                CaloriesText = "Male";
        });
       


        public Command GoBackCommand => new Command(async () =>
        {
            await CoreMethods.PopPageModel();
        });




        public Command AnimateCommand => new Command(() =>
        {
           
        });


    }
}
