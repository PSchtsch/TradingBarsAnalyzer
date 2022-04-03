using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBarsAnalyzer
{
    internal interface IReporter
    {
        void MakeAndSaveReport(List<TradingBar> tradingBars, string savePath);
    }
}
