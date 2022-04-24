using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace sma_visualization
{

    public partial class Table : Window
    {
        public Table(SMAData data)
        {
            InitializeComponent();
            symbolLabel.Content = data.Symbol;
            foreach(SMA sma in data.SMAlist)
            {
                string dateString;
                if (data.Interval == "daily" || data.Interval == "weekly" || data.Interval == "monthly")
                {
                    dateString = sma.Date.ToString("dd.MM.yyyy");

                }
                else
                {
                    dateString = sma.Date.ToString("dd.MM.yyyy HH:mm");
                }

                SMATable tableItem = new SMATable(data.Symbol, dateString, sma.Value.ToString() + " USD");

                TableGrid.Items.Add(tableItem);
            }

        }
    }
}
