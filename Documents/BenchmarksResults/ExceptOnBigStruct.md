## ExceptOnBigStruct

### Source
[ExceptOnBigStruct.cs](../../src/StructLinq.Benchmark/ExceptOnBigStruct.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |     Mean |   Error |  StdDev |        Ratio | RatioSD |     Gen0 |     Gen1 |     Gen2 | Allocated | Alloc Ratio |
|--------------------- |---------:|--------:|--------:|-------------:|--------:|---------:|---------:|---------:|----------:|------------:|
| OptiLinqWithComparer | 266.4 μs | 3.24 μs | 2.88 μs | 2.90x faster |   0.03x | 155.7617 | 147.9492 | 147.4609 | 801.25 KB |  1.13x less |
|             OptiLinq | 456.1 μs | 5.23 μs | 4.36 μs | 1.69x faster |   0.02x | 172.3633 | 166.0156 | 156.7383 | 849.99 KB |  1.07x less |
|     LinqWithComparer | 635.5 μs | 1.78 μs | 1.58 μs | 1.21x faster |   0.00x | 249.0234 | 249.0234 | 249.0234 | 843.45 KB |  1.07x less |
|                 Linq | 771.5 μs | 2.12 μs | 1.88 μs |     baseline |         | 249.0234 | 249.0234 | 249.0234 | 906.41 KB |             |
