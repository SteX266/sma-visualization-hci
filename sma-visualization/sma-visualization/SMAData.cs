using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sma_visualization
{
    internal class SMAData
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

        internal class SMA
        {
            public DateTime Date { get; set; }
            public int Value { get; set; }

            public SMA(DateTime date, int v)
            {
                Date = date;
                Value = v;
            }
        }
    }
