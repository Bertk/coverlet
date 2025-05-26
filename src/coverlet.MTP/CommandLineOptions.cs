// Copyright (c) Toni Solarin-Sodara
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

//using coverlet.MTP.Extension;
using Microsoft.Testing.Platform.CommandLine;
using Microsoft.Testing.Platform.Extensions;
using Microsoft.Testing.Platform.Extensions.CommandLine;

namespace coverlet.MTP
{

  public sealed class CommandLineOption
  {
    public CommandLineOption(string name, string description, ArgumentArity arity, bool isHidden)
    {
      Name = name;
      Description = description;
      Arity = arity;
      IsHidden = isHidden;
    }

    public string Name { get; }
    public string Description { get; }
    public ArgumentArity Arity { get; }
    public bool IsHidden { get; }
  }

  public class CommandLineOptionsProvider : ICommandLineOptionsProvider
  {
    private readonly List<CommandLineOption> _commandLineOptions = new()
      {
        new CommandLineOption(name: "formats", description: "Specifies the output formats for the coverage report (e.g., 'json', 'lcov').", arity: ArgumentArity.OneOrMore, isHidden: false),
        new CommandLineOption(name: "exclude", description: "Filter expressions to exclude specific modules and types.", arity: ArgumentArity.OneOrMore, isHidden: false),
        new CommandLineOption(name: "include", description: "Filter expressions to include only specific modules and type", arity: ArgumentArity.OneOrMore, isHidden: false),
        new CommandLineOption(name: "exclude-by-file", description: "Glob patterns specifying source files to exclude.", arity: ArgumentArity.OneOrMore, isHidden: false),
        new CommandLineOption(name: "include-directory", description: "Include directories containing additional assemblies to be instrumented.", arity: ArgumentArity.OneOrMore, isHidden: false),
        new CommandLineOption(name: "exclude-by-attribute", description: "Attributes to exclude from code coverage.", arity: ArgumentArity.OneOrMore, isHidden: false),
        new CommandLineOption(name: "include-test-assembly", description: "Specifies whether to report code coverage of the test assembly.", arity: ArgumentArity.Zero, isHidden: false),
        new CommandLineOption(name: "single-hit", description: "Specifies whether to limit code coverage hit reporting to a single hit for each location", arity: ArgumentArity.Zero, isHidden: false),
        new CommandLineOption(name: "skipautoprops", description: "Neither track nor record auto-implemented properties.", arity: ArgumentArity.Zero, isHidden: false),
        new CommandLineOption(name: "does-not-return-attribute", description: "Attributes that mark methods that do not return", arity: ArgumentArity.ZeroOrMore, isHidden: false),
        new CommandLineOption(name: "exclude-assemblies-without-sources", description: "Specifies behavior of heuristic to ignore assemblies with missing source documents.", arity: ArgumentArity.ZeroOrOne, isHidden: false),
        new CommandLineOption(name: "source-mapping-file", description: "Specifies the path to a SourceRootsMappings file.", arity: ArgumentArity.ZeroOrOne, isHidden: false),

      };

    public string Uid => "Coverlet.MTP.CommandLineOptionsProvider";
    public string Version => "1.0.0";
    public string DisplayName => "Coverlet MTP Command Line Options Provider";
    public string Description => "Provides command line options for Coverlet MTP.";
    public Task<bool> IsEnabledAsync() => Task.FromResult(true);
    public IReadOnlyCollection<CommandLineOption> GetCommandLineOptions() => _commandLineOptions.AsReadOnly();

    public Task<ValidationResult> ValidateOptionArgumentsAsync(Microsoft.Testing.Platform.Extensions.CommandLine.CommandLineOption commandOption, string[] arguments)
    {
      if (commandOption.Name == "formats")
      {
        if (arguments.Length == 0)
        {
          return Task.FromResult(ValidationResult.Invalid("At least one format must be specified."));
        }
        if (!arguments[0].Contains("json") && !arguments[0].Contains("lcov") && !arguments[0].Contains("opencover") && !arguments[0].Contains("cobertura") && !arguments[0].Contains("teamcity"))
        {
          return Task.FromResult(ValidationResult.Invalid($"The value '{arguments[0]}' is not a valid option for '{commandOption.Name}'."));
        }
      }
      if (commandOption.Name == "exclude-assemblies-without-sources")
      {
        if (arguments.Length == 0)
        {
          return Task.FromResult(ValidationResult.Invalid($"At least one value must be specified for '{commandOption.Name}'."));
        }
        if (arguments.Length > 1)
        {
          return Task.FromResult(ValidationResult.Invalid($"Only one value is allowed for '{commandOption.Name}'."));
        }
        if (!arguments[0].Contains("MissingAll") && !arguments[0].Contains("MissingAny") && !arguments[0].Contains("MissingNone"))
        {
          return Task.FromResult(ValidationResult.Invalid($"The value '{arguments[0]}' is not a valid option for '{commandOption.Name}'."));
        }
      }

      return ValidationResult.ValidTask;
    }

  }

}
