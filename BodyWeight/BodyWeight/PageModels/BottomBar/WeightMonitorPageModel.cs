using BodyWeight.Models;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using BodyWeight.Pages.PopUps;
using BodyWeight.Events;
using System.Linq;
using System.Threading.Tasks;

namespace BodyWeight.PageModels
{
    public partial class StartingPageModel : FreshBasePageModel
    {
        public string Text { get; set; } = "Historia";
        public ObservableCollection<Changings> Measurements { get; set; }
        public PlotModel Model { get; set; }
        public DateTimeAxis XAXIS { get; set; }
        public LinearAxis YAXIS { get; set; }
        public LineSeries Series { get; set; }

        public bool IsChartLoading { get; set; } = true;
        public bool IsChartVisible { get; set; } = false;

        private void DrawPlot()
        {
            PrepareBottomAxis();
            PrepareLeftAxis();

            Series = new LineSeries();
            Series.Color= OxyColor.Parse("#ea7571");    
            
            Model.Series.Add(Series);
            Model.TextColor = OxyColor.Parse("#BEBEBE");
            YAXIS.MajorGridlineColor = OxyColor.Parse("#D0D0D0");
            YAXIS.TicklineColor = OxyColor.Parse(Color.WhiteSmoke.ToHex());
            XAXIS.TicklineColor = OxyColor.Parse(Color.WhiteSmoke.ToHex());

            Measurements = new ObservableCollection<Changings>();
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

        private async void ConfigurePlot(PageEvent obj)
        {
            // when downolad the measurments look for the oldest one and draw axes diffrently
            IsChartLoading = true;
            IsChartVisible = false;
            await HandleDatabaseRequest();
            IsChartLoading = false;
            IsChartVisible = true;

        }

        private async Task HandleDatabaseRequest()
        {
           List<Measurement> measurements = await DatabaseMethods.GetMeasurements();

            if(measurements.Count != 0)
            {
                measurements.Sort((x,y)=> y.MeasurementDate.CompareTo(x.MeasurementDate));
                
                RedefineList(measurements);

                //RecalculateChanges();

                RedrawPlot();

                AddMeasurmentsToPlot();
              
            }
           
        }

        private void RedefineList(List<Measurement> measurements)
        {
            Measurements.Clear();
            int iterator = 0;
            for (int i = 1; i < measurements.Count; i++)
            {
                var change = measurements[i - 1].Weight - measurements[i].Weight;
                Changings c = new Changings(change, measurements[i - 1].Weight, measurements[i - 1].MeasurementDate);
                Measurements.Add(c);
                iterator++;
            }           
            Changings ch = new Changings(0, measurements[measurements.Count - 1].Weight, measurements[measurements.Count - 1].MeasurementDate);
            Measurements.Add(ch);
        }

        private void RecalculateChanges()
        {
            int iterator = 0;
            for (int i = 1; i < Measurements.Count; i++)
            {
                Measurements[iterator].Change = Measurements[i-1].Weight - Measurements[i].Weight;
                iterator++;
            }
            Measurements[Measurements.Count - 1].Change = 0;
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

        private async void AddWeight(WeightEvent obj)
        {
            double date = DateTimeAxis.ToDouble(obj.Date);
            double weight = obj.Weight;
            var point = new DataPoint(date, weight);
            // here we will add to DB not local serie
            Measurement newMeasurment = new Measurement();
            if(Measurements.Count==0)
            {
                newMeasurment.MeasurementDate = obj.Date;
                newMeasurment.Weight = weight;
            }
            else
            {
                
                Measurement lastMeasurement = new Measurement();
                lastMeasurement = FindLastMeasurment(obj);
                newMeasurment.MeasurementDate = obj.Date;
                newMeasurment.Weight = weight;
            }          
             await DatabaseMethods.AddMeasurementToDatabase(newMeasurment);
             await HandleDatabaseRequest();
        }

        private Measurement FindLastMeasurment(WeightEvent mes)
        {
            Measurement m = new Measurement();
            if (mes.Date < Measurements[Measurements.Count - 1].MeasurementDate)
            { 
                m.Weight = mes.Weight;
                m.MeasurementDate = mes.Date;
            }
            else
            {
                m = Measurements.Where(d => d.MeasurementDate <= mes.Date).First(); 
            }

            return m;
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
