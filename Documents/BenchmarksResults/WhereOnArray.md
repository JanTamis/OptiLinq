## WhereOnArray

### Source
[WhereOnArray.cs](../../src/StructLinq.Benchmark/WhereOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                  Method |      Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------ |----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|               Handmaded |  9.227 μs | 0.3826 μs | 1.1222 μs |     baseline |         |         - |          NA |
| OptiLinqWithFunctionSum | 12.829 μs | 0.0765 μs | 0.0639 μs | 1.53x slower |   0.13x |      32 B |          NA |
|             OptiLinqSum | 28.104 μs | 0.2534 μs | 0.2116 μs | 3.34x slower |   0.27x |      32 B |          NA |
|    OptiLINQWithFunction | 29.218 μs | 1.2614 μs | 3.7194 μs | 3.24x slower |   0.71x |         - |          NA |
|                OptiLINQ | 37.314 μs | 0.5621 μs | 0.4983 μs | 4.43x slower |   0.36x |         - |          NA |
|                 LinqSum | 49.555 μs | 0.4317 μs | 0.3827 μs | 5.89x slower |   0.46x |      48 B |          NA |
|                    LINQ | 52.287 μs | 1.9993 μs | 5.8321 μs | 5.81x slower |   1.27x |      48 B |          NA |
