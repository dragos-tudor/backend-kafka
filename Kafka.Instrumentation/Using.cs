
global using System;
global using System.Collections.Generic;
global using System.Collections.Immutable;
global using System.Diagnostics;
global using System.Diagnostics.Metrics;
global using Microsoft.Extensions.Logging;
global using Confluent.Kafka;
global using static Kafka.Messages.MessagesFuncs;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Kafka.StateMachines")]
[assembly: InternalsVisibleTo("Kafka.Operations.Inbox")]
[assembly: InternalsVisibleTo("Kafka.Operations.Outbox")]

namespace Kafka.Instrumentation;

public static partial class InstrumentationFuncs;