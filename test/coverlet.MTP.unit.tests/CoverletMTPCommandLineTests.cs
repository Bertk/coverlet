// Copyright (c) Toni Solarin-Sodara
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;
using coverlet.Extension;

using Microsoft.Testing.Platform.Extensions;
using Microsoft.Testing.Platform.Extensions.CommandLine;
using Xunit;

namespace coverlet.MTP.unit.tests
{
  public class CoverletMTPCommandLineTests
  {
    private readonly CoverletExtension _extension = new();

    [Fact]
    public async Task IsInvValid_If_Format_Has_IncorrectValue()
    {
      var provider = new CoverletExtensionCommandLineProvider(_extension);
      CommandLineOption option = provider.GetCommandLineOptions().First(x => x.Name == "formats");

      ValidationResult validateOptionsResult = await provider.ValidateOptionArgumentsAsync(option, ["invalid"]);
      Assert.Equal("The value 'invalid' is not a valid option for 'formats'.", validateOptionsResult.ErrorMessage);
      Assert.False(validateOptionsResult.IsValid);
    }

    [Fact]
    public async Task CoverletMTP_CommandLineOptions_Are_AlwaysValid()
    {
      var provider = new CoverletExtensionCommandLineProvider(_extension);

      ValidationResult validateOptionsResult = await provider.ValidateCommandLineOptionsAsync(new TestCommandLineOptions([])).ConfigureAwait(false);
      Assert.True(validateOptionsResult.IsValid);
      Assert.True(string.IsNullOrEmpty(validateOptionsResult.ErrorMessage));
    }

    internal sealed class TestCommandLineOptions : Microsoft.Testing.Platform.CommandLine.ICommandLineOptions
    {
      private readonly Dictionary<string, string[]> _options;

      public TestCommandLineOptions(Dictionary<string, string[]> options) => _options = options;

      public bool IsOptionSet(string optionName) => _options.ContainsKey(optionName);

      public bool TryGetOptionArgumentList(string optionName, [NotNullWhen(true)] out string[]? arguments) => _options.TryGetValue(optionName, out arguments);
    }
  }
}
