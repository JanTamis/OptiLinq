## ToArrayOnArraySelectOfClass

### Source
[ToArrayOnArraySelectOfClass.cs](../../src/StructLinq.Benchmark/ToArrayOnArraySelectOfClass.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |      Mean |    Error |   StdDev |        Ratio | RatioSD |   Gen0 |   Gen1 | Allocated | Alloc Ratio |
|--------------------- |----------:|---------:|---------:|-------------:|--------:|-------:|-------:|----------:|------------:|
|                 Linq |  30.90 μs | 1.134 μs | 3.289 μs |     baseline |         | 4.7607 | 0.5798 |  39.13 KB |             |
| OptiLinqWithFunction |  73.53 μs | 0.133 μs | 0.111 μs | 2.57x slower |   0.09x | 4.7607 | 0.4883 |  39.09 KB |  1.00x less |
|             OptiLinq | 127.29 μs | 3.071 μs | 8.811 μs | 4.17x slower |   0.58x | 4.6387 | 0.4883 |  39.09 KB |  1.00x less |
