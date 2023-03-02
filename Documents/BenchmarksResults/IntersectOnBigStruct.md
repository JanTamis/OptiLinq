## IntersectOnBigStruct

### Source
[IntersectOnBigStruct.cs](../../src/OptiLinq.Benchmark/IntersectOnBigStruct.cs)

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
| OptiLinqWithComparer | 218.6 μs | 1.78 μs | 1.67 μs | 1.76x faster |   0.02x | 151.6113 | 149.1699 | 141.6016 | 801.14 KB |  2.38x more |
|                 Linq | 384.0 μs | 3.75 μs | 3.32 μs |     baseline |         |  76.6602 |  76.6602 |  76.6602 | 336.71 KB |             |
|             OptiLinq | 424.0 μs | 4.95 μs | 4.39 μs | 1.10x slower |   0.01x | 193.8477 | 191.4063 | 176.2695 | 890.68 KB |  2.65x more |
