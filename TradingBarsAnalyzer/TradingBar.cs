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

        public TradingBar()
        {

        }

        public override string ToString()
        {
            return $"{Symbol},{Description},{Date},{Time},{Open},{High},{Low},{Close},{TotalVolume}";
        }
    }
}