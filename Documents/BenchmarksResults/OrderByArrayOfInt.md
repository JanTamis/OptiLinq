## OrderByArrayOfInt

### Source
[OrderByArrayOfInt.cs](../../src/OptiLinq.Benchmark/OrderByArrayOfInt.cs)

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
|      OptiLinqOrderSum |     4.969 μs |  0.0410 μs |  0.0343 μs | 259.386x faster |   3.14x |       - |      - |      40 B | 3,007.850x less |
|         OptiLinqOrder |   459.023 μs |  3.3816 μs |  2.6401 μs |   2.810x faster |   0.03x |  4.3945 | 0.4883 |   40409 B |     2.977x less |
| OptiLinqOrderComparer |   759.909 μs |  5.1008 μs |  4.7713 μs |   1.695x faster |   0.02x |  3.9063 |      - |   40529 B |     2.969x less |
|                  LINQ | 1,288.318 μs | 12.4429 μs | 11.6391 μs |        baseline |         | 13.6719 | 1.9531 |  120314 B |                 |
