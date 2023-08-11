using System.Collections.Generic;
using System.Threading;

namespace ET
{
    public static class ConsoleMode
    {
        public const string ReloadDll = "R";        //热重载dll代码
        public const string ReloadConfig = "C";     //热重载配置
        public const string ShowMemory = "M";
        public const string Repl = "Repl";
        public const string Debugger = "Debugger";
        public const string CreateRobot = "CreateRobot";
        public const string Robot = "Robot";
    }

    [ComponentOf(typeof(Scene))]
    public class ConsoleComponent: Entity, IAwake, ILoad
    {
        public CancellationTokenSource CancellationTokenSource;
        public Dictionary<string, IConsoleHandler> Handlers = new Dictionary<string, IConsoleHandler>();
    }
}