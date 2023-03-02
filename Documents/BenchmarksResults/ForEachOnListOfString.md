## ForEachOnListOfString

### Source
[ForEachOnListOfString.cs](../../src/OptiLinq.Benchmark/ForEachOnListOfString.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |     Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |---------:|---------:|---------:|-------------:|--------:|----------:|------------:|
|  Default | 10.01 μs | 0.119 μs | 0.105 μs |     baseline |         |         - |          NA |
| OptiLinq | 13.08 μs | 0.060 μs | 0.053 μs | 1.31x slower |   0.01x |         - |          NA |
