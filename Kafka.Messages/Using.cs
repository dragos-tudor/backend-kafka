
global using System;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Threading;
global using System.Globalization;
global using Confluent.Kafka;
global using static Kafka.Messages.MessagesFuncs;
global using static Kafka.Utils.UtilsFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Clients")]
[assembly:InternalsVisibleTo("StateMachines")]
[assembly:InternalsVisibleTo("Instrumentation")]
[assembly:InternalsVisibleTo("Inbox")]
[assembly:InternalsVisibleTo("Outbox")]

namespace Kafka.Messages;

public static partial class MessagesFuncs;