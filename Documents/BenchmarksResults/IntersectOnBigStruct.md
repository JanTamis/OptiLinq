## IntersectOnBigStruct

### Source
[IntersectOnBigStruct.cs](../../src/OptiLinq.Benchmark/IntersectOnBigStruct.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |      Mean |    Error |    StdDev |        Ratio | RatioSD |     Gen0 |     Gen1 |    Gen2 | Allocated |     Alloc Ratio |
|--------------------- |----------:|---------:|----------:|-------------:|--------:|---------:|---------:|--------:|----------:|----------------:|
| OptiLinqWithComparer |  70.74 μs | 1.244 μs |  1.163 μs | 6.40x faster |   0.34x |        - |        - |       - |      48 B | 7,192.333x less |
|             OptiLinq | 209.63 μs | 3.298 μs |  3.085 μs | 2.16x faster |   0.10x |   7.5684 |   0.2441 |       - |   64097 B |     5.386x less |
|                 Linq | 416.85 μs | 8.145 μs | 21.170 μs |     baseline |         | 218.7500 | 218.7500 | 62.5000 |  345232 B |                 |
