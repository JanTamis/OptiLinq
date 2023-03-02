## Max

### Source
[Max.cs](../../src/OptiLinq.Benchmark/Max.cs)

### Results:
``` ini

BenchmarkDotNet=v0.13.4, OS=macOS 13.2 (22D5038i) [Darwin 22.3.0]
Intel Core i9-9880H CPU 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=8.0.100-preview.1.23115.2
  [Host]     : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.2 (7.0.222.60605), X64 RyuJIT AVX2


```
|     Method |           Mean |       Error |      StdDev |          Ratio | RatioSD | Allocated | Alloc Ratio |
|----------- |---------------:|------------:|------------:|---------------:|--------:|----------:|------------:|
| StructLINQ |      0.0110 ns |   0.0073 ns |   0.0057 ns |             NA |      NA |         - |          NA |
|  Handmaded |  2,360.6866 ns |  23.9731 ns |  21.2515 ns |       baseline |         |         - |          NA |
|       LINQ | 35,103.7936 ns | 252.0012 ns | 223.3925 ns | 14.872x slower |   0.20x |      40 B |          NA |
| ConvertMin | 35,244.5282 ns | 472.3098 ns | 418.6903 ns | 14.931x slower |   0.26x |      40 B |          NA |
