
global using System;
global using System.Threading.Tasks;
global using System.Collections.Generic;
global using System.Threading;
global using System.Globalization;
global using Confluent.Kafka;
global using static Kafka.Messages.MessagesFuncs;
global using static Kafka.Utils.UtilsFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.Clients")]
[assembly:InternalsVisibleTo("Kafka.StateMachines")]
[assembly:InternalsVisibleTo("Kafka.Instrumentation")]
[assembly:InternalsVisibleTo("Kafka.Inbox")]

namespace Kafka.Messages;

public static partial class MessagesFuncs;