## ToArrayOnArrayWhereOfClass

### Source
[ToArrayOnArrayWhereOfClass.cs](../../src/OptiLinq.Benchmark/ToArrayOnArrayWhereOfClass.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |      Mean |    Error |   StdDev |        Ratio | RatioSD |    Gen0 |   Gen1 | Allocated | Alloc Ratio |
|--------------------- |----------:|---------:|---------:|-------------:|--------:|--------:|-------:|----------:|------------:|
|                 Linq |  51.56 μs | 0.581 μs | 0.515 μs |     baseline |         | 12.6343 | 2.0752 | 103.73 KB |             |
| OptiLinqWithFunction | 108.83 μs | 1.166 μs | 1.033 μs | 2.11x slower |   0.03x | 12.5732 | 1.7090 | 103.69 KB |  1.00x less |
|             OptiLinq | 149.22 μs | 1.852 μs | 1.642 μs | 2.89x slower |   0.04x | 12.4512 | 1.7090 | 103.69 KB |  1.00x less |
