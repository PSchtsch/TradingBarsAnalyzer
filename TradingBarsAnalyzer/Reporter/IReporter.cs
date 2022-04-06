using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBarsAnalyzer
{
    public interface IReporter
    {
        void CreateAndSaveReport(List<TradingBar> tradingBars, string savePath);
    }
}
