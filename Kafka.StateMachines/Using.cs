global using System;
global using System.Collections.Generic;
global using System.Collections.Immutable;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Diagnostics;
global using Microsoft.Extensions.Logging;
global using Confluent.Kafka;
global using Kafka.Messages;
global using Kafka.Operations;
global using static Kafka.Instrumentation.InstrumentationFuncs;
global using static Kafka.Operations.OperationsFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.Resiliency")]

namespace Kafka.StateMachines;

public static partial class StateMachinesFuncs;