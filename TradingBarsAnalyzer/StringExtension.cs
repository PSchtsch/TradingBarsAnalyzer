using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBarsAnalyzer
{
    public static class StringExtension
    {
        public static bool IsReportHeader(this string barAsString)
        {
            var header = @"""Symbol"",""Description"",""Date"",""Time"",""Open"",""High"",""Low"",""Close"",""TotalVolume""";
            return barAsString == header;
        }
    }
}
