## UnionOnBigStruct

### Source
[UnionOnBigStruct.cs](../../src/StructLinq.Benchmark/UnionOnBigStruct.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |     Mean |    Error |   StdDev |   Median |        Ratio | RatioSD |     Gen0 |     Gen1 |     Gen2 | Allocated |     Alloc Ratio |
|--------------------- |---------:|---------:|---------:|---------:|-------------:|--------:|---------:|---------:|---------:|----------:|----------------:|
| OptiLinqWithComparer | 186.1 μs | 12.05 μs | 35.34 μs | 175.4 μs | 6.42x faster |   0.91x |        - |        - |        - |     256 B | 6,560.258x less |
|             OptiLinq | 374.1 μs |  7.47 μs | 16.55 μs | 367.6 μs | 2.59x faster |   0.18x |   7.3242 |        - |        - |   64450 B |    26.058x less |
|                 Linq | 989.9 μs | 19.32 μs | 18.97 μs | 982.2 μs |     baseline |         | 398.4375 | 398.4375 | 398.4375 | 1679426 B |                 |
