## ForEachOnList

### Source
[ForEachOnList.cs](../../src/OptiLinq.Benchmark/ForEachOnList.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |     Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |---------:|----------:|----------:|-------------:|--------:|----------:|------------:|
| OptiLinq | 7.652 μs | 0.0368 μs | 0.0288 μs | 1.30x faster |   0.01x |         - |          NA |
|  Default | 9.990 μs | 0.1004 μs | 0.0890 μs |     baseline |         |         - |          NA |
