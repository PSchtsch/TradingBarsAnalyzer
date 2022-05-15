namespace TradingBarsAnalyzer
{
    public static class TradingBarExtension
    {
        public static List<List<TradingBar>> GroupByTimeSpan(this List<TradingBar> tradingBars, TimeSpanOptions timeSpan)
        {
            var groups = new List<List<TradingBar>>();
            var currentGroup = new List<TradingBar>();

            var firstBar = tradingBars[0];
            currentGroup.Add(firstBar);

            var tradingBarsCount = tradingBars.Count;
            for (int i = 1; i < tradingBarsCount; i++)
            {
                var currentBar = tradingBars[i];
                var previousBar = tradingBars[i - 1];

                if (currentBar.InSameTimeSpan(previousBar, timeSpan))
                {
                    currentGroup.Add(currentBar);
                }
                else
                {
                    groups.Add(currentGroup);
                    currentGroup = new List<TradingBar>();
                    currentGroup.Add(currentBar);
                }

                if (i == tradingBarsCount - 1)
                {
                    groups.Add(currentGroup);
                }
            }

            return groups;
        }

        private static bool InSameTimeSpan(this TradingBar source, TradingBar target, TimeSpanOptions timeSpan)
        {
            switch (timeSpan)
            {
                case TimeSpanOptions.Hour:
                    return source.Time.Hour == target.Time.Hour;
                case TimeSpanOptions.Day:
                    return source.Date == target.Date;
                default: throw new Exception($"Unknown time span {timeSpan}");
            }
        }

        public static List<TradingBar> GetMinMaxPriceBars(this List<List<TradingBar>> groupedTradingBars)
        {
            var minMaxInGroup = new List<TradingBar>();
            foreach (var group in groupedTradingBars)
            {
                var minMaxPriceBar = group.GetMinMaxPriceBar();
                minMaxInGroup.Add(minMaxPriceBar.MinimumPriceBar);
                minMaxInGroup.Add(minMaxPriceBar.MaximumPriceBar);
            }

            return minMaxInGroup;
        }

        public static (TradingBar MinimumPriceBar, TradingBar MaximumPriceBar) GetMinMaxPriceBar(this List<TradingBar> tradingBars)
        {
            TradingBar tempMinBar = tradingBars[0];
            TradingBar tempMaxBar = tradingBars[0];

            var tradingBarsCount = tradingBars.Count;
            for (int i = 1; i < tradingBarsCount; i++)
            {
                var currentBar = tradingBars[i];

                if (currentBar.Low < tempMinBar.Low)
                {
                    tempMinBar = currentBar;
                }

                if (currentBar.High > tempMaxBar.High)
                {
                    tempMaxBar = currentBar;
                }
            }

            return (tempMinBar, tempMaxBar);
        }

        /// <summary>
        /// Trading bars with special properties:
        /// Open - price which range starts with, 
        /// Close - price which range ends with, 
        /// High - max price on range, 
        /// Low - minimum price on range, 
        /// TotalVolume - sum of Volumes on range
        /// </summary>
        /// <param name="groupedTradingBars"></param>
        /// <returns></returns>
        public static List<TradingBar> ConvertToRangesBars(this List<List<TradingBar>> groupedTradingBars)
        {
            List<TradingBar> rangesBars = new List<TradingBar>();
            foreach (var group in groupedTradingBars)
            {
                var rangeBar = group.ConvertToRangeBar();
                rangesBars.Add(rangeBar);
            }

            return rangesBars;
        }

        /// <summary>
        /// Trading bar with special properties:
        /// Open - price which range starts with, 
        /// Close - price which range ends with, 
        /// High - max price on range, 
        /// Low - minimum price on range, 
        /// TotalVolume - sum of Volumes on range
        /// </summary>
        /// <param name="tradingBars"></param>
        /// <returns></returns>
        public static TradingBar ConvertToRangeBar(this List<TradingBar> tradingBars)
        {
            var minMaxPriceBar = tradingBars.GetMinMaxPriceBar();
            var openClosePriceBar = tradingBars.GetOpenCloseBar();
            var totalVolume = tradingBars.CalculateTotalVolume();

            var openBar = openClosePriceBar.OpenBar;
            var symbol = openBar.Symbol;
            var description = openBar.Description;
            var date = openBar.Date;
            var time = openBar.Time;
            var open = openBar.Open;

            var high = minMaxPriceBar.MaximumPriceBar.High;
            var low = minMaxPriceBar.MinimumPriceBar.Low;

            var close = openClosePriceBar.CloseBar.Close;

            var rangeAsBar = new TradingBar(
                symbol,
                description,
                date,
                time,
                open,
                high,
                low,
                close,
                totalVolume);

            return rangeAsBar;
        }

        public static (TradingBar OpenBar, TradingBar CloseBar) GetOpenCloseBar(this List<TradingBar> tradingBars)
        {
            var tradingBarsCount = tradingBars.Count;

            var openPrice = tradingBars[0];
            var closePrice = tradingBars[tradingBarsCount - 1];

            return (openPrice, closePrice);
        }

        public static int CalculateTotalVolume(this List<TradingBar> tradingBars)
        {
            int totalVolume = 0;
            foreach (var tradingBar in tradingBars)
            {
                totalVolume += tradingBar.TotalVolume;
            }

            return totalVolume;
        }

        public static List<string> ToStrings(this List<TradingBar> tradingBars)
        {
            List<string> barsAsStrings = new List<string>();
            foreach (var tradingBar in tradingBars)
            {
                barsAsStrings.Add(tradingBar.ToString());
            }

            return barsAsStrings;
        }
    }
}
