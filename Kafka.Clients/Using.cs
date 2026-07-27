global using System;
global using System.Linq;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using Confluent.Kafka;
global using Confluent.Kafka.Admin;
global using Kafka.Messages;
global using static Kafka.Clients.ClientsFuncs;
global using static Kafka.Messages.MessagesFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka")]

namespace Kafka.Clients;

public static partial class ClientsFuncs;