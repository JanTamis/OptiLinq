## FirstOnArray

### Source
[FirstOnArray.cs](../../src/StructLinq.Benchmark/FirstOnArray.cs)

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
|       OptiLinq |  1.578 ns | 0.0676 ns | 0.1012 ns | 14.21x faster |   1.12x |         - |          NA |
|           Linq | 22.261 ns | 0.1269 ns | 0.1060 ns |      baseline |         |         - |          NA |
| EnumerableLinq | 22.809 ns | 0.1508 ns | 0.1260 ns |  1.02x slower |   0.01x |         - |          NA |
