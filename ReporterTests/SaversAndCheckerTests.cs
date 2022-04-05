using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using TradingBarsAnalyzer;

namespace ReporterTests
{
    public class Tests
    {
        [Test]
        [TestCase(@"Resources\AAPL-IQFeed-SMART-Stocks-Minute-Trade.txt")]
        public void PerDayReporterTest(string readPath)
        {
            var tradingBars = TradingBar.CreateTradingBarsFromFile(readPath);

            Assert.Pass();
        }

        [Test]
        public void PerHourReporterTest()
        {

            Assert.Pass();
        }

        [Test]
        public void NewAndLostStringCheckerTest()
        {


            Assert.Pass();
        }
    }
}