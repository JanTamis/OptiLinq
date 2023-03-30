## WhereOnArray

### Source
[WhereOnArray.cs](../../src/OptiLinq.Benchmark/WhereOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                  Method |          Mean |       Error |      StdDev |           Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------ |--------------:|------------:|------------:|----------------:|--------:|----------:|------------:|
|                OptiLinq |      8.826 ns |   0.0449 ns |   0.0350 ns | 788.214x faster |   7.10x |         - |          NA |
|    OptiLinqWithFunction |     12.577 ns |   0.1169 ns |   0.1037 ns | 552.643x faster |   6.24x |         - |          NA |
|               Handmaded |  6,950.169 ns |  65.6242 ns |  58.1742 ns |        baseline |         |         - |          NA |
| OptiLinqWithFunctionSum |  6,998.719 ns |  64.0783 ns |  56.8037 ns |   1.007x slower |   0.01x |      32 B |          NA |
|             OptiLinqSum | 26,514.321 ns | 411.3152 ns | 709.4985 ns |   3.886x slower |   0.14x |      32 B |          NA |
|                    LINQ | 41,007.478 ns | 355.1806 ns | 314.8583 ns |   5.901x slower |   0.08x |      48 B |          NA |
|                 LinqSum | 44,149.700 ns | 406.9409 ns | 339.8141 ns |   6.350x slower |   0.07x |      48 B |          NA |
