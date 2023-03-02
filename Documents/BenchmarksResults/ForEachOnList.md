## ForEachOnList

### Source
[ForEachOnList.cs](../../src/OptiLinq.Benchmark/ForEachOnList.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |      Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|  Default |  9.849 μs | 0.1047 μs | 0.0874 μs |     baseline |         |         - |          NA |
| OptiLinq | 15.487 μs | 0.3076 μs | 0.4697 μs | 1.60x slower |   0.05x |         - |          NA |
