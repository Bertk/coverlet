// Copyright (c) Toni Solarin-Sodara
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using coverlet.core.coverage.tests;
using Coverlet.Core.Tests.Infrastructure;

RegisterUnhandledExceptionLogging();

// Handle ProcessExecutor invocations when running as a child process
if (ProcessExecutor.TryExecute(args))
{
  return Environment.ExitCode;
}

// Normal test execution via xunit.v3.mtp-v2
// Check for automated/inline arguments (used by xunit console runner)
if (args.Any(arg => arg == "-automated" || arg == "@@"))
{
  return await Xunit.Runner.InProc.SystemConsole.ConsoleRunner.Run(args);
}
else
{
  return await Xunit.MicrosoftTestingPlatform.TestPlatformTestFramework.RunAsync(args, SelfRegisteredExtensions.AddSelfRegisteredExtensions);
}

static void RegisterUnhandledExceptionLogging()
{
  AppDomain.CurrentDomain.UnhandledException += (_, eventArgs) =>
  {
    LogUnhandledException(eventArgs.ExceptionObject as Exception, "AppDomain.CurrentDomain.UnhandledException");
  };

  TaskScheduler.UnobservedTaskException += (_, eventArgs) =>
  {
    LogUnhandledException(eventArgs.Exception, "TaskScheduler.UnobservedTaskException");
  };
}

static void LogUnhandledException(Exception exception, string source)
{
  Console.Error.WriteLine($"[{source}] Unhandled exception detected.");
  Console.Error.WriteLine(UnhandledExceptionTestContextTracker.GetCurrentDiagnosticMessage());

  if (exception is not null)
  {
    Console.Error.WriteLine(exception);
  }
}
