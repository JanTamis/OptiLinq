## WhereOnArrayOfClass

### Source
[WhereOnArrayOfClass.cs](../../src/OptiLinq.Benchmark/WhereOnArrayOfClass.cs)

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
|               Handmaded |   6.912 μs | 0.0690 μs | 0.0645 μs |      baseline |         |         - |          NA |
|                    LINQ |  46.586 μs | 0.4736 μs | 0.4430 μs |  6.74x slower |   0.08x |      48 B |          NA |
|                 LinqSum |  53.736 μs | 0.4936 μs | 0.4375 μs |  7.78x slower |   0.10x |      48 B |          NA |
|    OptiLinqWithFunction |  78.945 μs | 0.8509 μs | 0.7959 μs | 11.42x slower |   0.20x |         - |          NA |
| OptiLinqWithFunctionSum | 110.914 μs | 0.9400 μs | 0.7849 μs | 16.05x slower |   0.17x |      32 B |          NA |
|                OptiLinq | 120.520 μs | 2.1833 μs | 2.0422 μs | 17.44x slower |   0.30x |         - |          NA |
|             OptiLinqSum | 151.322 μs | 2.7864 μs | 5.4346 μs | 22.18x slower |   1.15x |      32 B |          NA |
