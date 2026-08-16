
global using System;
global using System.Collections.Generic;
global using System.Diagnostics;
global using System.Diagnostics.Metrics;
global using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Kafka.Pipelines")]
[assembly: InternalsVisibleTo("Kafka.Operations.Inbox")]
[assembly: InternalsVisibleTo("Kafka.Operations.Outbox")]
[assembly: InternalsVisibleTo("Kafka.Operations.DeadLetter")]

namespace Kafka.Instrumentation;

public static partial class InstrumentationFuncs;