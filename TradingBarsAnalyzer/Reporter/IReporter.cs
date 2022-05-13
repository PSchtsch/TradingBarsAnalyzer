namespace TradingBarsAnalyzer
{
    public interface IReporter
    {
        void CreateAndSaveReport(List<string> contentLines, string savePath);
    }
}
