global using System;
global using System.Collections.Generic;
global using System.Collections.Immutable;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Diagnostics;
global using System.Diagnostics.Metrics;
global using Microsoft.Extensions.Logging;
global using Confluent.Kafka;
global using Kafka.Clients;
global using Kafka.Instrumentation;
global using Kafka.Messages;
global using Kafka.Utils;
global using static Kafka.Clients.ClientsFuncs;
global using static Kafka.Messages.MessagesFuncs;
global using static Kafka.Instrumentation.InstrumentationFuncs;
global using MetricCounters = System.Collections.Generic.IDictionary<Kafka.Engine.MetricCounterTypes, System.Diagnostics.Metrics.Counter<long>>;
global using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.Resiliency")]

namespace Kafka.Engine;

public static partial class EngineFuncs;