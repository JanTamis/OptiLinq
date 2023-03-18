## RangeToArray

### Source
[RangeToArray.cs](../../src/OptiLinq.Benchmark/RangeToArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |     Mean |     Error |    StdDev |        Ratio | RatioSD |   Gen0 |   Gen1 | Allocated | Alloc Ratio |
|--------- |---------:|----------:|----------:|-------------:|--------:|-------:|-------:|----------:|------------:|
| OptiLinq | 1.794 μs | 0.0219 μs | 0.0205 μs | 2.98x faster |   0.08x | 4.7607 | 0.5951 |  39.17 KB |  1.00x more |
|     Linq | 5.333 μs | 0.1011 μs | 0.1082 μs |     baseline |         | 4.7607 | 0.5951 |  39.13 KB |             |
