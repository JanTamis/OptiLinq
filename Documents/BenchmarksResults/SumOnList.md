## SumOnList

### Source
[SumOnList.cs](../../src/OptiLinq.Benchmark/SumOnList.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|   Method |       Mean |    Error |   StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------- |-----------:|---------:|---------:|-------------:|--------:|----------:|------------:|
| OptiLinq |   392.9 ns |  7.77 ns |  9.25 ns | 9.89x faster |   0.27x |         - |          NA |
|     Linq | 3,874.9 ns | 53.86 ns | 44.98 ns |     baseline |         |         - |          NA |
