using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace sma_visualization
{
    public partial class LineChart : UserControl
    {
        public SMAData data { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public LineChart()
        {
            SeriesCollection = new SeriesCollection();
            Labels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
        }



        public void showData(SMAData givenData)
        {
            data = givenData;
            List<double> values = new List<double>();
            List<string> dates = new List<string>();
            List<SMA> points = data.SMAlist;
            foreach (SMA point in points)
            {
                values.Add(point.Value);
                dates.Add(point.Date.ToString());
            }
            values.Reverse();
            dates.Reverse();
            SeriesCollection.Add(new LineSeries
            {
                Title = givenData.Symbol,
                Values = new ChartValues<double>(values),
                LineSmoothness = 0,
                PointGeometry = null,
                Fill = new SolidColorBrush()
            });
        }

        public void ClearData()
        {
            SeriesCollection.Clear();
        }
    }
}
