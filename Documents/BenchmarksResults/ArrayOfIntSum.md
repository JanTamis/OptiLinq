## ArrayOfIntSum

### Source
[ArrayOfIntSum.cs](../../src/OptiLinq.Benchmark/ArrayOfIntSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|         Method |      Mean |    Error |   StdDev |        Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------- |----------:|---------:|---------:|-------------:|--------:|-------:|----------:|------------:|
|       OptiLinq |  65.60 ns | 0.832 ns | 0.650 ns | 5.54x faster |   0.09x | 0.0029 |      24 B |          NA |
| EnumerableLINQ | 361.53 ns | 2.125 ns | 1.775 ns | 1.01x faster |   0.01x |      - |         - |          NA |
|      Handmaded | 362.96 ns | 5.436 ns | 5.085 ns | 1.00x faster |   0.01x |      - |         - |          NA |
|      ArrayLINQ | 363.26 ns | 3.497 ns | 3.100 ns |     baseline |         |      - |         - |          NA |
