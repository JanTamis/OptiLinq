## IntersectOnBigStruct

### Source
[IntersectOnBigStruct.cs](../../src/StructLinq.Benchmark/IntersectOnBigStruct.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                    Method |     Mean |   Error |  StdDev |        Ratio | RatioSD |     Gen0 |     Gen1 |     Gen2 | Allocated | Alloc Ratio |
|-------------------------- |---------:|--------:|--------:|-------------:|--------:|---------:|---------:|---------:|----------:|------------:|
| RefStructLinqWithComparer | 226.4 μs | 4.52 μs | 4.84 μs | 1.91x faster |   0.05x | 145.7520 | 137.6953 | 137.6953 | 769.15 KB |  2.28x more |
|                      Linq | 433.1 μs | 6.82 μs | 6.38 μs |     baseline |         |  76.6602 |  76.6602 |  76.6602 | 336.71 KB |             |
|                StructLinq | 442.5 μs | 6.39 μs | 5.67 μs | 1.02x slower |   0.02x | 165.5273 | 153.3203 | 149.9023 |  831.3 KB |  2.47x more |
