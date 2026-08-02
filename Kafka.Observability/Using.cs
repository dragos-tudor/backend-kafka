
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Diagnostics;
global using Microsoft.Extensions.Logging;
global using OpenTelemetry;
global using OpenTelemetry.Context.Propagation;
global using Confluent.Kafka;
global using static Kafka.Messages.MessagesFuncs;
global using static Kafka.Utils.UtilsFuncs;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Kafka.Engine")]

namespace Kafka.Observability;

public static partial class ObservabilityFuncs;