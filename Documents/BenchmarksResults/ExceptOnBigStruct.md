## ExceptOnBigStruct

### Source
[ExceptOnBigStruct.cs](../../src/OptiLinq.Benchmark/ExceptOnBigStruct.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |     Mean |   Error |  StdDev |        Ratio | RatioSD |     Gen0 |     Gen1 |     Gen2 | Allocated |     Alloc Ratio |
|--------------------- |---------:|--------:|--------:|-------------:|--------:|---------:|---------:|---------:|----------:|----------------:|
| OptiLinqWithComparer | 148.8 μs | 2.36 μs | 1.84 μs | 5.49x faster |   0.07x |        - |        - |        - |     288 B | 3,222.792x less |
|             OptiLinq | 452.9 μs | 8.94 μs | 8.36 μs | 1.80x faster |   0.04x |   7.3242 |        - |        - |   64578 B |    14.373x less |
|     LinqWithComparer | 657.3 μs | 6.08 μs | 5.39 μs | 1.24x faster |   0.01x | 249.0234 | 249.0234 | 249.0234 |  863696 B |     1.075x less |
|                 Linq | 815.4 μs | 7.36 μs | 6.53 μs |     baseline |         | 249.0234 | 249.0234 | 249.0234 |  928164 B |                 |
