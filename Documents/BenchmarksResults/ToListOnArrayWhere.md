## ToListOnArrayWhere

### Source
[ToListOnArrayWhere.cs](../../src/StructLinq.Benchmark/ToListOnArrayWhere.cs)

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
| OptiLinqWithFunction | 15.05 μs | 0.091 μs | 0.081 μs | 2.58x faster |   0.02x | 7.8430 | 0.9766 |   64.3 KB |  1.00x less |
|             OptiLinq | 32.90 μs | 0.370 μs | 0.328 μs | 1.18x faster |   0.02x | 7.8125 | 0.9766 |   64.3 KB |  1.00x less |
|                 Linq | 38.75 μs | 0.225 μs | 0.200 μs |     baseline |         | 7.8125 | 1.0376 |  64.34 KB |             |
