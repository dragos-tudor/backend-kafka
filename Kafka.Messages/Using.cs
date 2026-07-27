
global using System;
global using System.Linq;
global using System.Text;
global using System.Globalization;
global using Confluent.Kafka;
global using static Kafka.Messages.MessagesFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.Clients")]
[assembly:InternalsVisibleTo("Kafka")]

namespace Kafka.Messages;

public static partial class MessagesFuncs;