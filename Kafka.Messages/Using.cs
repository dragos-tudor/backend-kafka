
global using System;
global using System.ComponentModel.DataAnnotations;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Threading;
global using System.Globalization;
global using Confluent.Kafka;
global using Kafka.Utils;
global using static Kafka.Messages.MessagesFuncs;
global using static Kafka.Utils.UtilsFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.Pipelines")]
[assembly:InternalsVisibleTo("Kafka.Instrumentation")]
[assembly:InternalsVisibleTo("Kafka.Operations.Inbox")]
[assembly:InternalsVisibleTo("Kafka.Operations.Outbox")]
[assembly:InternalsVisibleTo("Kafka.Operations.DeadLetter")]

namespace Kafka.Messages;

public static partial class MessagesFuncs;