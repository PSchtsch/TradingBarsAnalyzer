using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBarsAnalyzer.Reporter
{
    public class ReportersComparer
    {
        private readonly string _sourceFilePath;
        private readonly string _targetFilePath;

        public ReportersComparer(string sourceFilePath, string targetFilePath)
        {
            _sourceFilePath = sourceFilePath;
            _targetFilePath = targetFilePath;
        }

        public List<string> FindNewLines()
        {
            List<string> lines = new List<string>();
            return lines;
        }

        public List<string> FindLostLines()
        {
            List<string> lines = new List<string>();
            return lines;
        }

        public List<string> FindUniqLines()
        {
            List<string> lines = new List<string>();
            return lines;
        }
    }
}
