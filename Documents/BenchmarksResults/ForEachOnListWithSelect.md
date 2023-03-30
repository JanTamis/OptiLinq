## ForEachOnListWithSelect

### Source
[ForEachOnListWithSelect.cs](../../src/OptiLinq.Benchmark/ForEachOnListWithSelect.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                 Method |     Mean |    Error |   StdDev |   Median |        Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------------- |---------:|---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
| OptiLinqWithStructFunc | 19.77 μs | 0.391 μs | 0.824 μs | 19.54 μs | 3.67x faster |   0.14x |         - |          NA |
|       OptiLinqWithFunc | 30.02 μs | 0.595 μs | 1.161 μs | 29.56 μs | 2.31x faster |   0.11x |         - |          NA |
|                   LINQ | 70.57 μs | 0.418 μs | 0.391 μs | 70.68 μs |     baseline |         |      72 B |             |
