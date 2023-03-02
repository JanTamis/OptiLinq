## ToArrayOnArraySelect

### Source
[ToArrayOnArraySelect.cs](../../src/OptiLinq.Benchmark/ToArrayOnArraySelect.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |      Mean |     Error |    StdDev |        Ratio | RatioSD |   Gen0 |   Gen1 | Allocated | Alloc Ratio |
|--------------------- |----------:|----------:|----------:|-------------:|--------:|-------:|-------:|----------:|------------:|
| OptiLinqWithFunction |  7.585 μs | 0.0945 μs | 0.0789 μs | 2.68x faster |   0.04x | 4.7607 | 0.5951 |  39.09 KB |  1.00x less |
|                 Linq | 20.295 μs | 0.1938 μs | 0.1513 μs |     baseline |         | 4.7607 | 0.5798 |  39.13 KB |             |
|             OptiLinq | 21.783 μs | 0.2957 μs | 0.2766 μs | 1.07x slower |   0.01x | 4.7607 | 0.5798 |  39.09 KB |  1.00x less |
