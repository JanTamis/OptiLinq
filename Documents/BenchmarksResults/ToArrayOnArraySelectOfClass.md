## ToArrayOnArraySelectOfClass

### Source
[ToArrayOnArraySelectOfClass.cs](../../src/OptiLinq.Benchmark/ToArrayOnArraySelectOfClass.cs)

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
|                 Linq | 20.43 μs | 0.152 μs | 0.127 μs |     baseline |         | 4.7607 | 0.5798 |  39.13 KB |             |
| OptiLinqWithFunction | 59.45 μs | 0.507 μs | 0.423 μs | 2.91x slower |   0.02x | 4.7607 | 0.5493 |  39.09 KB |  1.00x less |
|             OptiLinq | 99.21 μs | 0.734 μs | 0.650 μs | 4.86x slower |   0.05x | 4.7607 | 0.4883 |  39.09 KB |  1.00x less |
