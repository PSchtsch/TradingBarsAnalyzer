﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBarsAnalyzer
{
    public static class TradingBarExtension
    {
        public static List<List<TradingBar>> GroupByTimeSpan(this List<TradingBar> tradingBars, TimeSpanOptions timeSpan)
        {
            var groups = new List<List<TradingBar>>();
            var currentGroup = new List<TradingBar>();

            var firstBar = tradingBars[0];
            currentGroup.Add(firstBar);

            var tradingBarsCount = tradingBars.Count;
            for (int i = 1; i < tradingBarsCount; i++)
            {
                var currentBar = tradingBars[i];
                var previousBar = tradingBars[i - 1];

                if (currentBar.InSameTimeSpan(previousBar, timeSpan))
                {
                    currentGroup.Add(currentBar);
                }
                else
                {
                    groups.Add(currentGroup);
                    currentGroup = new List<TradingBar>();
                    currentGroup.Add(currentBar);
                }

                if (i == tradingBarsCount - 1)
                {
                    groups.Add(currentGroup);
                }
            }

            return groups;
        }

        private static bool InSameTimeSpan(this TradingBar source, TradingBar target, TimeSpanOptions timeSpan)
        {
            switch (timeSpan)
            {
                case TimeSpanOptions.Hour:
                    return source.Time.Hour == target.Time.Hour;
                case TimeSpanOptions.Day:
                    return source.Date == target.Date;
                default: throw new Exception($"Unknown time span {timeSpan}");
            }
        }

        public static (TradingBar MinimumPrice, TradingBar MaximumPrice) GetMinMax(this List<TradingBar> tradingBars)
        {
            TradingBar tempMinBar = tradingBars[0];
            TradingBar tempMaxBar = tradingBars[0];

            var tradingBarsCount = tradingBars.Count;
            for (int i = 1; i < tradingBarsCount; i++)
            {
                var currentBar = tradingBars[i];

                if (currentBar.Low < tempMinBar.Low)
                {
                    tempMinBar = currentBar;
                }

                if (currentBar.High > tempMaxBar.High)
                {
                    tempMaxBar = currentBar;
                }
            }

            return (tempMinBar, tempMaxBar);
        }
    }
}