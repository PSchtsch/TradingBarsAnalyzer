using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using TradingBarsAnalyzer;
using TradingBarsAnalyzer.Reporter;


namespace ReporterTests
{
    public class ReporterTests
    {
        private readonly List<TradingBar> _tradingBars;
        private readonly string _simpleReporterHeader;

        public ReporterTests()
        {
            var soureFilePath = @"Resources\AAPL-IQFeed-SMART-Stocks-Minute-Trade.txt";

            var linesSeparator = Environment.NewLine;
            _tradingBars = TradingBar.CreateTradingBarsFromFile(soureFilePath, linesSeparator);

            _simpleReporterHeader = "\"Symbol\",\"Description\",\"Date\",\"Time\",\"Open\",\"High\",\"Low\",\"Close\",\"TotalVolume\"";
        }


        [Test]
        public void PerDayReporterTest()
        {
            var groupedByDays = _tradingBars.GroupByTimeSpan(TimeSpanOptions.Day);

            var minMaxPerDay = new List<TradingBar>();
            foreach (var day in groupedByDays)
            {
                var minMaxPriceBar = day.GetMinMaxPriceBar();
                minMaxPerDay.Add(minMaxPriceBar.MinimumPriceBar);
                minMaxPerDay.Add(minMaxPriceBar.MaximumPriceBar);
            }

            IReporter reporter = new ReporterWithHeader(_simpleReporterHeader);
            string reportPath = @"..\..\..\ReportersResults\PerDayReport.txt";
            reporter.CreateAndSaveReport(minMaxPerDay.ToStrings(), reportPath);
        }

        [Test]
        public void PerHourReporterTest()
        {
            var groupedByHours = _tradingBars.GroupByTimeSpan(TimeSpanOptions.Hour);

            var barPerHours = new List<TradingBar>();
            foreach (var hour in groupedByHours)
            {
                var minMaxPriceBar = hour.GetMinMaxPriceBar();
                var openClosePriceBar = hour.GetOpenCloseBar();
                var totalVolume = hour.CalculateTotalVolume();

                var openBar = openClosePriceBar.OpenBar;
                var symbol = openBar.Symbol;
                var description = openBar.Description;
                var date = openBar.Date;
                var time = openBar.Time;
                var open = openBar.Open;

                var high = minMaxPriceBar.MaximumPriceBar.High;
                var low = minMaxPriceBar.MinimumPriceBar.Low;

                var close = openClosePriceBar.CloseBar.Close;

                var barPerHour = new TradingBar(
                    symbol,
                    description,
                    date,
                    time,
                    open,
                    high,
                    low,
                    close,
                    totalVolume);

                barPerHours.Add(barPerHour);
            }

            IReporter reporter = new ReporterWithHeader(_simpleReporterHeader);
            string reportPath = @"..\..\..\ReportersResults\PerHourReport.txt";
            reporter.CreateAndSaveReport(barPerHours.ToStrings(), reportPath);
        }

        [Test]
        public void NewAndLostStringCheckerTest()
        {
            var fineFilePath = @"Resources\AAPL-IQFeed-SMART-Stocks-Minute-Trade.txt";
            var corruptedFilePath = @"Resources\AAPL-IQFeed-SMART-Stocks-Minute-Trade-corrupted.txt";

            var linesSeparator = Environment.NewLine;
            var comparer = new ReportersComparer(fineFilePath, corruptedFilePath, linesSeparator);

            var newLines = comparer.GetNewLines();
            var lostLines = comparer.GetLostLines();
            var uniqueLines = comparer.GetUniqueLines();

            IReporter reporter = new ReporterWithHeader(string.Empty);

            string newLinesReportPath = @"..\..\..\ReportersResults\NewLinesReport.txt";
            reporter.CreateAndSaveReport(newLines, newLinesReportPath);

            string lostLinesReportPath = @"..\..\..\ReportersResults\LostLinesReport.txt";
            reporter.CreateAndSaveReport(lostLines, lostLinesReportPath);

            string uniqueLinesReportPath = @"..\..\..\ReportersResults\UniqueLinesReport.txt";
            reporter.CreateAndSaveReport(uniqueLines, uniqueLinesReportPath);

            Assert.Pass();
        }
    }
}