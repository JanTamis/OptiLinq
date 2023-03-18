## Union

### Source
[Union.cs](../../src/OptiLinq.Benchmark/Union.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|              Method |      Mean |    Error |   StdDev |        Ratio | RatioSD |    Gen0 |    Gen1 |    Gen2 | Allocated |      Alloc Ratio |
|-------------------- |----------:|---------:|---------:|-------------:|--------:|--------:|--------:|--------:|----------:|-----------------:|
| OptiLinqComparerSum |  87.15 μs | 0.834 μs | 0.739 μs | 3.89x faster |   0.03x |       - |       - |       - |      96 B |  5,611.667x less |
|    OptiLinqComparer |  98.10 μs | 1.947 μs | 3.844 μs | 3.43x faster |   0.15x |       - |       - |       - |      48 B | 11,223.333x less |
|         OptiLinqSum | 116.86 μs | 1.045 μs | 0.873 μs | 2.90x faster |   0.02x |       - |       - |       - |      96 B |  5,611.667x less |
|            OptiLinq | 132.57 μs | 1.701 μs | 1.591 μs | 2.56x faster |   0.03x |       - |       - |       - |      96 B |  5,611.667x less |
|                Linq | 339.27 μs | 1.992 μs | 1.766 μs |     baseline |         | 95.2148 | 95.2148 | 95.2148 |  538720 B |                  |
