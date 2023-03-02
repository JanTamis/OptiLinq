## ElementAtOnArray

### Source
[ElementAtOnArray.cs](../../src/OptiLinq.Benchmark/ElementAtOnArray.cs)

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
|       OptiLinq |  0.3828 ns | 0.0161 ns | 0.0134 ns | 37.15x faster |   1.38x |         - |          NA |
|           Linq | 14.2064 ns | 0.3002 ns | 0.2507 ns |      baseline |         |         - |          NA |
| EnumerableLinq | 14.4165 ns | 0.1797 ns | 0.1593 ns |  1.02x slower |   0.02x |         - |          NA |
