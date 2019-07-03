``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Xeon CPU E5-1650 v3 3.50GHz, 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host]     : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  DefaultJob : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT


```
|                                           Method |         Mean |       Error |      StdDev |  Gen 0 |  Gen 1 | Allocated |
|------------------------------------------------- |-------------:|------------:|------------:|-------:|-------:|----------:|
|                        SingleMatchInFirst_Create |     47.54 ns |   0.4280 ns |   0.4003 ns |      - |      - |         - |
|                       SingleMatchInSecond_Create |     47.86 ns |   0.1560 ns |   0.1460 ns |      - |      - |         - |
|                   SingleMatchInEnumerable_Create |    146.85 ns |   0.3129 ns |   0.2927 ns | 0.0076 |      - |      48 B |
|                    MultipleMatchesInSlots_Create |     47.05 ns |   0.0409 ns |   0.0382 ns |      - |      - |         - |
| MultipleMatchesInSlotsIncludingEnumerable_Create |    151.89 ns |   0.7040 ns |   0.6241 ns | 0.0062 |      - |      40 B |
|                       AppendedSingleMatch_Create |    146.49 ns |   0.1546 ns |   0.1291 ns | 0.0138 |      - |      88 B |
|           AppendedSingleMatchAsEnumerable_Create |    240.57 ns |   0.3498 ns |   0.3272 ns | 0.0138 |      - |      88 B |
|                            Append4Matches_Create |    423.56 ns |   0.5032 ns |   0.3929 ns | 0.0558 |      - |     352 B |
|                          Append100Matches_Create |  9,370.05 ns |  28.6475 ns |  25.3953 ns | 1.3885 | 0.0153 |    8800 B |
|                 AggregatorAndAppendedList_Create |    236.87 ns |   1.1203 ns |   0.9931 ns | 0.0458 |      - |     288 B |
|                SingleMatchInFirst_FirstOrDefault |     56.83 ns |   0.0916 ns |   0.0812 ns |      - |      - |         - |
|               SingleMatchInSecond_FirstOrDefault |     74.34 ns |   0.2731 ns |   0.2554 ns |      - |      - |         - |
|           SingleMatchInEnumerable_FirstOrDefault |    234.13 ns |   0.3268 ns |   0.2551 ns | 0.0267 |      - |     168 B |
|                     MultipleMatchesInSlots_Count |    129.02 ns |   0.1828 ns |   0.1710 ns |      - |      - |         - |
|         MultipleMatchesIncludingEnumerable_Count |    620.02 ns |   1.9984 ns |   1.7716 ns | 0.0248 |      - |     160 B |
|                              AppendedMatch_Count |    196.06 ns |   0.2849 ns |   0.2665 ns | 0.0191 |      - |     120 B |
|                  AppendedMatchAsEnumerable_Count |    185.81 ns |   1.7596 ns |   1.6459 ns | 0.0191 |      - |     120 B |
|                             Append4Matches_Count |    519.05 ns |   0.6575 ns |   0.6151 ns | 0.0191 |      - |     120 B |
|                           Append100Matches_Count | 16,129.11 ns | 170.4177 ns | 159.4089 ns |      - |      - |     120 B |
|                  AggregatorAndAppendedList_Count |  1,210.76 ns |   1.3906 ns |   1.2327 ns | 0.0591 |      - |     376 B |
