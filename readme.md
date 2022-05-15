
# This is Trading Bars Analyzer

## A test lib that could be used for retrive some statistic from financial quotation data

At present the lib can create TradingBars from text file with sorted
financial quotation data, group them by time, convert range of bars into
one, find mimimum and maximum price on range. And all of that without LINQ.

With obtained data report as txt-file can be created. There are files
comparer as well which helps to find lost, suddenly added and unique
strings. Except duplicates.

Usage examples could be run as test in ReporterTests project.