## Min

### Source
[Min.cs](../../src/OptiLinq.Benchmark/Min.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|     Method |           Mean |       Error |      StdDev |         Median |         Ratio | RatioSD | Allocated | Alloc Ratio |
|----------- |---------------:|------------:|------------:|---------------:|--------------:|--------:|----------:|------------:|
| StructLINQ |      0.0167 ns |   0.0253 ns |   0.0291 ns |      0.0000 ns |            NA |      NA |         - |          NA |
|  Handmaded |  8,632.7079 ns | 165.4604 ns | 203.2002 ns |  8,550.9044 ns |      baseline |         |         - |          NA |
| ConvertMin | 35,221.3509 ns | 305.9545 ns | 286.1901 ns | 35,196.4142 ns | 4.076x slower |   0.10x |      40 B |          NA |
|       LINQ | 37,520.9693 ns | 404.2061 ns | 337.5304 ns | 37,516.2522 ns | 4.338x slower |   0.14x |      40 B |          NA |
