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
            string reportPath = @"..\..\..\ReportersResults\PerDayReport.txt";

            var minMaxPerDay = TradingBar.GetMinMaxPerDay(_tradingBars);
            IReporter reporter = new SimpleReporter();
            reporter.CreateAndSaveReport(minMaxPerDay, reportPath);

            Assert.Pass();
        }

        [Test]
        public void PerHourReporterTest()
        {
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