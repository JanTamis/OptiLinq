## ExceptOnBigStruct

### Source
[ExceptOnBigStruct.cs](../../src/OptiLinq.Benchmark/ExceptOnBigStruct.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |     Mean |    Error |   StdDev |        Ratio | RatioSD |     Gen0 |     Gen1 |     Gen2 | Allocated | Alloc Ratio |
|--------------------- |---------:|---------:|---------:|-------------:|--------:|---------:|---------:|---------:|----------:|------------:|
| OptiLinqWithComparer | 242.0 μs |  4.06 μs |  3.80 μs | 2.80x faster |   0.04x | 159.9121 | 152.8320 | 151.3672 |  800.6 KB |  1.13x less |
|             OptiLinq | 423.1 μs |  6.77 μs |  6.00 μs | 1.60x faster |   0.03x | 181.6406 | 178.7109 | 166.0156 | 862.98 KB |  1.05x less |
|     LinqWithComparer | 579.9 μs | 10.35 μs | 10.17 μs | 1.17x faster |   0.02x | 249.0234 | 249.0234 | 249.0234 | 843.45 KB |  1.07x less |
|                 Linq | 676.4 μs |  7.15 μs |  6.34 μs |     baseline |         | 249.0234 | 249.0234 | 249.0234 | 906.41 KB |             |
