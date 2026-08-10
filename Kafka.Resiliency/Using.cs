global using System;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Logging;
global using Kafka.Clients;
global using Kafka.Operations.Inbox;
global using Kafka.Instrumentation;
global using Kafka.Pipelines;
global using static Kafka.Messages.MessagesFuncs;
global using static Kafka.Pipelines.PipelinesFuncs;
global using static Kafka.Utils.UtilsFuncs;

namespace Kafka.Resiliency;

public static partial class ResiliencyFuncs;