## Where

### Source
[Where.cs](../../src/StructLinq.Benchmark/Where.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|         Method |       Mean |     Error |    StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------- |-----------:|----------:|----------:|--------------:|--------:|----------:|------------:|
|   StructSelect |   9.993 μs | 0.1941 μs | 0.2455 μs | 10.63x faster |   0.30x |      32 B |  3.00x less |
| DelegateSelect |  22.577 μs | 0.0621 μs | 0.0519 μs |  4.72x faster |   0.03x |      32 B |  3.00x less |
|      SysSelect | 106.556 μs | 0.5472 μs | 0.4569 μs |      baseline |         |      96 B |             |
