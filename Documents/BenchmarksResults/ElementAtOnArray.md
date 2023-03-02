## ElementAtOnArray

### Source
[ElementAtOnArray.cs](../../src/StructLinq.Benchmark/ElementAtOnArray.cs)

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
|       OptiLinq |  0.7304 ns | 0.0459 ns | 0.0430 ns | 30.40x faster |   1.84x |         - |          NA |
| EnumerableLinq | 21.6716 ns | 0.4656 ns | 0.5718 ns |  1.02x faster |   0.05x |         - |          NA |
|           Linq | 22.0466 ns | 0.4077 ns | 0.5846 ns |      baseline |         |         - |          NA |
