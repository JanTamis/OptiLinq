## Zip

### Source
[Zip.cs](../../src/StructLinq.Benchmark/Zip.cs)

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
|                Linq | 70.96 μs | 1.331 μs | 1.307 μs |     baseline |         |     144 B |             |
| OptiLinqFunctionSum | 74.60 μs | 0.492 μs | 0.411 μs | 1.05x slower |   0.02x |     104 B |  1.38x less |
|        LinqFunction | 75.17 μs | 0.477 μs | 0.423 μs | 1.06x slower |   0.02x |     160 B |  1.11x more |
|             LinqSum | 76.78 μs | 0.411 μs | 0.365 μs | 1.08x slower |   0.02x |     144 B |  1.00x more |
|    OptiLinqFunction | 79.94 μs | 0.898 μs | 0.796 μs | 1.13x slower |   0.02x |      64 B |  2.25x less |
|     LinqFunctionSum | 79.98 μs | 0.588 μs | 0.521 μs | 1.13x slower |   0.02x |     160 B |  1.11x more |
|         OptiLinqSum | 88.86 μs | 0.985 μs | 0.921 μs | 1.25x slower |   0.03x |     104 B |  1.38x less |
|            OptiLinq | 88.89 μs | 0.990 μs | 0.878 μs | 1.26x slower |   0.03x |      64 B |  2.25x less |
