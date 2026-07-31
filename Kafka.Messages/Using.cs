
global using System;
global using System.Linq;
global using System.Text;
global using System.Globalization;
global using Confluent.Kafka;
global using Kafka.Utils;
global using static Kafka.Messages.MessagesFuncs;
global using static Kafka.Utils.UtilsFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.Clients")]
[assembly:InternalsVisibleTo("Kafka.Engine")]

namespace Kafka.Messages;

public static partial class MessagesFuncs;