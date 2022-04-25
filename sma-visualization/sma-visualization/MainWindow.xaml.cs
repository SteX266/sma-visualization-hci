using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
using Newtonsoft.Json;

namespace sma_visualization
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public LineChart lineChart { get; set; }
        public BarChart barChart { get; set; }
        public SMAParser parser { get; set; }
        SMAData currentData { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            parser = new SMAParser();
            lineChart = new LineChart();
            barChart = new BarChart();
           
            DataContext = this;
            fillComboboxes();
            currentData = new SMAData();
            
        }
        
        private void fillComboboxes()
        {
            List<String> symbols = new List<String>();
            List<String> intervals = new List<String>();
            List<String> seriesTypes = new List<String>();

            symbols = loadSymbols();



            intervals.Add("1min");
            intervals.Add("5min");
            intervals.Add("15min");
            intervals.Add("30min");
            intervals.Add("60min");
            intervals.Add("daily");
            intervals.Add("weekly");
            intervals.Add("monthly");

            seriesTypes.Add("close");
            seriesTypes.Add("open");
            seriesTypes.Add("high");
            seriesTypes.Add("low");

            comboSymbol.ItemsSource = symbols;

            comboInterval.ItemsSource = intervals;

            comboSeriesType.ItemsSource = seriesTypes;



        }

        private List<string> loadSymbols()
        {
            List<string> symbols = new List<string>();
            using (StreamReader r = new StreamReader("../../symbols.json"))
            {
                string json = r.ReadToEnd();
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                foreach (Item item in items)
                {
                    symbols.Add(item.Symbol);
                }
            }
            return symbols;
        }

        private void ClearLineChart(object sender, RoutedEventArgs e)
        {
            lineChart.ClearData();
            barChart.ClearData();
            this.currentData = new SMAData();
            comboSymbol.SelectedIndex = -1;

            comboSeriesType.SelectedIndex = -1;
            comboInterval.SelectedIndex = -1;
            textTimePeriod.Text = "";
        }

        private void ShowLineChart(object sender, RoutedEventArgs e)
        {

            lineChart.ClearData();
            barChart.ClearData();
            string symbol = comboSymbol.Text;
            string interval = comboInterval.Text;
            string seriesType = comboSeriesType.Text;
            string timePeriod = textTimePeriod.Text;


            if (isDataValid(symbol, interval, seriesType, timePeriod))
            {
                SMAData data = parser.parseData(symbol, interval, timePeriod, seriesType);
                currentData = data;
                if (data.SMAlist.Count == 0)
                {
                    return;
                }
                XAxis.Labels = lineChart.Labels;
                lineChart.showData(data);
                barChart.showData(data);
            } else
            {
                changeBorderColors(symbol, interval, seriesType, timePeriod);
                MessageBox.Show("Invalid inputs!");
            }

        }
        private void changeBorderColors(string symbol, string interval, string seriesType, string timePeriod)
        {
            if (symbol == "")
                symbolBorder.BorderBrush = Brushes.Red;
            if(interval == "")
                intervalBorder.BorderBrush = Brushes.Red;
            if(seriesType == "")
                seriesBorder.BorderBrush = Brushes.Red;
            if(isTimePeriodValid(timePeriod))
                textTimePeriod.BorderBrush = Brushes.Red;
        }

        private bool isTimePeriodValid(string timePeriod)
        {
            try
            {
                int value = Convert.ToInt32(timePeriod);
                if (value <= 1)
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        private bool isDataValid(string symbol, string interval, string seriesType, string timePeriod)
        {
            if (symbol == "" || interval == "" || seriesType == "" || !isTimePeriodValid(timePeriod))
                return false;
            return true;
        }

        private void ShowTable(object sender, RoutedEventArgs e)
        {
            Table table = new Table(currentData);
            table.Show();
        }
    }
}
