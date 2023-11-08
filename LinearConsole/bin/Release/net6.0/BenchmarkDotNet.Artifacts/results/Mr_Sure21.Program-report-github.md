```

BenchmarkDotNet v0.13.10, Windows 10 (10.0.19045.3570/22H2/2022Update)
Intel Core i7-8750H CPU 2.20GHz (Coffee Lake), 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.100-rc.1.23463.5
  [Host]     : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.24 (6.0.2423.51814), X64 RyuJIT AVX2


```
| Method | Mean     | Error    | StdDev   | Gen0    | Gen1   | Allocated |
|------- |---------:|---------:|---------:|--------:|-------:|----------:|
| Test   | 86.21 μs | 0.962 μs | 0.853 μs | 15.7471 | 0.4883 |   72.6 KB |
