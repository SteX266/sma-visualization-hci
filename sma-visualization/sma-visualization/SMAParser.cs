using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace sma_visualization
{
    public class SMAParser
    {
        
        public SMAData parseData(string symbol = "IBM", string interval = "weekly", string timePeriod = "10", string seriesType = "open")
        {
            List<SMA> smaData = new List<SMA>();

            string QUERY_URL = "https://www.alphavantage.co/query?function=SMA&symbol="+symbol+"&interval="+ interval+"&time_period="+timePeriod+"&series_type="+seriesType+ "&apikey=U7LNNB8KPZINPTNQ";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {

                JavaScriptSerializer js = new JavaScriptSerializer();
                dynamic json_data = js.Deserialize(client.DownloadString(queryUri), typeof(object));
                Console.WriteLine(json_data);
                dynamic metaData = json_data["Meta Data"];
                dynamic SMAListRaw = json_data["Technical Analysis: SMA"];

                string SMASymbol = metaData["1: Symbol"];
                string SMAIndicator = metaData["2: Indicator"];
                DateTime SMADate = createDateObject(metaData["3: Last Refreshed"]);
                string SMAInterval = metaData["4: Interval"];
                int SMATimePeriod = Convert.ToInt32(metaData["5: Time Period"]);
                string SMASeriesType = metaData["6: Series Type"];
                string SMATimeZone = metaData["7: Time Zone"];


                foreach (string smaDate in SMAListRaw.Keys)
                {
                    DateTime date = createDateObject(smaDate);
                    SMA newSMA = new SMA(date, Math.Round(Convert.ToDouble(SMAListRaw[smaDate]["SMA"]), 2));
                    smaData.Add(newSMA);
                }

                SMAData data = new SMAData(SMASymbol, SMAIndicator, SMADate, SMAInterval, SMATimePeriod, SMASeriesType, SMATimeZone, smaData);

                return data;
            }

        }

        private DateTime createDateObject(string smaDate)
        {
            string[] tokens = smaDate.Split('-');
            int year = int.Parse(tokens[0]);
            int month = int.Parse(tokens[1]);
            string[] timeTokens = tokens[2].Split(' ');
            int day = int.Parse(timeTokens[0]);
            int hours = 0;
            int minutes = 0;
            int seconds = 0;
            if (timeTokens.Length > 1)
            {
                string [] hoursTokens = timeTokens[1].Split(':');
                hours = int.Parse(hoursTokens[0]);
                minutes = int.Parse(hoursTokens[1]);
            }

            DateTime date = new DateTime(year, month, day, hours, minutes, seconds);
            return date;
        }
    }
}
