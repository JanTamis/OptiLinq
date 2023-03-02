## SelectOnArray

### Source
[SelectOnArray.cs](../../src/StructLinq.Benchmark/SelectOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
|            Handmaded | 13.24 μs | 0.632 μs | 1.862 μs |     baseline |         |         - |          NA |
| OptiLINQWithFunction | 20.00 μs | 0.037 μs | 0.031 μs | 1.32x slower |   0.09x |         - |          NA |
|             OptiLINQ | 28.90 μs | 0.543 μs | 0.534 μs | 1.93x slower |   0.13x |         - |          NA |
|                 LINQ | 70.90 μs | 0.239 μs | 0.187 μs | 4.67x slower |   0.33x |      48 B |          NA |
