## ForEachDuckTyping

### Source
[ForEachDuckTyping.cs](../../src/OptiLinq.Benchmark/ForEachDuckTyping.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                               Method |      Mean |     Error |    StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------------------- |----------:|----------:|----------:|--------------:|--------:|----------:|------------:|
|                       ForEachOnArray |  3.221 μs | 0.0226 μs | 0.0188 μs |  1.44x faster |   0.02x |         - |          NA |
|                           ForOnArray |  4.647 μs | 0.0624 μs | 0.0553 μs |      baseline |         |         - |          NA |
|              ForEachOnArrayOptiQuery |  5.320 μs | 0.0719 μs | 0.0562 μs |  1.15x slower |   0.02x |         - |          NA |
|                 ForEachOnIEnumerable | 42.714 μs | 0.2715 μs | 0.2267 μs |  9.19x slower |   0.13x |      32 B |          NA |
| ForEachOnArrayOptiQueryAsIEnumerable | 42.790 μs | 0.2539 μs | 0.2375 μs |  9.21x slower |   0.14x |      32 B |          NA |
|             ForEachOnYieldEnumerable | 47.630 μs | 0.3985 μs | 0.3533 μs | 10.25x slower |   0.14x |      56 B |          NA |
