
global using System;
global using System.Diagnostics;
global using System.Diagnostics.Metrics;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Logging;
global using Confluent.Kafka;
global using Kafka.Clients;
global using Kafka.Instrumentation;
global using Kafka.Messages;
global using Kafka.Utils;
global using static Kafka.Clients.ClientsFuncs;
global using static Kafka.Instrumentation.InstrumentationFuncs;
global using static Kafka.Messages.MessagesFuncs;
global using static Kafka.Operations.Inbox.InboxFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.Pipelines")]

namespace Kafka.Operations.Inbox;

public static partial class InboxFuncs
{
  static readonly internal Meter InboxMeter = new ("kafka.inbox");
}