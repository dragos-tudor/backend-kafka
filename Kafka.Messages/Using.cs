
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Globalization;
global using System.Diagnostics;
global using Confluent.Kafka;
global using static Kafka.Messages.MessagesFuncs;
global using static Kafka.Utils.UtilsFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.Clients")]
[assembly:InternalsVisibleTo("Kafka.Engine")]
[assembly:InternalsVisibleTo("Kafka.Instrumentation")]

namespace Kafka.Messages;

public static partial class MessagesFuncs;