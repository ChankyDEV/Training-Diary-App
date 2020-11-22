using BodyWeight.Models;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using BodyWeight.Pages.PopUps;

namespace BodyWeight.PageModels
{
    public partial class StartingPageModel : FreshBasePageModel
    {
        public string Text { get; set; } = "Historia";
        public ObservableCollection<Measurement> Measurements { get; set; }

        public PlotModel Model { get; set; }
        public DateTimeAxis XAXIS { get; set; }
        public LinearAxis YAXIS { get; set; }

      

        private void CreateAxes()
        {
            

            var startDate = DateTime.Now.AddDays(-10);
            var endDate = DateTime.Now;
            XAXIS.Position = AxisPosition.Bottom;      
            XAXIS.Minimum = DateTimeAxis.ToDouble(startDate);
            XAXIS.Maximum = DateTimeAxis.ToDouble(endDate);
            XAXIS.StringFormat = "M/d";

            YAXIS.Position = AxisPosition.Left;
            YAXIS.Maximum = 80;
            YAXIS.Minimum = 50;
            YAXIS.CropGridlines = true;
            YAXIS.MajorGridlineStyle = LineStyle.Solid;

            Model.PlotAreaBorderColor = OxyColor.Parse("#d3d3d3");

            Model.Axes.Add(XAXIS);
            Model.Axes.Add(YAXIS);
            

            LineSeries series = new LineSeries();

            var point = new DataPoint(DateTimeAxis.ToDouble(DateTime.Now.AddDays(-10)),68);
            var point3 = new DataPoint(DateTimeAxis.ToDouble(DateTime.Now.AddDays(-7)), 65.8);
            var point1 = new DataPoint(DateTimeAxis.ToDouble(DateTime.Now.AddDays(-4)), 65.2);
            var point2 = new DataPoint(DateTimeAxis.ToDouble(DateTime.Now.AddDays(-3)), 62);

            series.Points.Add(point);
            series.Points.Add(point3);
            series.Points.Add(point1);
            series.Points.Add(point2);

            Model.Series.Add(series);

        }



        public Command AddMeasurmentCommand => new Command(async () =>
        {
            await PopupNavigation.Instance.PushAsync(new AddMeasurmentPopUp());


        });

        

    }
}
