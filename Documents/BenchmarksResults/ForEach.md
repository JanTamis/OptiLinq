## ForEach

### Source
[ForEach.cs](../../src/StructLinq.Benchmark/ForEach.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                      Method |      Mean |     Error |    StdDev |    Median |         Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |----------:|----------:|----------:|----------:|--------------:|--------:|----------:|------------:|
|                  WithStruct |  27.94 μs |  0.244 μs |  0.228 μs |  28.05 μs | 18.11x faster |   1.61x |         - |          NA |
| ToTypedEnumerableWithStruct | 430.50 μs |  4.596 μs |  4.299 μs | 432.67 μs |  1.17x faster |   0.10x |      40 B |  1.00x more |
|                  ClrForEach | 440.90 μs | 19.443 μs | 56.099 μs | 412.22 μs |      baseline |         |      40 B |             |
|                  WithAction | 907.04 μs | 15.062 μs | 14.089 μs | 902.04 μs |  1.81x slower |   0.16x |      49 B |  1.23x more |
