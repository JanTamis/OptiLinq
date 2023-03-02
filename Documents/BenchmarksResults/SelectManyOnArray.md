## SelectManyOnArray

### Source
[SelectManyOnArray.cs](../../src/StructLinq.Benchmark/SelectManyOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |     Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |---------:|----------:|----------:|-------------:|--------:|----------:|------------:|
| OptiLINQWithDelegate | 4.712 ms | 0.0154 ms | 0.0129 ms | 1.06x faster |   0.01x |  31.26 KB |  1.00x less |
| OptiLINQWithFunction | 4.760 ms | 0.0907 ms | 0.0970 ms | 1.05x faster |   0.03x |  31.26 KB |  1.00x less |
|                 LINQ | 4.979 ms | 0.0686 ms | 0.0608 ms |     baseline |         |  31.32 KB |             |
