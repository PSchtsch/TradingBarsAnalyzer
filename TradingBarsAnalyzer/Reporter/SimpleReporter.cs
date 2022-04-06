using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBarsAnalyzer.Reporter
{
    public class SimpleReporter : IReporter
    {
        private readonly string _reportHeader = "\"Symbol\",\"Description\",\"Date\",\"Time\",\"Open\",\"High\",\"Low\",\"Close\",\"TotalVolume\"";

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
    }
}
