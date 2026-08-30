// Copyright (c) Toni Solarin-Sodara
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Coverlet.Core;
using Coverlet.Core.Abstractions;
using Coverlet.Core.Enums;
using Coverlet.MTP.Configuration;
using Microsoft.Testing.Platform.CommandLine;
using Microsoft.Testing.Platform.Configurations;
using Microsoft.Testing.Platform.Extensions.OutputDevice;
using Microsoft.Testing.Platform.Logging;
using Microsoft.Testing.Platform.OutputDevice;
using Moq;
using Xunit;

namespace Coverlet.MTP.Collector.Tests;

public class CollectorExtensionThresholdTests
{
  [Fact]
  public async Task DisplayThresholdSummaryAsyncDisplaysConfiguredPassingMetrics()
  {
    var outputDevice = new Mock<IOutputDevice>();
    CollectorExtension collector = CreateCollector(outputDevice);
    ConfigureThreshold(collector, 70, ThresholdStatistic.Total, ["line", "branch", "method"]);

    await DisplayThresholdSummaryAsync(collector, CreateCoverageResult(hits: 1));

    outputDevice.Verify(x => x.DisplayAsync(
      It.Is<IOutputDeviceDataProducer>(producer => producer == collector),
      It.Is<TextOutputDeviceData>(data =>
        data.Text.Contains("Coverage Threshold Results:") &&
        data.Text.Contains("Total - Line (Total over Module): 100.0% >= 70.0% threshold") &&
        data.Text.Contains("Total - Branch (Total over Module): 100.0% >= 70.0% threshold") &&
        data.Text.Contains("Total - Method (Total over Module): 100.0% >= 70.0% threshold")),
      It.IsAny<CancellationToken>()),
      Times.Once);
  }

  [Fact]
  public async Task DisplayThresholdSummaryAsyncDisplaysFailedMetric()
  {
    var outputDevice = new Mock<IOutputDevice>();
    CollectorExtension collector = CreateCollector(outputDevice);
    ConfigureThreshold(collector, 70, ThresholdStatistic.Total, ["line"]);

    await DisplayThresholdSummaryAsync(collector, CreateCoverageResult(hits: 0));

    outputDevice.Verify(x => x.DisplayAsync(
      It.IsAny<IOutputDeviceDataProducer>(),
      It.Is<TextOutputDeviceData>(data =>
        data.Text.Contains("Total - Line (Total over Module): 0.0% < 70.0% threshold") &&
        data.Text.Contains("The total line coverage is below the specified 70.0% threshold.")),
      It.IsAny<CancellationToken>()),
      Times.Once);
  }

  private static CollectorExtension CreateCollector(Mock<IOutputDevice> outputDevice)
  {
    var loggerFactory = new Mock<ILoggerFactory>();
    loggerFactory.Setup(factory => factory.CreateLogger(It.IsAny<string>()))
      .Returns(new Mock<Microsoft.Testing.Platform.Logging.ILogger>().Object);

    var commandLineOptions = new Mock<ICommandLineOptions>();
    var configuration = new Mock<IConfiguration>();
    var fileSystem = new Mock<IFileSystem>();
    outputDevice.Setup(device => device.DisplayAsync(
      It.IsAny<IOutputDeviceDataProducer>(),
      It.IsAny<IOutputDeviceData>(),
      It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

    return new CollectorExtension(
      loggerFactory.Object,
      commandLineOptions.Object,
      outputDevice.Object,
      configuration.Object,
      fileSystem.Object);
  }

  private static void ConfigureThreshold(CollectorExtension collector, int threshold, ThresholdStatistic thresholdStat, List<string> thresholdTypes)
  {
    System.Reflection.FieldInfo configurationField = typeof(CollectorExtension)
      .GetField("_configuration", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
    var configuration = (CoverletExtensionConfiguration)configurationField.GetValue(collector)!;
    configuration.Threshold = threshold;
    configuration.ThresholdStat = thresholdStat;
    configuration.ThresholdType = thresholdTypes;
  }

  private static async Task DisplayThresholdSummaryAsync(CollectorExtension collector, CoverageResult result)
  {
    System.Reflection.MethodInfo method = typeof(CollectorExtension)
      .GetMethod("DisplayThresholdSummaryAsync", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
    await (Task)method.Invoke(collector, [result, CancellationToken.None])!;
  }

  private static CoverageResult CreateCoverageResult(int hits)
  {
    var methods = new Methods
    {
      ["System.Void TestClass::TestMethod()"] = new Method
      {
        Lines = new Lines { { 1, hits } },
        Branches = [new BranchInfo { Line = 1, Hits = hits }]
      }
    };
    var classes = new Classes { ["TestClass"] = methods };
    var documents = new Documents { ["TestClass.cs"] = classes };

    return new CoverageResult
    {
      Identifier = "test-id",
      Modules = new Modules { ["test.dll"] = documents },
      Parameters = new CoverageParameters()
    };
  }
}
