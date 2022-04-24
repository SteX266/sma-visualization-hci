using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sma_visualization
{
    public class SMAData
    {
        public string Symbol { get; set; }
        public string Indicator { get; set; }
        public DateTime LastRefresh { get; set; }
        public string Interval { get; set; }
        public int TimePeriod { get; set; }
        public string SeriesType { get; set; }
        public string TimeZone { get; set; }
        public List<SMA> SMAlist { get; set; }
        public SMAData(string symbol, string indicator, DateTime lastRefresh, string interval, int timePeriod, string seriesType, string timeZone, List<SMA> smalist)
        {
            Symbol = symbol;
            Indicator = indicator;
            LastRefresh = lastRefresh;
            Interval = interval;
            TimePeriod = timePeriod;
            SeriesType = seriesType;
            TimeZone = timeZone;
            SMAlist = smalist;

        }

        public SMAData()
        {
            SMAlist = new List<SMA>();  
        }
    }

        public class SMA
        {
            public DateTime Date { get; set; }
            public double Value { get; set; }

            public SMA(DateTime date, double value)
            {
                Date = date;
                Value = value;
            }
        }

    public class SMATable
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public string Value { get; set; }
        public SMATable(string symbol, string date, string value )
        {
            Symbol = symbol;
            Date = date;    
            Value = value;
        }
    }
    }
