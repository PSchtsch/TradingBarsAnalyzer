using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using TradingBarsAnalyzer;
using TradingBarsAnalyzer.Reporter;


namespace ReporterTests
{
    public class ReporterTests
    {
        private readonly List<TradingBar> _tradingBars;

        public ReporterTests()
        {
            var soureFilePath = @"Resources\AAPL-IQFeed-SMART-Stocks-Minute-Trade.txt";

            _tradingBars = TradingBar.CreateTradingBarsFromFile(soureFilePath);
        }


        [Test]
        public void PerDayReporterTest()
        {
            var barsPerDay = _tradingBars.GroupByTimeSpan(TimeSpanOptions.Day);

            var minMaxPerDay = new List<TradingBar>();
            foreach (var day in barsPerDay)
            {
                var minMax = day.GetMinMax();
                minMaxPerDay.Add(minMax.MinimumPrice);
                minMaxPerDay.Add(minMax.MaximumPrice);
            }

            //reporter with simple header
            IReporter reporter = new SimpleReporter();
            string reportPath = @"..\..\..\ReportersResults\PerDayReport.txt";
            reporter.CreateAndSaveReport(minMaxPerDay, reportPath);

            Assert.Pass();
        }

        [Test]
        public void PerHourReporterTest()
        {
            string reportPath = @"..\..\..\ReportersResults\PerHourReport.txt";

            var hmm = AppDomain.CurrentDomain.BaseDirectory;

            Assert.Pass();
        }

        [Test]
        public void NewAndLostStringCheckerTest()
        {


            Assert.Pass();
        }
    }
}