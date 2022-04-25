using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBarsAnalyzer.Reporter
{
    public class ReporterWithHeader : IReporter
    {
        private readonly string _reportHeader = string.Empty;

        public void CreateAndSaveReport(List<TradingBar> tradingBars, string savePath)
        {
            var report = new StringBuilder();
            report.AppendLine(_reportHeader);
            foreach (var tradingBar in tradingBars)
            {
                report.AppendLine(tradingBar.ToString());
            }

            File.WriteAllText(savePath, report.ToString());
        }

        public ReporterWithHeader(string header)
        {
            _reportHeader = header;
        }
    }
}
