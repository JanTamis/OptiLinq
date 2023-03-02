## Zip

### Source
[Zip.cs](../../src/OptiLinq.Benchmark/Zip.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|              Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
| OptiLinqFunctionSum | 43.35 μs | 0.666 μs | 0.623 μs | 1.46x faster |   0.03x |      72 B |  2.00x less |
|    OptiLinqFunction | 44.62 μs | 0.784 μs | 0.733 μs | 1.42x faster |   0.04x |      32 B |  4.50x less |
|            OptiLinq | 49.63 μs | 0.525 μs | 0.466 μs | 1.28x faster |   0.02x |      32 B |  4.50x less |
|         OptiLinqSum | 49.74 μs | 0.903 μs | 0.801 μs | 1.28x faster |   0.03x |      72 B |  2.00x less |
|                Linq | 63.26 μs | 1.239 μs | 1.272 μs |     baseline |         |     144 B |             |
|        LinqFunction | 67.62 μs | 0.805 μs | 0.753 μs | 1.07x slower |   0.02x |     160 B |  1.11x more |
|             LinqSum | 67.79 μs | 0.853 μs | 0.756 μs | 1.07x slower |   0.02x |     144 B |  1.00x more |
|     LinqFunctionSum | 70.95 μs | 1.370 μs | 1.281 μs | 1.12x slower |   0.04x |     160 B |  1.11x more |
