using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BodyWeight.Helpers
{
    public class ClickableListView : ListView
    {
        public static BindableProperty ItemClickCommandProperty = BindableProperty.Create(nameof(ItemClickCommand), typeof(ICommand),typeof(ICommand));

        public ICommand ItemClickCommand
        {
            get 
            {
                return (ICommand)this.GetValue(ItemClickCommandProperty);
            }
            set
            {
                this.SetValue(ItemClickCommandProperty, value);
            }
        }

        public ClickableListView()
        {
            this.ItemTapped += OnItemClicked;
        }

        private void OnItemClicked(object sender, ItemTappedEventArgs e)
        {
            if(e.Item!=null)
            {
                ItemClickCommand?.Execute(e.Item);
                SelectedItem = null;
            }
        }
    }
}
