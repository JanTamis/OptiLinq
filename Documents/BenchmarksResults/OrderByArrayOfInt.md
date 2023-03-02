## OrderByArrayOfInt

### Source
[OrderByArrayOfInt.cs](../../src/StructLinq.Benchmark/OrderByArrayOfInt.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                Method |         Mean |      Error |     StdDev |           Ratio | RatioSD |    Gen0 |   Gen1 | Allocated |     Alloc Ratio |
|---------------------- |-------------:|-----------:|-----------:|----------------:|--------:|--------:|-------:|----------:|----------------:|
|      OptiLinqOrderSum |     6.032 μs |  0.0918 μs |  0.0859 μs | 255.323x faster |   3.83x |       - |      - |      40 B | 3,007.850x less |
|         OptiLinqOrder |   554.591 μs |  0.9602 μs |  0.8512 μs |   2.783x faster |   0.01x |  3.9063 |      - |   40377 B |     2.980x less |
| OptiLinqOrderComparer |   946.786 μs | 12.7841 μs | 11.9582 μs |   1.631x faster |   0.02x |  3.9063 |      - |   40466 B |     2.973x less |
|                  LINQ | 1,543.446 μs |  4.1496 μs |  3.2397 μs |        baseline |         | 13.6719 | 1.9531 |  120314 B |                 |
