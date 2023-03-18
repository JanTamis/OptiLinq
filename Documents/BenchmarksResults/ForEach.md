## ForEach

### Source
[ForEach.cs](../../src/OptiLinq.Benchmark/ForEach.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                      Method |      Mean |    Error |    StdDev |    Median |        Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |----------:|---------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|                  WithStruct |  48.09 μs | 0.538 μs |  0.477 μs |  48.16 μs | 7.28x faster |   0.41x |      88 B |  2.20x more |
|                  WithAction | 170.89 μs | 1.891 μs |  1.768 μs | 169.99 μs | 2.04x faster |   0.11x |      88 B |  2.20x more |
|                  ClrForEach | 334.31 μs | 6.650 μs | 18.428 μs | 325.99 μs |     baseline |         |      40 B |             |
| ToTypedEnumerableWithStruct | 376.36 μs | 4.856 μs |  4.305 μs | 376.78 μs | 1.08x slower |   0.06x |      40 B |  1.00x more |
