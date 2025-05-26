// Copyright (c) Toni Solarin-Sodara
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.ComponentModel.DataAnnotations;
using Microsoft.Testing.Platform.CommandLine;
using Microsoft.Testing.Platform.Extensions.TestFramework;

namespace coverlet.MTP.Extension
{
  public interface IExtension
  {
    string Uid { get; }
    string Version { get; }
    string DisplayName { get; }
    string Description { get; }
    Task<bool> IsEnabledAsync();
  }

  public interface ITestFramework : IExtension
  {
    Task<CreateTestSessionResult> CreateTestSessionAsync(CreateTestSessionContext context);
    Task ExecuteRequestAsync(ExecuteRequestContext context);
    Task<CloseTestSessionResult> CloseTestSessionAsync(CloseTestSessionContext context);
  }

  // Add test hooks:
  internal interface ITestHooks
  {
    Task OnBeforeInstrumentationAsync(string modulePath);
    Task OnAfterInstrumentationAsync(string modulePath);
    //Task OnCoverageDataCollectedAsync(CoverageData data);
  }

  public interface ICommandLineOptionsProvider : IExtension
  {
    IReadOnlyCollection<CommandLineOption> GetCommandLineOptions();

    Task<ValidationResult> ValidateOptionArgumentsAsync(
        CommandLineOption commandOption,
        string[] arguments);

    Task<ValidationResult> ValidateCommandLineOptionsAsync(
        ICommandLineOptions commandLineOptions);
  }
}
