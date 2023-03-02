## ToArrayOnArraySelect

### Source
[ToArrayOnArraySelect.cs](../../src/StructLinq.Benchmark/ToArrayOnArraySelect.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |      Mean |     Error |    StdDev |    Median |        Ratio | RatioSD |   Gen0 |   Gen1 | Allocated | Alloc Ratio |
|--------------------- |----------:|----------:|----------:|----------:|-------------:|--------:|-------:|-------:|----------:|------------:|
| OptiLinqWithFunction |  9.337 μs | 0.1388 μs | 0.2280 μs |  9.226 μs | 2.64x faster |   0.07x | 4.7607 | 0.5951 |  39.09 KB |  1.00x less |
|                 Linq | 24.785 μs | 0.1201 μs | 0.1003 μs | 24.754 μs |     baseline |         | 4.7607 | 0.5798 |  39.13 KB |             |
|             OptiLinq | 26.284 μs | 0.1624 μs | 0.1356 μs | 26.215 μs | 1.06x slower |   0.01x | 4.7607 | 0.5798 |  39.09 KB |  1.00x less |
