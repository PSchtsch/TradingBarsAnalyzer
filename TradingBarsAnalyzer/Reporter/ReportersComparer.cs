using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TradingBarsAnalyzer.Reporter
{
    public class ReportersComparer
    {
        private readonly string _sourceFilePath;
        private readonly string _targetFilePath;
        private readonly string _linesSeparator;

        public ReportersComparer(string sourceFilePath, string targetFilePath, string linesSeparator = "\n")
        {
            _sourceFilePath = sourceFilePath;
            _targetFilePath = targetFilePath;
            _linesSeparator = linesSeparator;
        }

        public List<string> GetNewLines()
        {
            var sourceLines = TradingBar.ReadAndSplit(_sourceFilePath, _linesSeparator);
            var targetLines = TradingBar.ReadAndSplit(_targetFilePath, _linesSeparator);

            var sourceLinesHash = new HashSet<string>(sourceLines);
            var targetLinesHash = new HashSet<string>(targetLines);

            targetLinesHash.ExceptWith(sourceLinesHash);

            return targetLinesHash.ToList();
        }

        public List<string> GetLostLines()
        {
            var sourceLines = TradingBar.ReadAndSplit(_sourceFilePath, _linesSeparator);
            var targetLines = TradingBar.ReadAndSplit(_targetFilePath, _linesSeparator);

            var sourceLinesHash = new HashSet<string>(sourceLines);
            var targetLinesHash = new HashSet<string>(targetLines);

            sourceLinesHash.ExceptWith(targetLinesHash);

            return sourceLinesHash.ToList();
        }

        public List<string> GetUniqueLines()
        {
            var lostLinesHash = new HashSet<string>(GetLostLines());
            var newLinesHash = new HashSet<string>(GetNewLines());

            lostLinesHash.UnionWith(newLinesHash);

            return lostLinesHash.ToList();
        }
    }
}
