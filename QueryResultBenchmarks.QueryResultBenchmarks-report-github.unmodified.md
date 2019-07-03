``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Xeon CPU E5-1650 v3 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host]     : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  DefaultJob : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT


```
|                                           Method |         Mean |      Error |     StdDev |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|------------------------------------------------- |-------------:|-----------:|-----------:|-------:|-------:|------:|----------:|
|                        SingleMatchInFirst_Create |     47.84 ns |  0.1241 ns |  0.1161 ns |      - |      - |     - |         - |
|                       SingleMatchInSecond_Create |     47.67 ns |  0.1724 ns |  0.1613 ns |      - |      - |     - |         - |
|                   SingleMatchInEnumerable_Create |    134.70 ns |  0.1896 ns |  0.1773 ns | 0.0076 |      - |     - |      48 B |
|                    MultipleMatchesInSlots_Create |     46.65 ns |  0.0810 ns |  0.0758 ns |      - |      - |     - |         - |
| MultipleMatchesInSlotsIncludingEnumerable_Create |    148.62 ns |  0.1878 ns |  0.1568 ns | 0.0062 |      - |     - |      40 B |
|                       AppendedSingleMatch_Create |    121.50 ns |  0.0866 ns |  0.0768 ns | 0.0100 |      - |     - |      64 B |
|           AppendedSingleMatchAsEnumerable_Create |    236.73 ns |  0.2779 ns |  0.2600 ns | 0.0191 |      - |     - |     120 B |
|                            Append4Matches_Create |    440.30 ns |  1.1652 ns |  1.0900 ns | 0.0701 |      - |     - |     440 B |
|                          Append100Matches_Create | 10,132.48 ns | 20.2221 ns | 18.9158 ns | 2.0142 | 0.0458 |     - |   12728 B |
|                 AggregatorAndAppendedList_Create |    211.38 ns |  0.1473 ns |  0.1378 ns | 0.0420 |      - |     - |     264 B |
|                SingleMatchInFirst_FirstOrDefault |     58.39 ns |  0.4585 ns |  0.4288 ns |      - |      - |     - |         - |
|               SingleMatchInSecond_FirstOrDefault |     74.55 ns |  0.1296 ns |  0.1212 ns |      - |      - |     - |         - |
|           SingleMatchInEnumerable_FirstOrDefault |    312.17 ns |  0.9958 ns |  0.8827 ns | 0.0429 |      - |     - |     272 B |
|                     MultipleMatchesInSlots_Count |    121.50 ns |  0.0793 ns |  0.0703 ns |      - |      - |     - |         - |
|         MultipleMatchesIncludingEnumerable_Count |    828.91 ns |  1.5079 ns |  1.4105 ns | 0.0925 |      - |     - |     584 B |
|                              AppendedMatch_Count |    450.59 ns |  0.6205 ns |  0.5804 ns | 0.0505 |      - |     - |     320 B |
|                  AppendedMatchAsEnumerable_Count |    522.34 ns |  1.1214 ns |  0.8755 ns | 0.0591 |      - |     - |     376 B |
|                             Append4Matches_Count |  1,580.43 ns |  1.9050 ns |  1.7819 ns | 0.1831 |      - |     - |    1152 B |
|                           Append100Matches_Count | 45,800.73 ns | 43.0258 ns | 38.1413 ns | 4.0894 |      - |     - |   25728 B |
|                  AggregatorAndAppendedList_Count |  1,923.29 ns |  2.1212 ns |  1.7713 ns | 0.1564 |      - |     - |    1000 B |
