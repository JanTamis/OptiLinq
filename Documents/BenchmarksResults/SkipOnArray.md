## SkipOnArray

### Source
[SkipOnArray.cs](../../src/OptiLinq.Benchmark/SkipOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
| OptiLinq | 11.96 μs | 0.085 μs | 0.075 μs | 5.56x faster |   0.09x |         - |          NA |
|     Linq | 66.44 μs | 0.964 μs | 0.902 μs |     baseline |         |      48 B |             |
