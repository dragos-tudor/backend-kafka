global using System;
global using System.Linq;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using Confluent.Kafka;
global using Confluent.Kafka.Admin;
global using static Kafka.Clients.ClientsFuncs;
global using static Kafka.Messages.MessagesFuncs;
global using static Kafka.Utils.UtilsFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Inbox")]
[assembly:InternalsVisibleTo("Outbox")]
[assembly:InternalsVisibleTo("Resiliency")]

namespace Kafka.Clients;

public static partial class ClientsFuncs;