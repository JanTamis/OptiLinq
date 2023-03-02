## FirstOnArray

### Source
[FirstOnArray.cs](../../src/OptiLinq.Benchmark/FirstOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|         Method |      Mean |     Error |    StdDev |    Median |         Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------- |----------:|----------:|----------:|----------:|--------------:|--------:|----------:|------------:|
|       OptiLinq |  1.298 ns | 0.0240 ns | 0.0201 ns |  1.297 ns | 13.40x faster |   0.27x |         - |          NA |
|           Linq | 17.389 ns | 0.2008 ns | 0.1878 ns | 17.347 ns |      baseline |         |         - |          NA |
| EnumerableLinq | 18.215 ns | 0.4436 ns | 1.2941 ns | 17.510 ns |  1.06x slower |   0.08x |         - |          NA |
