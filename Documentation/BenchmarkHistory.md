# Coverlet Benchmark History

This file is maintained automatically by `scripts/Update-BenchmarkHistory.ps1`.
Do not edit the table rows by hand – re-run the script after each benchmark run.

See [`test/coverlet.core.benchmark.tests/HowTo.md`](../test/coverlet.core.benchmark.tests/HowTo.md)
for instructions on how to run the benchmarks.

See [`test/coverlet.core.benchmark.tests/PerformanceImprovementProposal.md`](../test/coverlet.core.benchmark.tests/PerformanceImprovementProposal.md)
for the performance improvement roadmap.

---

## How to update this file

```powershell
# After building and running benchmarks:
pwsh scripts/Update-BenchmarkHistory.ps1 `
    -ArtifactsRoot "artifacts/bin/coverlet.core.benchmark.tests/release_net10.0" `
    -CoverletVersion "10.0.2"
```

---

## Results

> **Options column** shows only user-configured `[Params]` values (e.g. `SingleHit=True`).
> BDN infrastructure columns (Job, Toolchain, WarmupCount, Lock Contentions, Exceptions, etc.) are excluded by the script.
> Rows where BDN could not measure (out-of-process jobs, benchmark exceptions) are automatically skipped.

| Date | Version | Runtime | BenchmarkClass | Method | Options | Mean (ms) | Max (ms) | Allocated (MB) |
| ------ | --------- | --------- | ---------------- | -------- | --------- | ----------: | ---------: | ---------------: |
| 2025-01-01 | 6.0.0 | .NET 6.0.36, X64 RyuJIT AVX2 | CoverageBenchmarks | GetCoverageBenchmark | | 0.000 | 0.000 | 0.0001 |
| 2025-01-01 | 6.0.0 | .NET 6.0.36, X64 RyuJIT AVX2 | InstrumenterBenchmarks | InstrumenterBigClassBenchmark | | 4938.714 | 5706.474 | 2745.291 |
| 2025-01-01 | 6.0.1 | .NET 8.0.15, X64 RyuJIT AVX2 | CoverageBenchmarks | GetCoverageBenchmark | | 0.000 | 0.000 | 0.0001 |
| 2025-01-01 | 6.0.1 | .NET 8.0.15, X64 RyuJIT AVX2 | InstrumenterBenchmarks | InstrumenterBigClassBenchmark | | 3675.772 | 4550.028 | 2732.512 |
| 2025-01-01 | 6.0.2 | .NET 8.0.15, X64 RyuJIT AVX2 | CoverageBenchmarks | GetCoverageBenchmark | | 0.000 | 0.000 | 0.0001 |
| 2025-01-01 | 6.0.2 | .NET 8.0.15, X64 RyuJIT AVX2 | InstrumenterBenchmarks | InstrumenterBigClassBenchmark | | 19105.224 | 23555.328 | 3023.829 |
| 2025-01-01 | 6.0.3 | .NET 8.0.15, X64 RyuJIT AVX2 | CoverageBenchmarks | GetCoverageBenchmark | | 0.000 | 0.000 | 0.0001 |
| 2025-01-01 | 6.0.3 | .NET 8.0.15, X64 RyuJIT AVX2 | InstrumenterBenchmarks | InstrumenterBigClassBenchmark | | 3620.666 | 4201.277 | 2668.645 |
| 2025-01-01 | 6.0.4 | .NET 8.0.15, X64 RyuJIT AVX2 | CoverageBenchmarks | GetCoverageBenchmark | | 0.000 | 0.000 | 0.0001 |
| 2025-01-01 | 6.0.4 | .NET 8.0.15, X64 RyuJIT AVX2 | InstrumenterBenchmarks | InstrumenterBigClassBenchmark | | 3594.264 | 3787.390 | 2668.644 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | CoverageBenchmarks | GetCoverageBenchmark |  | 0.000 | 0.000 | 0.0001 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | CoverageWorkflowBenchmark | 'Simulate Workflow' |  | 276.311 | 277.185 | 30.1264 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumenterBenchmarks | InstrumenterBenchmark |  | 156.676 | 157.980 | 28.9014 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=False, SingleHit=False, SkipAutoProps=False | 155.396 | 156.260 | 29.0382 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=True, SingleHit=False, SkipAutoProps=False | 153.958 | 154.582 | 29.0380 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=False, SingleHit=False, SkipAutoProps=True | 154.074 | 154.722 | 28.9020 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=True, SingleHit=False, SkipAutoProps=True | 154.089 | 154.731 | 28.9020 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=False, SingleHit=True, SkipAutoProps=False | 154.029 | 154.667 | 29.0380 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=True, SingleHit=True, SkipAutoProps=False | 153.755 | 154.374 | 29.0380 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=False, SingleHit=True, SkipAutoProps=True | 154.346 | 154.939 | 28.9020 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=True, SingleHit=True, SkipAutoProps=True | 155.529 | 156.171 | 28.9020 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=cobertura | 0.003 | 0.003 | 0.0182 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=json | 0.002 | 0.002 | 0.0006 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=lcov | 0.000 | 0.000 | 0.0004 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=opencover | 0.004 | 0.004 | 0.0219 |
| 2026-05-26 | 10.0.2-p0 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=teamcity | 0.001 | 0.001 | 0.0030 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | CoverageBenchmarks | GetCoverageBenchmark |  | 0.000 | 0.000 | 0.0001 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | CoverageWorkflowBenchmark | 'Simulate Workflow' |  | 277.916 | 279.007 | 30.0778 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumenterBenchmarks | InstrumenterBenchmark |  | 155.798 | 156.547 | 28.8514 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=False, SingleHit=False, SkipAutoProps=False | 155.561 | 156.247 | 28.9797 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=True, SingleHit=False, SkipAutoProps=False | 155.395 | 156.087 | 28.9794 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=False, SingleHit=False, SkipAutoProps=True | 156.231 | 157.116 | 28.8520 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=True, SingleHit=False, SkipAutoProps=True | 156.000 | 156.979 | 28.8520 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=False, SingleHit=True, SkipAutoProps=False | 154.609 | 155.352 | 28.9794 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=True, SingleHit=True, SkipAutoProps=False | 155.989 | 156.779 | 28.9794 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=False, SingleHit=True, SkipAutoProps=True | 154.639 | 155.388 | 28.8520 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=True, SingleHit=True, SkipAutoProps=True | 155.231 | 156.016 | 28.8520 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=cobertura | 0.044 | 0.044 | 0.2680 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=json | 0.002 | 0.002 | 0.0006 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=lcov | 0.000 | 0.000 | 0.0003 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=opencover | 0.084 | 0.084 | 0.2716 |
| 2026-05-26 | 10.0.2-p2 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=teamcity | 0.001 | 0.001 | 0.0030 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | CoverageBenchmarks | GetCoverageBenchmark |  | 0.000 | 0.000 | 0.0002 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | CoverageWorkflowBenchmark | 'Simulate Workflow' |  | 280.344 | 282.062 | 30.0015 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumenterBenchmarks | InstrumenterBenchmark |  | 158.021 | 160.316 | 28.8310 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=False, SingleHit=False, SkipAutoProps=False | 152.373 | 153.191 | 28.9593 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=True, SingleHit=False, SkipAutoProps=False | 155.852 | 156.862 | 28.9590 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=False, SingleHit=False, SkipAutoProps=True | 152.891 | 153.720 | 28.8316 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=True, SingleHit=False, SkipAutoProps=True | 152.263 | 152.888 | 28.8316 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=False, SingleHit=True, SkipAutoProps=False | 152.648 | 153.369 | 28.9590 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=True, SingleHit=True, SkipAutoProps=False | 151.548 | 152.158 | 28.9590 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=False, SingleHit=True, SkipAutoProps=True | 152.008 | 152.801 | 28.8316 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | InstrumentationOptionsBenchmarks | 'Instrumentation - PrepareModules' | IncludeTestAssembly=True, SingleHit=True, SkipAutoProps=True | 152.397 | 153.153 | 28.8316 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=cobertura | 0.048 | 0.048 | 0.2682 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=json | 0.002 | 0.002 | 0.0006 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=lcov | 0.000 | 0.000 | 0.0004 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=opencover | 0.088 | 0.089 | 0.2716 |
| 2026-05-26 | 10.0.2-p3 | .NET 10.0.8 (10.0.8, 10.0.826.23019), X64 RyuJIT x86-64-v3 | ReportFormatBenchmarks | 'GetCoverageResult + Report' | ReportFormat=teamcity | 0.001 | 0.001 | 0.0030 |
