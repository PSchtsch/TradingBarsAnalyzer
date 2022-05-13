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

        public async Task<List<string>> GetNewLinesAsync()
        {
            var sourceLines = TradingBar.ReadAndSplitAsync(_sourceFilePath, _linesSeparator);
            var targetLines = TradingBar.ReadAndSplitAsync(_targetFilePath, _linesSeparator);

            var sourceLinesHash = new HashSet<string>(await sourceLines);
            var targetLinesHash = new HashSet<string>(await targetLines);

            targetLinesHash.ExceptWith(sourceLinesHash);

            return targetLinesHash.ToList();
        }

        public async Task<List<string>> GetLostLinesAsync()
        {
            var sourceLines = TradingBar.ReadAndSplitAsync(_sourceFilePath, _linesSeparator);
            var targetLines = TradingBar.ReadAndSplitAsync(_targetFilePath, _linesSeparator);

            var sourceLinesHash = new HashSet<string>(await sourceLines);
            var targetLinesHash = new HashSet<string>(await targetLines);

            sourceLinesHash.ExceptWith(targetLinesHash);

            return sourceLinesHash.ToList();
        }

        public async Task<List<string>> GetUniqueLinesAsync()
        {
            var lostLines = GetLostLinesAsync();
            var newLines = GetNewLinesAsync();

            var lostLinesHash = new HashSet<string>(await lostLines);
            var newLinesHash = new HashSet<string>(await newLines);

            lostLinesHash.UnionWith(newLinesHash);

            return lostLinesHash.ToList();
        }
    }
}
