## ForEachOnArrayWhereSelect

### Source
[ForEachOnArrayWhereSelect.cs](../../src/OptiLinq.Benchmark/ForEachOnArrayWhereSelect.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.3 (22E5236f) [Darwin 22.4.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|               Method |         Mean |      Error |     StdDev | Allocated |
|--------------------- |-------------:|-----------:|-----------:|----------:|
| OptiLinqWithDelegate |     11.76 ns |   0.266 ns |   0.356 ns |         - |
|             OptiLinq |     20.65 ns |   0.431 ns |   0.442 ns |         - |
|              SysLinq | 58,901.28 ns | 613.487 ns | 543.840 ns |     104 B |
