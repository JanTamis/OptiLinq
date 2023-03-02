## ForEachOnListWithSelect

### Source
[ForEachOnListWithSelect.cs](../../src/StructLinq.Benchmark/ForEachOnListWithSelect.cs)

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
|             OptiLinqWithStructFunc | 28.30 μs | 0.225 μs | 0.199 μs | 2.71x faster |   0.05x |         - |          NA |
|                   OptiLinqWithFunc | 32.57 μs | 0.408 μs | 0.382 μs | 2.36x faster |   0.04x |         - |          NA |
| OptiLinqWithStructFuncAsEnumerable | 76.14 μs | 0.991 μs | 0.927 μs | 1.01x faster |   0.01x |      80 B |  1.11x more |
|                               LINQ | 76.75 μs | 1.160 μs | 1.028 μs |     baseline |         |      72 B |             |
|       OptiLinqWithFuncAsEnumerable | 89.71 μs | 1.060 μs | 0.991 μs | 1.17x slower |   0.02x |      80 B |  1.11x more |
