## SkipOnArrayWhere

### Source
[SkipOnArrayWhere.cs](../../src/StructLinq.Benchmark/SkipOnArrayWhere.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |      Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |----------:|---------:|---------:|-------------:|--------:|----------:|------------:|
| OptiLinq |  48.10 μs | 0.217 μs | 0.181 μs | 2.43x faster |   0.06x |         - |          NA |
|     Linq | 117.44 μs | 2.297 μs | 2.554 μs |     baseline |         |     104 B |             |
