## LastOnArray

### Source
[LastOnArray.cs](../../src/StructLinq.Benchmark/LastOnArray.cs)

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
|       OptiLinq |  1.450 ns | 0.0117 ns | 0.0097 ns | 14.02x faster |   0.30x |         - |          NA |
| EnumerableLinq | 20.089 ns | 0.1695 ns | 0.1415 ns |  1.01x faster |   0.02x |         - |          NA |
|           Linq | 20.314 ns | 0.3984 ns | 0.3531 ns |      baseline |         |         - |          NA |
