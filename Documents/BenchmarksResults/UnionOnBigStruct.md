## UnionOnBigStruct

### Source
[UnionOnBigStruct.cs](../../src/OptiLinq.Benchmark/UnionOnBigStruct.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |     Mean |   Error |  StdDev |        Ratio | RatioSD |     Gen0 |     Gen1 |     Gen2 | Allocated |      Alloc Ratio |
|--------------------- |---------:|--------:|--------:|-------------:|--------:|---------:|---------:|---------:|----------:|-----------------:|
| OptiLinqWithComparer | 129.6 μs | 3.28 μs | 8.98 μs | 6.41x faster |   0.50x |        - |        - |        - |     160 B | 10,496.406x less |
|             OptiLinq | 294.8 μs | 2.58 μs | 2.41 μs | 2.58x faster |   0.03x |   7.3242 |        - |        - |   64450 B |     26.058x less |
|                 Linq | 760.9 μs | 9.22 μs | 8.18 μs |     baseline |         | 399.4141 | 399.4141 | 399.4141 | 1679425 B |                  |
