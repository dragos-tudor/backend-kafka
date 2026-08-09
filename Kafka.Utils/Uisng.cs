
global using System;
global using System.Collections.Generic;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Text;
global using static Kafka.Utils.UtilsFuncs;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Clients")]
[assembly:InternalsVisibleTo("StateMachines")]
[assembly:InternalsVisibleTo("Instrumentation")]
[assembly:InternalsVisibleTo("Messages")]
[assembly:InternalsVisibleTo("Resiliency")]

namespace Kafka.Utils;

public static partial class UtilsFuncs;