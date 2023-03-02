## Select

### Source
[Select.cs](../../src/StructLinq.Benchmark/Select.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|         Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
| OptiLinqSelect | 25.96 μs | 0.344 μs | 0.322 μs | 3.09x faster |   0.15x |      32 B |  2.75x less |
| DelegateSelect | 26.23 μs | 0.501 μs | 0.988 μs | 2.89x faster |   0.17x |      32 B |  2.75x less |
|      SysSelect | 77.87 μs | 3.215 μs | 9.276 μs |     baseline |         |      88 B |             |
