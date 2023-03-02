## ArrayOfIntSum

### Source
[ArrayOfIntSum.cs](../../src/StructLinq.Benchmark/ArrayOfIntSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|         Method |      Mean |    Error |    StdDev | Ratio | RatioSD |   Gen0 | Allocated | Alloc Ratio |
|--------------- |----------:|---------:|----------:|------:|--------:|-------:|----------:|------------:|
|      Handmaded | 380.92 ns | 4.603 ns |  4.080 ns |  0.92 |    0.03 |      - |         - |          NA |
| EnumerableLINQ | 385.33 ns | 5.562 ns |  4.342 ns |  0.93 |    0.04 |      - |         - |          NA |
|      ArrayLINQ | 413.03 ns | 8.225 ns | 13.514 ns |  1.00 |    0.00 |      - |         - |          NA |
|       OptiLinq |  71.82 ns | 1.259 ns |  1.996 ns |  0.17 |    0.01 | 0.0029 |      24 B |          NA |
