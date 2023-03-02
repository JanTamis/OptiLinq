## ToArrayComparison

### Source
[ToArrayComparison.cs](../../src/OptiLinq.Benchmark/ToArrayComparison.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|             Method |      Mean |     Error |    StdDev |    Gen0 |   Gen1 | Allocated |
|------------------- |----------:|----------:|----------:|--------:|-------:|----------:|
|    OptiLinqToArray |  1.533 μs | 0.0305 μs | 0.0327 μs |  4.7607 | 0.5951 |  39.09 KB |
| UseCountForToArray |  6.011 μs | 0.0733 μs | 0.0650 μs |  4.7607 | 0.5951 |  39.09 KB |
|  ToListThenToArray | 22.931 μs | 0.4506 μs | 0.3994 μs | 20.3857 | 2.5330 | 167.41 KB |
