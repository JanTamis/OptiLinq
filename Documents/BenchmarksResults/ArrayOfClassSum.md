## ArrayOfClassSum

### Source
[ArrayOfClassSum.cs](../../src/OptiLinq.Benchmark/ArrayOfClassSum.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                   Method |        Mean |     Error |    StdDev |         Ratio | RatioSD | Allocated | Alloc Ratio |
|------------------------- |------------:|----------:|----------:|--------------:|--------:|----------:|------------:|
|                Handmaded |    457.5 ns |   4.17 ns |   3.48 ns | 14.80x faster |   0.24x |         - |          NA |
|             IFunctionSum |  3,446.7 ns |  66.30 ns |  62.02 ns |  1.96x faster |   0.04x |      24 B |  2.00x less |
| DelegateWithoutSelectSum |  4,865.4 ns |  54.13 ns |  42.26 ns |  1.39x faster |   0.02x |      24 B |  2.00x less |
|                  LINQSum |  6,768.8 ns | 104.85 ns |  92.95 ns |      baseline |         |      48 B |             |
|              DelegateSum | 10,580.3 ns | 148.20 ns | 152.19 ns |  1.56x slower |   0.03x |      32 B |  1.50x less |
