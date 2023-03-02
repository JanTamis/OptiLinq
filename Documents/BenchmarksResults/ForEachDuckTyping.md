## ForEachDuckTyping

### Source
[ForEachDuckTyping.cs](../../src/StructLinq.Benchmark/ForEachDuckTyping.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                                      Method |      Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|-------------------------------------------- |----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|                              ForEachOnArray |  3.458 μs | 0.0053 μs | 0.0047 μs | 1.41x faster |   0.00x |         - |          NA |
|                                  ForOnArray |  4.871 μs | 0.0058 μs | 0.0049 μs |     baseline |         |         - |          NA |
|              ForEachOnArrayStructEnumerable |  5.601 μs | 0.0565 μs | 0.0528 μs | 1.15x slower |   0.01x |         - |          NA |
|                        ForEachOnIEnumerable | 44.940 μs | 0.3955 μs | 0.3700 μs | 9.22x slower |   0.08x |      32 B |          NA |
|                    ForEachOnYieldEnumerable | 45.266 μs | 0.3817 μs | 0.3384 μs | 9.30x slower |   0.07x |      56 B |          NA |
| ForEachOnArrayStructEnumerableAsIEnumerable | 45.408 μs | 0.5109 μs | 0.4779 μs | 9.32x slower |   0.11x |      32 B |          NA |
