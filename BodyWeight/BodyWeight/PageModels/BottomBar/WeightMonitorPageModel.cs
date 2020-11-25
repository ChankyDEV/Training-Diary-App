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
using BodyWeight.Events;
using System.Linq;

namespace BodyWeight.PageModels
{
    public partial class StartingPageModel : FreshBasePageModel
    {
        public string Text { get; set; } = "Historia";
        public ObservableCollection<Measurement> Measurements { get; set; }

        public PlotModel Model { get; set; }
        public DateTimeAxis XAXIS { get; set; }
        public LinearAxis YAXIS { get; set; }

        public LineSeries Series { get; set; }


        private void DrawPlot()
        {
            PrepareBottomAxis();
            PrepareLeftAxis();

            Series = new LineSeries();
            Series.Color= OxyColor.Parse("#4B0082");          
            Model.Series.Add(Series);
            Model.InvalidatePlot(true);
        }

        private void PrepareLeftAxis()
        {
            YAXIS.Position = AxisPosition.Left;
            YAXIS.Maximum = 80;
            YAXIS.Minimum = 50;
            YAXIS.CropGridlines = true;
            YAXIS.MinorGridlineStyle = LineStyle.Solid;
            YAXIS.MajorGridlineStyle = LineStyle.Solid;
            Model.PlotAreaBorderColor = OxyColor.Parse("#d3d3d3");
            Model.Axes.Add(YAXIS);
        }

        private void PrepareBottomAxis()
        {
                
            XAXIS.Minimum = DateTimeAxis.ToDouble(DateTime.Now.AddDays(-31));
            XAXIS.Maximum = DateTimeAxis.ToDouble(DateTime.Now.AddDays(7));
            XAXIS.Position = AxisPosition.Bottom;
            XAXIS.StringFormat = "MMM";
            Model.Axes.Add(XAXIS);
        }

        private void ConfigurePlot(PageEvent obj)
        {
            // when downolad the measurments look for the oldest one and draw axes diffrently
            HandleDatabaseRequest();
            
        }

        private async void HandleDatabaseRequest()
        {
           List<Measurement> measurements = await DatabaseMethods.GetMeasurements();

            // todo: sortowanie
            measurements.Sort((x,y)=> y.MeasurementDate.CompareTo(x.MeasurementDate));


            Measurements = new ObservableCollection<Measurement>(measurements);

            RedrawPlot();

            AddMeasurmentsToPlot();
        }

        private void RedrawPlot()
        {

            var lowestWeight = Measurements.Min(x => x.Weight);
            var heighestWeight = Measurements.Max(x => x.Weight);

            var lowestDate = Measurements.Min(x => x.MeasurementDate);
            var heighestDate = Measurements.Max(x => x.MeasurementDate);


            YAXIS.Minimum = lowestWeight - 5;
            YAXIS.Maximum = heighestWeight + 5;

            XAXIS.Minimum = DateTimeAxis.ToDouble(lowestDate);
            XAXIS.Maximum = DateTimeAxis.ToDouble(heighestDate);
        }

        public Command AddMeasurmentCommand => new Command(async () =>
        {
            await PopupNavigation.Instance.PushAsync(new AddMeasurmentPopUp());

        });

        private void AddWeight(WeightEvent obj)
        {
            double date = DateTimeAxis.ToDouble(obj.Date);
            double weight = obj.Weight;
            var point = new DataPoint(date, weight);
            // here we will add to DB not local serie
            Measurement newMeasurment = new Measurement();
            if(Measurements.Count==0)
            {
                newMeasurment.Change = 0;
                newMeasurment.MeasurementDate = obj.Date;
                newMeasurment.Weight = weight;
            }
            else
            {
                var count = Measurements.Count-1;
                Measurement lastMeasurement = new Measurement();
                lastMeasurement = Measurements[count];
                newMeasurment.MeasurementDate = obj.Date;
                newMeasurment.Weight = weight;
                newMeasurment.Change = newMeasurment.Weight- lastMeasurement.Weight;
            }          
            DatabaseMethods.AddMeasurementToDatabase(newMeasurment);
            HandleDatabaseRequest();
        }

        private void AddMeasurmentsToPlot()
        {
            if(Measurements!=null)
            {
                Series.Points.Clear();
                 foreach (var item in Measurements)
                 {
                     var date = DateTimeAxis.ToDouble(item.MeasurementDate);
                     var weight = item.Weight;
                     var point = new DataPoint(date,weight);
                     Series.Points.Add(point);
                 }
                Model.InvalidatePlot(true);
                }
            }
            
    }
}
