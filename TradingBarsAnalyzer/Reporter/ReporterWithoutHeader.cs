using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TradingBarsAnalyzer.Reporter
{
    public class ReporterWithoutHeader : IReporter
    {
        public ReporterWithoutHeader()
        {
        }

        public void CreateAndSaveReport(List<string> contentLines, string savePath)
        {
            var report = new StringBuilder();
            foreach (var contentLine in contentLines)
            {
                report.AppendLine(contentLine);
            }

            File.WriteAllText(savePath, report.ToString());
        }
    }
}
