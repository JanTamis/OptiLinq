## ForEachOnArrayWhereSelect

### Source
[ForEachOnArrayWhereSelect.cs](../../src/StructLinq.Benchmark/ForEachOnArrayWhereSelect.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|                           Method |     Mean |    Error |   StdDev |   Median | Allocated |
|--------------------------------- |---------:|---------:|---------:|---------:|----------:|
|                         OptiLinq | 18.86 μs | 0.029 μs | 0.024 μs | 18.86 μs |         - |
|             OptiLinqAsEnumerable | 38.53 μs | 0.062 μs | 0.055 μs | 38.52 μs |      80 B |
|             OptiLinqWithDelegate | 49.65 μs | 0.871 μs | 2.184 μs | 48.65 μs |         - |
|                          SysLinq | 55.85 μs | 0.401 μs | 0.335 μs | 55.69 μs |     104 B |
| OptiLinqWithDelegateAsEnumerable | 59.15 μs | 0.157 μs | 0.131 μs | 59.15 μs |      96 B |
