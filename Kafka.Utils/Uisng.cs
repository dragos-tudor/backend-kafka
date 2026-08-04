
global using System;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Text;
global using static Kafka.Utils.UtilsFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Kafka.Clients")]
[assembly:InternalsVisibleTo("Kafka.StateMachines")]
[assembly:InternalsVisibleTo("Kafka.Instrumentation")]
[assembly:InternalsVisibleTo("Kafka.Messages")]
[assembly:InternalsVisibleTo("Kafka.Resiliency")]

namespace Kafka.Utils;

public static partial class UtilsFuncs;