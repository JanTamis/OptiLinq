## Zip

### Source
[Zip.cs](../../src/OptiLinq.Benchmark/Zip.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|              Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
|    OptiLinqFunction | 50.36 μs | 0.426 μs | 0.356 μs | 1.26x faster |   0.01x |      32 B |  4.50x less |
| OptiLinqFunctionSum | 51.81 μs | 0.491 μs | 0.525 μs | 1.23x faster |   0.01x |      72 B |  2.00x less |
|         OptiLinqSum | 55.71 μs | 0.751 μs | 0.702 μs | 1.14x faster |   0.02x |      72 B |  2.00x less |
|            OptiLinq | 55.76 μs | 0.758 μs | 0.709 μs | 1.14x faster |   0.02x |      32 B |  4.50x less |
|                Linq | 63.55 μs | 0.839 μs | 0.700 μs |     baseline |         |     144 B |             |
|        LinqFunction | 66.92 μs | 0.853 μs | 0.798 μs | 1.05x slower |   0.02x |     160 B |  1.11x more |
|     LinqFunctionSum | 71.36 μs | 0.823 μs | 0.687 μs | 1.12x slower |   0.01x |     160 B |  1.11x more |
|             LinqSum | 71.61 μs | 1.029 μs | 1.056 μs | 1.13x slower |   0.03x |     144 B |  1.00x more |
