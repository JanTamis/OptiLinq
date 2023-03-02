## ToListComparison

### Source
[ToListComparison.cs](../../src/OptiLinq.Benchmark/ToListComparison.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|    Method |      Mean |     Error |    StdDev |    Gen0 |   Gen1 | Allocated |
|---------- |----------:|----------:|----------:|--------:|-------:|----------:|
|    ToList |  1.288 μs | 0.0228 μs | 0.0202 μs |  4.7607 | 0.5951 |  39.12 KB |
| AddInList | 20.468 μs | 0.2618 μs | 0.2321 μs | 15.6250 | 3.1128 | 128.32 KB |
