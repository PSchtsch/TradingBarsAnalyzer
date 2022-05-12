using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TradingBarsAnalyzer;
using TradingBarsAnalyzer.Reporter;

namespace ReporterSavers
{
    public class ReporterSavers
    {
        private readonly string _sourceFilePath;
        private readonly string _corruptedFilePath;

        private readonly string _linesSeparator;
        private readonly string _simpleReporterHeader;

        public ReporterSavers()
        {
            _sourceFilePath = @"Resources\AAPL-IQFeed-SMART-Stocks-Minute-Trade.txt";
            _corruptedFilePath = @"Resources\AAPL-IQFeed-SMART-Stocks-Minute-Trade-corrupted.txt";

            _linesSeparator = Environment.NewLine;
            _simpleReporterHeader = "\"Symbol\",\"Description\",\"Date\",\"Time\",\"Open\",\"High\",\"Low\",\"Close\",\"TotalVolume\"";
        }

        [Test]
        public async Task MinMaxPriceBarPerDayAsync()
        {
            var tradingBars = await TradingBar.CreateTradingBarsFromFileAsync(_sourceFilePath, _linesSeparator);
            var groupedByDays = tradingBars.GroupByTimeSpan(TimeSpanOptions.Day);
            var minMaxPerDay = groupedByDays.GetMinMaxPriceBars();

            IReporter reporter = new ReporterWithHeader(_simpleReporterHeader);
            string reportPath = @"..\..\..\ReportersResults\MinMaxPricePerDayReport.txt";
            reporter.CreateAndSaveReport(minMaxPerDay.ToStrings(), reportPath);
        }

        [Test]
        public async Task RangeBarPerHourAsync()
        {
            var tradingBars = await TradingBar.CreateTradingBarsFromFileAsync(_sourceFilePath, _linesSeparator);
            var groupedByHours = tradingBars.GroupByTimeSpan(TimeSpanOptions.Hour);
            var hourRangesBars = groupedByHours.ConvertToRangesBars();

            IReporter reporter = new ReporterWithHeader(_simpleReporterHeader);
            string reportPath = @"..\..\..\ReportersResults\HourRangesReport.txt";
            reporter.CreateAndSaveReport(hourRangesBars.ToStrings(), reportPath);
        }

        [Test]
        public async Task CompareFilesAsync()
        {
            var comparer = new ReportersComparer(_sourceFilePath, _corruptedFilePath, _linesSeparator);

            var newLines = comparer.GetNewLinesAsync();
            var lostLines = comparer.GetLostLinesAsync();
            var uniqueLines = comparer.GetUniqueLinesAsync();

            IReporter reporter = new ReporterWithoutHeader();

            string newLinesReportPath = @"..\..\..\ReportersResults\NewLinesReport.txt";
            string lostLinesReportPath = @"..\..\..\ReportersResults\LostLinesReport.txt";
            string uniqueLinesReportPath = @"..\..\..\ReportersResults\UniqueLinesReport.txt";

            reporter.CreateAndSaveReport(await newLines, newLinesReportPath);
            reporter.CreateAndSaveReport(await lostLines, lostLinesReportPath);
            reporter.CreateAndSaveReport(await uniqueLines, uniqueLinesReportPath);
        }
    }
}