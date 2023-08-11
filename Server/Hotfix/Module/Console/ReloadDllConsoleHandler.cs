namespace ET
{
    [ConsoleHandler(ConsoleMode.ReloadDll)]
    public class ReloadDllConsoleHandler: IConsoleHandler
    {
        public async ETTask Run(ModeContex contex, string content)
        {
            switch (content)
            {
                case ConsoleMode.ReloadDll:
                    contex.Parent.RemoveComponent<ModeContex>();
                    Log.Warning("执行逻辑热重载..");
                    Game.EventSystem.Add(DllHelper.GetHotfixAssembly());
                    Game.EventSystem.Load();
                    Log.Warning("热重载逻辑结束..");
                    break;
            }
            
            await ETTask.CompletedTask;
        }
    }
}