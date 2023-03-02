## ToListOnArrayWhere

### Source
[ToListOnArrayWhere.cs](../../src/OptiLinq.Benchmark/ToListOnArrayWhere.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |     Mean |    Error |   StdDev |        Ratio | RatioSD |   Gen0 |   Gen1 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|---------:|-------------:|--------:|-------:|-------:|----------:|------------:|
| OptiLinqWithFunction | 12.91 μs | 0.130 μs | 0.109 μs | 2.42x faster |   0.03x | 7.8430 | 0.9766 |   64.3 KB |  1.00x less |
|             OptiLinq | 25.78 μs | 0.307 μs | 0.287 μs | 1.21x faster |   0.02x | 7.8430 | 0.9766 |   64.3 KB |  1.00x less |
|                 Linq | 31.25 μs | 0.339 μs | 0.283 μs |     baseline |         | 7.8125 | 1.0376 |  64.34 KB |             |
