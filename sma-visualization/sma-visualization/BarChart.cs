using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace sma_visualization
{
    public class BarChart
    {
        public SMAData data { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public List<string> Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        public BarChart()
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

            foreach (string date in dates)
            {
                Labels.Add(date);
            }
            ChartValues<double> chartValues = new ChartValues<double>(values);
            double min = chartValues.Min();
            double max = chartValues.Max();
            SeriesCollection.Add(new ColumnSeries()
            {
                Title = givenData.Symbol,
                Values = chartValues,
            });
        }
        public void ClearData()
        {
            SeriesCollection.Clear();
        }
    }
}
