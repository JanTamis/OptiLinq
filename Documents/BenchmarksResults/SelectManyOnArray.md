## SelectManyOnArray

### Source
[SelectManyOnArray.cs](../../src/OptiLinq.Benchmark/SelectManyOnArray.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |     Mean |     Error |    StdDev |        Ratio | RatioSD | Allocated | Alloc Ratio |
|--------------------- |---------:|----------:|----------:|-------------:|--------:|----------:|------------:|
|                 LINQ | 4.079 ms | 0.0379 ms | 0.0336 ms |     baseline |         |  31.32 KB |             |
| OptiLINQWithDelegate | 4.082 ms | 0.0695 ms | 0.0616 ms | 1.00x slower |   0.02x |  31.26 KB |  1.00x less |
| OptiLINQWithFunction | 4.105 ms | 0.0578 ms | 0.0512 ms | 1.01x slower |   0.02x |  31.26 KB |  1.00x less |
