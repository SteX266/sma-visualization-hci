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
        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public LineChart()
        {
            SeriesCollection = new SeriesCollection();
            Labels = new List<string>();
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
                dates.Add(point.Date.ToString("MM yyyy"));
            }
            values.Reverse();
            dates.Reverse();


            int n = dates.Count;
            Labels.Add(dates[0]);
            Labels.Add(dates[n / 2]);
            Labels.Add(dates[n - 1]);

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
