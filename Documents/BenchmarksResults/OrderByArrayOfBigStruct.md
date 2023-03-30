## OrderByArrayOfBigStruct

### Source
[OrderByArrayOfBigStruct.cs](../../src/OptiLinq.Benchmark/OrderByArrayOfBigStruct.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|            Method |       Mean |    Error |   StdDev |        Ratio | RatioSD |     Gen0 |     Gen1 |     Gen2 | Allocated |   Alloc Ratio |
|------------------ |-----------:|---------:|---------:|-------------:|--------:|---------:|---------:|---------:|----------:|--------------:|
|     OptiLinqOrder |   931.7 μs | 18.45 μs | 28.72 μs | 2.65x faster |   0.12x |        - |        - |        - |     577 B | 780.861x less |
| OptiLinqOrderFunc | 2,034.8 μs | 40.36 μs | 80.60 μs | 1.22x faster |   0.07x |        - |        - |        - |    2309 B | 195.131x less |
|              LINQ | 2,476.5 μs | 49.07 μs | 98.00 μs |     baseline |         | 109.3750 | 109.3750 | 109.3750 |  450557 B |               |
