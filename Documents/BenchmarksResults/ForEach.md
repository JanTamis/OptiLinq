## ForEach

### Source
[ForEach.cs](../../src/OptiLinq.Benchmark/ForEach.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                      Method |      Mean |    Error |   StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|---------------------------- |----------:|---------:|---------:|--------------:|--------:|----------:|------------:|
|                  WithStruct |  24.89 μs | 0.159 μs | 0.141 μs | 13.22x faster |   0.25x |         - |          NA |
|                  ClrForEach | 329.16 μs | 5.490 μs | 4.867 μs |      baseline |         |      40 B |             |
| ToTypedEnumerableWithStruct | 383.32 μs | 3.104 μs | 2.592 μs |  1.17x slower |   0.01x |      40 B |  1.00x more |
|                  WithAction | 800.38 μs | 8.773 μs | 7.326 μs |  2.43x slower |   0.05x |      49 B |  1.23x more |
