## ForEachDuckTyping

### Source
[ForEachDuckTyping.cs](../../src/OptiLinq.Benchmark/ForEachDuckTyping.cs)

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
|                              ForEachOnArray |  3.044 μs | 0.0170 μs | 0.0142 μs | 1.43x faster |   0.01x |         - |          NA |
|                                  ForOnArray |  4.341 μs | 0.0312 μs | 0.0277 μs |     baseline |         |         - |          NA |
|              ForEachOnArrayStructEnumerable |  4.999 μs | 0.0543 μs | 0.0481 μs | 1.15x slower |   0.01x |         - |          NA |
|                    ForEachOnYieldEnumerable | 40.335 μs | 0.3106 μs | 0.2425 μs | 9.29x slower |   0.07x |      56 B |          NA |
| ForEachOnArrayStructEnumerableAsIEnumerable | 40.534 μs | 0.6276 μs | 0.5240 μs | 9.33x slower |   0.15x |      32 B |          NA |
|                        ForEachOnIEnumerable | 40.545 μs | 0.4628 μs | 0.4103 μs | 9.34x slower |   0.11x |      32 B |          NA |
