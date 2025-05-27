// Copyright (c) Toni Solarin-Sodara
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// see details here: https://learn.microsoft.com/en-us/dotnet/core/testing/microsoft-testing-platform-architecture-extensions#the-itestsessionlifetimehandler-extensions
// Coverlet instrumentation should be done before any test is executed, and the coverage data should be collected after all tests have run.
// Coverlet collects code coverage data and does not need to be aware of the test framework being used. It also does not need test case details or test results.

using coverlet.MTP.Extension;
using Coverlet.Core;
using Coverlet.Core.Abstractions;
using Coverlet.Core.Enums;
using Coverlet.Core.Helpers;
using Coverlet.Core.Reporters;
using Coverlet.Core.Symbols;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Testing.Platform.Extensions.TestHost;
using Microsoft.Testing.Platform.TestHost;

namespace coverlet.MTP
{
  /// <summary>
  /// Implements test session lifetime handling for coverage collection using the Microsoft Testing Platform.
  /// </summary>
  internal sealed class CoverletCollectorExtension : IExtension, ITestSessionLifetimeHandler
  {
    private readonly ILogger _logger;
    private readonly CoverletMTPConfiguration _configuration;
    private readonly IServiceProvider _serviceProvider;
    private Coverage? _coverage;
    private string? _sessionId;

    /// <summary>
    /// Initializes a new instance of the CoverletCollectorExtension class.
    /// </summary>
    public CoverletCollectorExtension(ILogger logger)
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _configuration = new CoverletMTPConfiguration();
      _serviceProvider = CreateServiceProvider();
    }

    /// <inheritdoc/>
    public string Uid => "Coverlet.MTP";

    /// <inheritdoc/>
    public string Version => "1.0.0";

    /// <inheritdoc/>
    public string DisplayName => "Coverlet Code Coverage Collector";

    /// <inheritdoc/>
    public string Description => "Provides code coverage collection for the Microsoft Testing Platform";

    /// <inheritdoc/>
    public Task<bool> IsEnabledAsync() => Task.FromResult(true);

    /// <inheritdoc/>
    public async Task<bool> BeforeTestSessionStartAsync(TestSessionContext context)
    {
      try
      {
        _sessionId = context.SessionUid.ToString();
        _logger.LogInformation($"Initializing coverage collection for session {_sessionId}");

        var parameters = new CoverageParameters
        {
          IncludeFilters = _configuration.IncludePatterns,
          ExcludeFilters = _configuration.ExcludePatterns,
          IncludeTestAssembly = _configuration.IncludeTestAssembly,
          SingleHit = false,
          UseSourceLink = true,
          SkipAutoProps = true,
          ExcludeAssembliesWithoutSources = AssemblySearchType.MissingAll.ToString().ToLowerInvariant(),
        };

        // Replace the problematic line with the following code to fix the error:
        string moduleDirectory = Path.GetDirectoryName(AppContext.BaseDirectory) ?? string.Empty;

        _coverage = new Coverage(
            moduleDirectory,
            parameters,
            _logger,
            _serviceProvider.GetRequiredService<IInstrumentationHelper>(),
            _serviceProvider.GetRequiredService<IFileSystem>(),
            _serviceProvider.GetRequiredService<ISourceRootTranslator>(),
            _serviceProvider.GetRequiredService<ICecilSymbolHelper>());

        // Instrument assemblies before any test execution
        await Task.Run(() =>
        {
          CoveragePrepareResult prepareResult = _coverage.PrepareModules();
          _logger.LogInformation($"Code coverage instrumentation completed. Instrumented {prepareResult.Results.Length} modules");
        });

        return true;
      }
      catch (Exception ex)
      {
        _logger.LogError("Failed to initialize code coverage");
        _logger.LogError(ex);
        return false;
      }
    }

    /// <inheritdoc/>
    public async Task<bool> AfterTestSessionEndAsync(TestSessionContext context)
    {
      try
      {
        if (_coverage == null)
        {
          _logger.LogError("Coverage instance not initialized");
          return false;
        }

        _logger.LogInformation("\nCalculating coverage result...");
        CoverageResult result = _coverage.GetCoverageResult();

        string dOutput = _configuration.OutputDirectory != null ? _configuration.OutputDirectory : Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar.ToString();

        string directory = Path.GetDirectoryName(dOutput)!;

        if (!Directory.Exists(directory))
        {
          Directory.CreateDirectory(directory);
        }

        ISourceRootTranslator sourceRootTranslator = _serviceProvider.GetRequiredService<ISourceRootTranslator>();
        IFileSystem fileSystem = _serviceProvider.GetService<IFileSystem>()!;

        // Convert to coverlet format
        foreach (string format in _configuration.formats)
        {
          IReporter reporter = new ReporterFactory(format).CreateReporter();
          if (reporter == null)
          {
            throw new InvalidOperationException($"Specified output format '{format}' is not supported");
          }

          if (reporter.OutputType == ReporterOutputType.Console)
          {
            // Output to console
            _logger.LogInformation("  Outputting results to console", important: true);
            _logger.LogInformation(reporter.Report(result, sourceRootTranslator), important: true);
          }
          else
          {
            // Output to file
            string filename = Path.GetFileName(dOutput);
            filename = (filename == string.Empty) ? $"coverage.{reporter.Extension}" : filename;
            filename = Path.HasExtension(filename) ? filename : $"{filename}.{reporter.Extension}";

            string report = Path.Combine(directory, filename);
            _logger.LogInformation($"  Generating report '{report}'", important: true);
            await Task.Run(() => fileSystem.WriteAllText(report, reporter.Report(result, sourceRootTranslator)));
          }
        }

        _logger.LogInformation("Code coverage collection completed");
        return true;
      }
      catch (Exception ex)
      {
        _logger.LogError("Failed to collect code coverage");
        _logger.LogError(ex);
        return false;
      }
    }

    private IServiceProvider CreateServiceProvider()
    {
      var services = new ServiceCollection();

      // Register core dependencies
      services.AddSingleton<IFileSystem, FileSystem>();
      services.AddSingleton<IAssemblyAdapter, AssemblyAdapter>();
      services.AddSingleton<ILogger>(_logger);

      // Register instrumentation components with singleton lifetime
      services.AddSingleton<IInstrumentationHelper, InstrumentationHelper>();
      services.AddSingleton<ICecilSymbolHelper, CecilSymbolHelper>();

      // Register SourceRootTranslator with its dependencies
      services.AddSingleton<ISourceRootTranslator>(provider => new SourceRootTranslator(_configuration.sourceMappingFile, provider.GetRequiredService<ILogger>(), provider.GetRequiredService<IFileSystem>()));

      return services.BuildServiceProvider();
    }

    public Task OnTestSessionStartingAsync(SessionUid sessionUid, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }

    public Task OnTestSessionFinishingAsync(SessionUid sessionUid, CancellationToken cancellationToken)
    {
      throw new NotImplementedException();
    }
  }
}
