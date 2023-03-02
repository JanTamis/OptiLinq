## ToArrayOnArrayWhere

### Source
[ToArrayOnArrayWhere.cs](../../src/OptiLinq.Benchmark/ToArrayOnArrayWhere.cs)

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
| OptiLinqWithFunction | 15.96 μs | 0.274 μs | 0.243 μs | 1.73x faster |   0.03x | 6.3477 | 0.5188 |  52.14 KB |  1.00x less |
|                 Linq | 27.63 μs | 0.307 μs | 0.272 μs |     baseline |         | 6.3477 | 0.4578 |  52.19 KB |             |
|             OptiLinq | 27.94 μs | 0.388 μs | 0.344 μs | 1.01x slower |   0.02x | 6.3477 | 0.5188 |  52.14 KB |  1.00x less |
