## Except

### Source
[Except.cs](../../src/OptiLinq.Benchmark/Except.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E252) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|      Method |      Mean |    Error |   StdDev |        Ratio | RatioSD |    Gen0 |    Gen1 |    Gen2 | Allocated |      Alloc Ratio |
|------------ |----------:|---------:|---------:|-------------:|--------:|--------:|--------:|--------:|----------:|-----------------:|
| OptiLinqSum |  37.63 μs | 0.491 μs | 0.435 μs | 5.46x faster |   0.09x |       - |       - |       - |      56 B |  5,144.268x less |
|    OptiLinq |  37.87 μs | 0.543 μs | 0.481 μs | 5.42x faster |   0.11x |       - |       - |       - |       8 B | 36,009.875x less |
|        Linq | 205.76 μs | 3.288 μs | 3.075 μs |     baseline |         | 45.4102 | 45.4102 | 45.4102 |  288079 B |                  |
|     LinqSum | 206.35 μs | 2.946 μs | 2.611 μs | 1.01x slower |   0.02x | 45.4102 | 45.4102 | 45.4102 |  288079 B |      1.000x more |
