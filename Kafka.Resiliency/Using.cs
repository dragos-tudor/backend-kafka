global using System;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Logging;
global using Confluent.Kafka;
global using Kafka.Clients;
global using Kafka.Operations.Inbox;
global using Kafka.Instrumentation;
global using Kafka.StateMachines;
global using static Kafka.Clients.ClientsFuncs;
global using static Kafka.StateMachines.StateMachinesFuncs;
global using static Kafka.Utils.UtilsFuncs;

namespace Kafka.Resiliency;

public static partial class ResiliencyFuncs;