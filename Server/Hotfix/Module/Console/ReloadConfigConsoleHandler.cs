using System;
using NLog;

namespace ET
{
    [ConsoleHandler(ConsoleMode.ReloadConfig)]
    public class ReloadConfigConsoleHandler: IConsoleHandler
    {
        public async ETTask Run(ModeContex contex, string content)
        {
            switch (content)
            {
                case ConsoleMode.ReloadConfig:
                    contex.Parent.RemoveComponent<ModeContex>();
                    Log.Console("C must have config name, like: C UnitConfig");
                    break;

                case "C All":
                    ConfigComponent.Instance.Load();    //ȫ������������
                    Log.Warning("All�ȸ�������:" + UnitConfigCategory.Instance.Get(1001).Name);
                    break;

                default:
                    string[] ss = content.Split(" ");
                    string configName = ss[1];
                    string category = $"{configName}Category";
                    Type type = Game.EventSystem.GetType($"ET.{category}");
                    if (type == null)
                    {
                        Log.Console($"reload config but not find {category}");
                        return;
                    }
                    ConfigComponent.Instance.LoadOneConfig(type);       //��������������
                    Log.Console($"reload config {configName} finish!");

                    Log.Warning("�ȸ�������:" + UnitConfigCategory.Instance.Get(1001).Name);
                    break;
            }
            
            await ETTask.CompletedTask;
        }
    }
}