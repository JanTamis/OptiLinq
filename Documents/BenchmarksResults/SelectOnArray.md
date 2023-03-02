## SelectOnArray

### Source
[SelectOnArray.cs](../../src/OptiLinq.Benchmark/SelectOnArray.cs)

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
|            Handmaded |  9.532 μs | 0.1346 μs | 0.1259 μs |     baseline |         |         - |          NA |
| OptiLINQWithFunction | 16.098 μs | 0.2566 μs | 0.2401 μs | 1.69x slower |   0.03x |         - |          NA |
|             OptiLINQ | 22.116 μs | 0.4300 μs | 0.5438 μs | 2.34x slower |   0.04x |         - |          NA |
|                 LINQ | 59.200 μs | 0.3324 μs | 0.2946 μs | 6.21x slower |   0.08x |      48 B |          NA |
