## ForEachOnListWithSelect

### Source
[ForEachOnListWithSelect.cs](../../src/OptiLinq.Benchmark/ForEachOnListWithSelect.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                             Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|----------------------------------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
|             OptiLinqWithStructFunc | 17.38 μs | 0.148 μs | 0.124 μs | 3.97x faster |   0.06x |         - |          NA |
|                   OptiLinqWithFunc | 23.94 μs | 0.267 μs | 0.237 μs | 2.88x faster |   0.04x |         - |          NA |
| OptiLinqWithStructFuncAsEnumerable | 61.76 μs | 1.234 μs | 1.848 μs | 1.11x faster |   0.04x |      72 B |  1.00x more |
|                               LINQ | 68.96 μs | 0.660 μs | 0.585 μs |     baseline |         |      72 B |             |
|       OptiLinqWithFuncAsEnumerable | 75.61 μs | 0.511 μs | 0.399 μs | 1.10x slower |   0.01x |      72 B |  1.00x more |
