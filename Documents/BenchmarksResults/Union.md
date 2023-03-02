## Union

### Source
[Union.cs](../../src/OptiLinq.Benchmark/Union.cs)

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
| OptiLinqComparerSum |  67.48 μs | 1.324 μs | 1.301 μs | 4.13x faster |   0.08x |       - |       - |       - |     144 B | 3,741.111x less |
|    OptiLinqComparer |  95.41 μs | 1.377 μs | 1.150 μs | 2.92x faster |   0.04x |       - |       - |       - |      96 B | 5,611.667x less |
|         OptiLinqSum |  99.05 μs | 0.721 μs | 0.639 μs | 2.82x faster |   0.03x |       - |       - |       - |     144 B | 3,741.111x less |
|            OptiLinq | 143.61 μs | 1.597 μs | 1.416 μs | 1.94x faster |   0.03x |       - |       - |       - |     128 B | 4,208.750x less |
|                Linq | 278.97 μs | 2.997 μs | 2.340 μs |     baseline |         | 95.2148 | 95.2148 | 95.2148 |  538720 B |                 |
