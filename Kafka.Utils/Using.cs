
global using System;
global using System.ComponentModel.DataAnnotations;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Text;
global using static Kafka.Utils.UtilsFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.Clients")]
[assembly:InternalsVisibleTo("Kafka.Pipelines")]
[assembly:InternalsVisibleTo("Kafka.Instrumentation")]
[assembly:InternalsVisibleTo("Kafka.Messages")]
[assembly:InternalsVisibleTo("Kafka.Operations.Inbox")]
[assembly:InternalsVisibleTo("Kafka.Operations.Outbox")]
[assembly:InternalsVisibleTo("Kafka.Operations.DeadLetter")]
[assembly:InternalsVisibleTo("Kafka.Resiliency")]

namespace Kafka.Utils;

public static partial class UtilsFuncs;