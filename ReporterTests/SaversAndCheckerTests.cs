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
            var groupedByDays = _tradingBars.GroupByTimeSpan(TimeSpanOptions.Day);

            var minMaxPerDay = new List<TradingBar>();
            foreach (var day in groupedByDays)
            {
                var minMaxPriceBar = day.GetMinMaxPriceBar();
                minMaxPerDay.Add(minMaxPriceBar.MinimumPriceBar);
                minMaxPerDay.Add(minMaxPriceBar.MaximumPriceBar);
            }

            //reporter with simple header
            IReporter reporter = new SimpleReporter();
            string reportPath = @"..\..\..\ReportersResults\PerDayReport.txt";
            reporter.CreateAndSaveReport(minMaxPerDay, reportPath);
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

            IReporter reporter = new SimpleReporter();
            string reportPath = @"..\..\..\ReportersResults\PerHourReport.txt";
            reporter.CreateAndSaveReport(barPerHours, reportPath);
        }

        [Test]
        public void NewAndLostStringCheckerTest()
        {


            Assert.Pass();
        }
    }
}