global using System;
global using System.Collections.Generic;
global using System.Globalization;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Diagnostics;
global using System.Diagnostics.Metrics;
global using Microsoft.Extensions.Logging;
global using Confluent.Kafka;
global using Kafka.Clients;
global using Kafka.Instrumentation;
global using Kafka.Messages;
global using Kafka.Operations.Inbox;
global using Kafka.Operations.Outbox;
global using Kafka.Operations.DeadLetter;
global using static Kafka.Instrumentation.InstrumentationFuncs;
global using static Kafka.Operations.Inbox.InboxFuncs;
global using static Kafka.Operations.Outbox.OutboxFuncs;
global using static Kafka.Operations.DeadLetter.DeadLetterFuncs;
global using static Kafka.Pipelines.PipelinesFuncs;
global using static Kafka.Utils.UtilsFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.Resiliency")]

namespace Kafka.Pipelines;

public static partial class PipelinesFuncs
{
  static readonly internal Meter PipelinesMeter = new ("kafka.pipelines");
}