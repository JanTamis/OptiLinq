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
|               Method |      Mean |    Error |   StdDev |        Ratio | RatioSD |    Gen0 |    Gen1 |    Gen2 | Allocated |      Alloc Ratio |
|--------------------- |----------:|---------:|---------:|-------------:|--------:|--------:|--------:|--------:|----------:|-----------------:|
| OptiLinqWithComparer |  58.26 μs | 0.369 μs | 0.327 μs | 7.43x faster |   0.06x |       - |       - |       - |      32 B | 10,774.625x less |
|             OptiLinq | 114.78 μs | 1.021 μs | 0.955 μs | 3.77x faster |   0.03x |       - |       - |       - |      32 B | 10,774.625x less |
|                 Linq | 432.76 μs | 2.340 μs | 2.075 μs |     baseline |         | 76.6602 | 76.6602 | 76.6602 |  344788 B |                  |
