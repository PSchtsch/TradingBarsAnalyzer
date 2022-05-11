using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TradingBarsAnalyzer.Reporter
{
    public class ReporterWithHeader : IReporter
    {
        private readonly string _reportHeader = string.Empty;

        public ReporterWithHeader(string header)
        {
            _reportHeader = header;
        }

        public void CreateAndSaveReport(List<string> contentLines, string savePath)
        {
            var report = new StringBuilder();
            report.AppendLine(_reportHeader);
            foreach (var contentLine in contentLines)
            {
                report.AppendLine(contentLine);
            }

            File.WriteAllText(savePath, report.ToString());
        }
    }
}
