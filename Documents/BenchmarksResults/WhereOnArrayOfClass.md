## WhereOnArrayOfClass

### Source
[WhereOnArrayOfClass.cs](../../src/StructLinq.Benchmark/WhereOnArrayOfClass.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                  Method |       Mean |     Error |    StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------ |-----------:|----------:|----------:|--------------:|--------:|----------:|------------:|
|               Handmaded |   8.078 μs | 0.1608 μs | 0.1720 μs |      baseline |         |         - |          NA |
|                    LINQ |  53.407 μs | 0.1529 μs | 0.1355 μs |  6.60x slower |   0.15x |      48 B |          NA |
|                 LinqSum |  61.485 μs | 0.2831 μs | 0.2510 μs |  7.59x slower |   0.18x |      48 B |          NA |
|    OptiLinqWithFunction |  90.221 μs | 1.7408 μs | 2.3828 μs | 11.22x slower |   0.34x |         - |          NA |
| OptiLinqWithFunctionSum | 125.524 μs | 0.2989 μs | 0.2649 μs | 15.50x slower |   0.36x |      32 B |          NA |
|                OptiLinq | 137.143 μs | 0.3330 μs | 0.2780 μs | 16.91x slower |   0.39x |         - |          NA |
|             OptiLinqSum | 167.445 μs | 1.9707 μs | 1.7470 μs | 20.68x slower |   0.40x |      32 B |          NA |
