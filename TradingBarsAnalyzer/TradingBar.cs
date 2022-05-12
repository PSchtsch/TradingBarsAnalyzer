using System.Globalization;

namespace TradingBarsAnalyzer
{
    public class TradingBar
    {
        public string Symbol { get; private set; }
        public string Description { get; private set; }

        public DateOnly Date { get; private set; }
        public TimeOnly Time { get; private set; }

        public double Open { get; private set; }
        public double High { get; private set; }
        public double Low { get; private set; }
        public double Close { get; private set; }
        public int TotalVolume { get; private set; }

        public TradingBar(
            string symbol,
            string description,
            DateOnly date,
            TimeOnly time,
            double open,
            double high,
            double low,
            double close,
            int totalVolume)
        {
            Symbol = symbol;
            Description = description;

            Date = date;
            Time = time;

            Open = open;
            High = high;
            Low = low;
            Close = close;
            TotalVolume = totalVolume;
        }

        public TradingBar(string barAsString)
        {
            var barProperties = barAsString.Split(',');

            Symbol = barProperties[0];
            Description = barProperties[1];

            Date = DateOnly.ParseExact(barProperties[2], "dd.MM.yyyy");
            Time = TimeOnly.Parse(barProperties[3]);

            Open = Convert.ToDouble(barProperties[4]);
            High = Convert.ToDouble(barProperties[5]);
            Low = Convert.ToDouble(barProperties[6]);
            Close = Convert.ToDouble(barProperties[7]);
            TotalVolume = Convert.ToInt32(barProperties[8]);
        }

        public override string ToString()
        {
            return $"{Symbol},{Description},{Date.ToString("dd.MM.yyyy")},{Time.ToString("HH:mm:ss")},{Open},{High},{Low},{Close},{TotalVolume}";
        }

        public static async Task<List<TradingBar>> CreateTradingBarsFromFileAsync(string path, string linesSeparator)
        {
            var fileLines = await ReadAndSplitAsync(path, linesSeparator);
            var tradingBars = fileLines.ToTradingBars();

            return tradingBars;
        }

        public static async Task<List<string>> ReadAndSplitAsync(string filePath, string linesSeparator)
        {
            var content = await Task.Run(() => File.ReadAllText(filePath));
            var lines = await Task.Run(() => content.Split(linesSeparator).ToList());

            return lines;
        }
    }
}