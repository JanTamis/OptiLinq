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
|               Method |     Mean |    Error |   StdDev |        Ratio | RatioSD |     Gen0 |     Gen1 |     Gen2 | Allocated |      Alloc Ratio |
|--------------------- |---------:|---------:|---------:|-------------:|--------:|---------:|---------:|---------:|----------:|-----------------:|
| OptiLinqWithComparer | 106.4 μs |  1.37 μs |  1.28 μs | 7.75x faster |   0.15x |        - |        - |        - |      32 B | 52,482.031x less |
|             OptiLinq | 287.3 μs |  3.95 μs |  3.70 μs | 2.87x faster |   0.03x |   7.3242 |        - |        - |   64034 B |     26.227x less |
|     LinqWithComparer | 684.6 μs | 11.37 μs | 10.63 μs | 1.20x faster |   0.02x | 399.4141 | 399.4141 | 399.4141 | 1614661 B |      1.040x less |
|                 Linq | 823.7 μs |  9.64 μs |  9.01 μs |     baseline |         | 399.4141 | 399.4141 | 399.4141 | 1679425 B |                  |
