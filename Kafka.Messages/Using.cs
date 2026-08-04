
global using System;
global using System.Globalization;
global using Confluent.Kafka;
global using static Kafka.Messages.MessagesFuncs;
global using static Kafka.Utils.UtilsFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.Clients")]
[assembly:InternalsVisibleTo("Kafka.StateMachines")]
[assembly:InternalsVisibleTo("Kafka.Instrumentation")]
[assembly:InternalsVisibleTo("Kafka.Operations")]

namespace Kafka.Messages;

public static partial class MessagesFuncs;