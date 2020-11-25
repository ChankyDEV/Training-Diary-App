using BodyWeight.Events;
using BodyWeight.Pages.BottomBar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BodyWeight.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartingPage : TabbedPage
    {
        public StartingPage()
        {
            InitializeComponent();
        }

        bool loaded = false;

        private void TabbedPage_CurrentPageChanged(object sender, EventArgs e)
        {
            if (loaded == true)
            {
                if (this.CurrentPage == Children[1])
                {
                    MessagingCenter.Send<PageEvent>(new PageEvent(), "User chose weight page");
                }
            }
            loaded = true;
        }

     
    }
}