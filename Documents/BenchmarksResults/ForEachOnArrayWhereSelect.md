## ForEachOnArrayWhereSelect

### Source
[ForEachOnArrayWhereSelect.cs](../../src/OptiLinq.Benchmark/ForEachOnArrayWhereSelect.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                           Method |     Mean |    Error |   StdDev | Allocated |
|--------------------------------- |---------:|---------:|---------:|----------:|
|                         OptiLinq | 22.83 μs | 0.149 μs | 0.132 μs |         - |
|             OptiLinqAsEnumerable | 35.29 μs | 0.337 μs | 0.299 μs |      80 B |
|             OptiLinqWithDelegate | 45.54 μs | 0.646 μs | 0.572 μs |         - |
|                          SysLinq | 49.15 μs | 0.352 μs | 0.312 μs |     104 B |
| OptiLinqWithDelegateAsEnumerable | 59.71 μs | 0.544 μs | 0.781 μs |      96 B |
