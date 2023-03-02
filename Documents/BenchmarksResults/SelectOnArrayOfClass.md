## SelectOnArrayOfClass

### Source
[SelectOnArrayOfClass.cs](../../src/StructLinq.Benchmark/SelectOnArrayOfClass.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |      Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |----------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|            Handmaded |  7.061 μs | 0.0676 μs | 0.0632 μs |     baseline |         |         - |          NA |
| OptiLINQWithFunction | 20.480 μs | 0.0750 μs | 0.0586 μs | 2.90x slower |   0.03x |         - |          NA |
|             OptiLINQ | 24.930 μs | 0.0530 μs | 0.0443 μs | 3.53x slower |   0.03x |         - |          NA |
|                 LINQ | 62.654 μs | 0.1105 μs | 0.0980 μs | 8.87x slower |   0.08x |      48 B |          NA |
