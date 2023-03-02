## Union

### Source
[Union.cs](../../src/StructLinq.Benchmark/Union.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|              Method |      Mean |    Error |   StdDev |        Ratio | RatioSD |    Gen0 |    Gen1 |    Gen2 | Allocated |     Alloc Ratio |
|-------------------- |----------:|---------:|---------:|-------------:|--------:|--------:|--------:|--------:|----------:|----------------:|
| OptiLinqComparerSum |  74.93 μs | 1.190 μs | 1.055 μs | 4.64x faster |   0.08x |       - |       - |       - |     144 B | 3,741.111x less |
|    OptiLinqComparer | 109.81 μs | 1.156 μs | 1.025 μs | 3.16x faster |   0.04x |       - |       - |       - |      96 B | 5,611.667x less |
|         OptiLinqSum | 115.08 μs | 2.078 μs | 1.944 μs | 3.02x faster |   0.06x |       - |       - |       - |     144 B | 3,741.111x less |
|            OptiLinq | 169.84 μs | 2.986 μs | 2.647 μs | 2.04x faster |   0.03x |       - |       - |       - |     128 B | 4,208.750x less |
|                Linq | 347.52 μs | 4.035 μs | 3.369 μs |     baseline |         | 95.2148 | 95.2148 | 95.2148 |  538720 B |                 |
