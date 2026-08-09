
global using System;
global using System.Collections.Generic;
global using System.Diagnostics;
global using System.Diagnostics.Metrics;
global using Microsoft.Extensions.Logging;
global using Confluent.Kafka;
global using static Kafka.Messages.MessagesFuncs;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("StateMachines")]
[assembly: InternalsVisibleTo("Inbox")]
[assembly: InternalsVisibleTo("Outbox")]

namespace Kafka.Instrumentation;

public static partial class InstrumentationFuncs;