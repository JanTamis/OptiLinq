## Select

### Source
[Select.cs](../../src/OptiLinq.Benchmark/Select.cs)

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
| OptiLinqSelect | 21.36 μs | 0.271 μs | 0.227 μs | 2.69x faster |   0.05x |      32 B |  2.75x less |
| DelegateSelect | 21.39 μs | 0.238 μs | 0.211 μs | 2.68x faster |   0.03x |      32 B |  2.75x less |
|      SysSelect | 57.30 μs | 0.629 μs | 0.558 μs |     baseline |         |      88 B |             |
