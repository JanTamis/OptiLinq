## ToArrayOnArrayWhereOfClass

### Source
[ToArrayOnArrayWhereOfClass.cs](../../src/StructLinq.Benchmark/ToArrayOnArrayWhereOfClass.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |      Mean |    Error |   StdDev |    Median |        Ratio | RatioSD |    Gen0 |   Gen1 | Allocated | Alloc Ratio |
|--------------------- |----------:|---------:|---------:|----------:|-------------:|--------:|--------:|-------:|----------:|------------:|
|                 Linq |  72.67 μs | 0.900 μs | 0.702 μs |  72.42 μs |     baseline |         | 12.5732 | 2.0752 | 103.73 KB |             |
| OptiLinqWithFunction | 134.84 μs | 1.539 μs | 1.440 μs | 135.02 μs | 1.86x slower |   0.03x | 12.4512 | 1.7090 | 103.69 KB |  1.00x less |
|             OptiLinq | 176.99 μs | 3.425 μs | 4.453 μs | 174.39 μs | 2.47x slower |   0.06x | 12.4512 | 1.7090 | 103.69 KB |  1.00x less |
