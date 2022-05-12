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
            var minMaxPerDay = groupedByDays.GetMinMaxPriceBars();

            IReporter reporter = new ReporterWithHeader(_simpleReporterHeader);
            string reportPath = @"..\..\..\ReportersResults\PerDayReport.txt";
            reporter.CreateAndSaveReport(minMaxPerDay.ToStrings(), reportPath);
        }

        [Test]
        public void PerHourReporterTest()
        {
            var groupedByHours = _tradingBars.GroupByTimeSpan(TimeSpanOptions.Hour);
            var hourRangesBars = groupedByHours.ConvertToRangesBars();
            
            IReporter reporter = new ReporterWithHeader(_simpleReporterHeader);
            string reportPath = @"..\..\..\ReportersResults\PerHourReport.txt";
            reporter.CreateAndSaveReport(hourRangesBars.ToStrings(), reportPath);
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

            IReporter reporter = new ReporterWithoutHeader();

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