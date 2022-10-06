// * Summary *

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.1081/21H1/May2021Update)
Intel Core i7-7820HQ CPU 2.90GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.301
  [Host]     : .NET 6.0.6 (6.0.622.26707), X64 RyuJIT AVX2  [AttachedDebugger]
  DefaultJob : .NET 6.0.6 (6.0.622.26707), X64 RyuJIT AVX2


|        Method | ListSize |         Mean |        Error |       StdDev |     Gen0 |     Gen1 |    Gen2 |  Allocated |
|-------------- |--------- |-------------:|-------------:|-------------:|---------:|---------:|--------:|-----------:|
|      For_Loop |    10000 | 726,529.1 us | 14,252.55 us | 23,417.35 us |        - |        - |       - | 1819.18 KB |
|  ForEach_Loop |    10000 | 711,355.9 us |  9,933.40 us |  9,291.71 us |        - |        - |       - |  1822.2 KB |
| Select_Lookup |    10000 | 695,874.8 us | 13,763.05 us | 27,802.05 us |        - |        - |       - | 1641.16 KB |
|          Join |    10000 |   2,768.5 us |     54.15 us |     68.49 us | 292.9688 | 160.1563 | 78.1250 | 1762.76 KB |
|    Query_Join |    10000 |   2,806.1 us |     51.85 us |     45.96 us | 292.9688 | 156.2500 | 78.1250 | 1762.73 KB |
|  Dict_Created |    10000 |     710.8 us |     13.71 us |     30.38 us | 114.2578 |  76.1719 | 76.1719 |  745.35 KB |
|    Dict_Exist |    10000 |     309.7 us |      5.72 us |      5.07 us |  76.1719 |  38.0859 |       - |  468.91 KB |
|        Manual |    10000 |     665.9 us |     11.59 us |     10.84 us | 114.2578 |  76.1719 | 76.1719 |  745.21 KB |
