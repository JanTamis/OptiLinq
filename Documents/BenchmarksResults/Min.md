## Min

### Source
[Min.cs](../../src/StructLinq.Benchmark/Min.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|     Method |           Mean |       Error |        StdDev |         Median |         Ratio | RatioSD | Allocated | Alloc Ratio |
|----------- |---------------:|------------:|--------------:|---------------:|--------------:|--------:|----------:|------------:|
| StructLINQ |      0.0373 ns |   0.0264 ns |     0.0770 ns |      0.0000 ns |            NA |      NA |         - |          NA |
|  Handmaded | 10,281.1507 ns | 133.1433 ns |   124.5423 ns | 10,231.5140 ns |      baseline |         |         - |          NA |
| ConvertMin | 45,115.3728 ns | 550.1866 ns |   429.5495 ns | 44,857.3790 ns | 4.377x slower |   0.07x |      40 B |          NA |
|       LINQ | 50,123.8932 ns | 995.2372 ns | 2,306.6136 ns | 49,790.1306 ns | 4.888x slower |   0.33x |      40 B |          NA |
