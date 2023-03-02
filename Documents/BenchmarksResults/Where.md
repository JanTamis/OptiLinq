## Where

### Source
[Where.cs](../../src/OptiLinq.Benchmark/Where.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|         Method |      Mean |     Error |    StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------- |----------:|----------:|----------:|--------------:|--------:|----------:|------------:|
|   StructSelect |  8.897 μs | 0.1703 μs | 0.1422 μs | 10.61x faster |   0.20x |      32 B |  3.00x less |
| DelegateSelect | 20.736 μs | 0.3920 μs | 0.4514 μs |  4.58x faster |   0.13x |      32 B |  3.00x less |
|      SysSelect | 94.373 μs | 1.4175 μs | 1.1837 μs |      baseline |         |      96 B |             |
