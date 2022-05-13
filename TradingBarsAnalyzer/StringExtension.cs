namespace TradingBarsAnalyzer
{
    public static class StringExtension
    {
        public static List<TradingBar> ToTradingBars(this List<string> lines)
        {
            TradingBar tradingBar;
            var tradingBars = new List<TradingBar>();
            foreach (var line in lines)
            {
                try
                {
                    tradingBar = new TradingBar(line);
                }
                catch (Exception)
                {
                    continue;
                }

                tradingBars.Add(tradingBar);
            }

            return tradingBars;
        }
    }
}
