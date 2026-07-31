global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.Extensions.Logging;
global using Confluent.Kafka;
global using Kafka.Clients;
global using Kafka.Messages;
global using Kafka.Utils;
global using Kafka.Engine;
global using static Kafka.Clients.ClientsFuncs;
global using static Kafka.Messages.MessagesFuncs;
global using static Kafka.Engine.EngineFuncs;
global using static Kafka.Utils.UtilsFuncs;

namespace Kafka.Resiliency;

public static partial class ResiliencyFuncs;