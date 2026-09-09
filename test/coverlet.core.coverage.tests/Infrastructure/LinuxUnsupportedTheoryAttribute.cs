// Copyright (c) Toni Solarin-Sodara
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using Xunit;

namespace Coverlet.Core.Tests.Infrastructure;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class LinuxUnsupportedTheoryAttribute : TheoryAttribute
{
  public LinuxUnsupportedTheoryAttribute(
    [CallerFilePath] string sourceFilePath = "",
    [CallerLineNumber] int sourceLineNumber = 0)
    : base(sourceFilePath, sourceLineNumber)
  {
    SkipType = typeof(TestEnvironment);
    SkipWhen = nameof(TestEnvironment.IsLinux);
    Skip = "Skipped on Linux due to runtime IL limitations (BadImageFormatException / InvalidProgramException).";
  }
}
