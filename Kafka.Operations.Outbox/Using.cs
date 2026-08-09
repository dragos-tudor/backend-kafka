
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
global using static Kafka.Operations.Outbox.OutboxFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.StateMachines")]

namespace Kafka.Operations.Outbox;

public static partial class OutboxFuncs
{
  static readonly internal Meter OutboxMeter = new ("kafka.outbox");
}