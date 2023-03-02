## LastOnArray

### Source
[LastOnArray.cs](../../src/OptiLinq.Benchmark/LastOnArray.cs)

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
|       OptiLinq |  1.300 ns | 0.0217 ns | 0.0181 ns | 13.43x faster |   0.22x |         - |          NA |
|           Linq | 17.445 ns | 0.1430 ns | 0.1194 ns |      baseline |         |         - |          NA |
| EnumerableLinq | 17.518 ns | 0.1648 ns | 0.1461 ns |  1.01x slower |   0.01x |         - |          NA |
