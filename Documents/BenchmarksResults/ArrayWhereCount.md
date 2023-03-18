## ArrayWhereCount

### Source
[ArrayWhereCount.cs](../../src/OptiLinq.Benchmark/ArrayWhereCount.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                     Method |      Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------------- |----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|          OptiLinqOptimized |  6.959 μs | 0.0335 μs | 0.0261 μs | 1.03x faster |   0.01x |         - |          NA |
|              HandmadedCode |  7.175 μs | 0.0747 μs | 0.0698 μs |     baseline |         |         - |          NA |
|             IFunctionWhere | 22.423 μs | 0.1871 μs | 0.1750 μs | 3.13x slower |   0.04x |         - |          NA |
|                    SysLinq | 29.314 μs | 0.2867 μs | 0.2394 μs | 4.08x slower |   0.06x |      48 B |          NA |
|       DelegateWithoutWhere | 29.413 μs | 0.2741 μs | 0.2430 μs | 4.10x slower |   0.06x |         - |          NA |
| OptiLinqIFunctionOptimized | 29.537 μs | 0.2823 μs | 0.2503 μs | 4.11x slower |   0.05x |      24 B |          NA |
|              DelegateWhere | 42.983 μs | 0.2954 μs | 0.2763 μs | 5.99x slower |   0.07x |         - |          NA |
|        SysLinqWithoutWhere | 65.072 μs | 0.4876 μs | 0.4072 μs | 9.06x slower |   0.10x |      32 B |          NA |
