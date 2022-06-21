using System.Reflection;
using System.Text;
using System.Text.Json;
using AmiableNext.SDK;
using HorseRaceAmi.Re.Code.BotEvents.Race;
using HorseRaceAmi.Re.Code.Dlc;
using HorseRaceAmi.Re.Code.Util;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.BotEvents
{
    public class HraCommands : IBotEvent
    {
        private AmiableEventContext _ctx;


        public CommonEventType EventType { get; set; } = CommonEventType.MessageGroup;

        public void Process(AmiableEventContext ctx)
        {
            _ctx = ctx;
            string command = "";
            if (ctx.Content.StartsWith($"[@{ctx.BotId}]"))
            {
                command = ctx.Content.Substring($"[@{ctx.BotId}]".Length).Trim();
            }

            if (command.StartsWith(".hrami"))
            {
                var config = ConfigUtil.GetConfig();
                var args = command.Substring(6).Trim().Split(' ');

                var newargs = args.Skip(1).ToArray(); //去掉前面-x后的args
                switch (args.ElementAtOrDefault(0))
                {
                    case "-i" or "-info" or "info" or "":
                        InfoCommand();
                        break;
                    case "-u" or "-user":
                        if (config.IsMaster(ctx.AuthorId))
                            UserCommand(newargs);
                        break;
                    case "-d" or "dlc":
                        if (config.IsMaster(ctx.AuthorId))
                            DlcCommand(newargs);
                        break;
                    case "-t" or "test":
                        if (config.IsMaster(ctx.AuthorId))
                            TestCommand(newargs);
                        break;
                    case "-cfg" or "-c" or "config" or "cfg":
                        if (config.IsMaster(ctx.AuthorId))
                            ConfigCommand(newargs);
                        break;

                    case "-r" or "race":
                        if (config.IsMaster(ctx.AuthorId))
                            RaceCommand(newargs);
                        break;
                    default:
                        InfoCommand();
                        break;
                }
            }
        }

        private void RaceCommand(string[] args)
        {
            switch (args.ElementAtOrDefault(0))
            {
                case "stop": //强制结束比赛
                {
                    HorseRace.RemoveRace(_ctx.GroupId.ToString());
                    _ctx.GroupReply("> 强制结束成功");
                    break;
                }
                case "clean":
                {
                    if (HorseRace.CanCreateRace(_ctx.GroupId, out var _))
                    {
                        HorseRace.RaceTimeHistory.Remove(_ctx.GroupId);
                        _ctx.GroupReply("> 赫尔因为咕咕咕已被击杀，场地可以不用打扫了。");
                    }

                    break;
                }
                case "open":
                {
                    HorseRace.Enable = true;
                    _ctx.GroupReply("> 场地修业结束!重新开放!Opened!");
                    break;
                }
                case "close":
                {
                    HorseRace.Enable = false;
                    _ctx.GroupReply("> 场地开始停业整顿!Closed!");
                    break;
                }
                default:
                    var sb = new StringBuilder();
                    sb.AppendLine($"> stop 强行停止比赛");
                    sb.AppendLine($"> clean 强行清理赛场");
                    _ctx.GroupReply(sb.ToString());
                    break;
            }
        }

        private void DlcCommand(string[] args)
        {
            switch (args.ElementAtOrDefault(0))
            {
                case "check": //检查DLC版本更新
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"> 正在检查DLC版本更新");
                    DlcLoader.DlcInfos.ForEach(x =>
                    {
                        //sb.AppendLine($"{x.DlcName}{(VersionUtil.CheckLatestVersion(x) ? "需要更新~" : "版本最新!!!")}");
                    });
                    sb.AppendLine($"> 你知道吗:{YouKnow.Get()}");
                    _ctx.GroupReply(sb.ToString());
                    break;
                }
                case "list": //获取当前被读取的所有DLC
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"> 当前已安装DLC列表:");
                    DlcLoader.DlcInfos.ForEach(x => { sb.AppendLine($"{x.DlcName} - {x.Version}"); });
                    _ctx.GroupReply(sb.ToString());
                    break;
                }
                default: //提示帮助文档
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"> check 检查所有DLC的版本信息");
                    sb.AppendLine($"> list 列出所有安装的DLC以及对应状态");
                    _ctx.GroupReply(sb.ToString());
                    break;
                }
            }
        }

        private void ConfigCommand(string[] args)
        {
            var cfg = ConfigUtil.GetConfig();
            switch (args.ElementAtOrDefault(0))
            {
                case "imagemode": //反转图片模式
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"> 图片模式设置完成:{cfg.ImageMode} -> {!cfg.ImageMode}");
                    sb.AppendLine(YouKnow.Get());
                    _ctx.GroupReply(sb.ToString());
                    cfg.ImageMode = !cfg.ImageMode;
                    ConfigUtil.SetConfig(cfg);
                    break;
                }
                case "off": //关闭这个群的赛马
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"> 本群赛马已关闭。使用 @<bot> .hrami -cfg on 来开启");
                    _ctx.GroupReply(sb.ToString());
                    cfg.DisableGroups.Add(_ctx.GroupId);
                    ConfigUtil.SetConfig(cfg);
                    break;
                }
                case "on": //开启这个群的赛马
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"> 本群赛马开启成功。使用 @<bot> .hrami -cfg off 来开启");
                    _ctx.GroupReply(sb.ToString());
                    cfg.DisableGroups.Remove(_ctx.GroupId);
                    ConfigUtil.SetConfig(cfg);
                    break;
                }
                default: //提示帮助文档
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"> @bot .hrami -c on/off");
                    sb.AppendLine(JsonSerializer.Serialize(cfg));
                    _ctx.GroupReply(sb.ToString());
                    break;
                }
            }
        }

        private void UserCommand(string[] args)
        {
            switch (args.ElementAtOrDefault(0))
            {
                case "coin":
                {
                    var qq_or_at = args.ElementAtOrDefault(1)!;
                    qq_or_at = qq_or_at.TrimStart("[@".ToCharArray()).TrimEnd(']');

                    var change = args.ElementAtOrDefault(2);
                    if (int.TryParse(change, out int _change))
                    {
                        var sb = new StringBuilder();
                        sb.AppendLine($"> 增加积分成功。");
                        _ctx.GroupReply(sb.ToString());

                        DataUtil.AddCoin(qq_or_at, _change);
                    }

                    break;
                }
                case "baga":
                {
                    var qq_or_at = args.ElementAtOrDefault(1)!;
                    qq_or_at = qq_or_at.TrimStart("[@".ToCharArray()).TrimEnd(']');
                    var item = args.ElementAtOrDefault(2)!;
                    DataUtil.AddItem(qq_or_at, item, 1);
                    var sb = new StringBuilder();
                    sb.AppendLine($"> 增加{item}成功。");
                    _ctx.GroupReply(sb.ToString());
                    break;
                }
                case "bagr":
                {
                    var qq_or_at = args.ElementAtOrDefault(1)!;
                    qq_or_at = qq_or_at.TrimStart("[@".ToCharArray()).TrimEnd(']');
                    var item = args.ElementAtOrDefault(2)!;
                    DataUtil.AddItem(qq_or_at, item, -1);
                    var sb = new StringBuilder();
                    sb.AppendLine($"> 减少{item}成功。");
                    _ctx.GroupReply(sb.ToString());
                    break;
                }
                default: //提示帮助文档
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"> 用户数据操作");
                    sb.AppendLine($"> .hrami -u");
                    sb.AppendLine($"> -u baga @xxx item_Name");
                    sb.AppendLine($"> -u bagr @xxx item_Name");
                    _ctx.GroupReply(sb.ToString());
                    break;
                }
            }
        }

        private void InfoCommand()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"当前版本: HorseRaceAmi.Re {"???????懒得写版本"}");
            sb.AppendLine($"程序集版本: {Assembly.GetExecutingAssembly().GetName().Version}");
            sb.AppendLine($"> -i/info 查看HRA核心信息");
            sb.AppendLine($"> -d/dlc 查看DLC相关命令");
            sb.AppendLine($"> -r/race 查看比赛相关命令");
            sb.AppendLine($"> -c/config 操作配置文件");
            sb.AppendLine($"帮助文档:https://gitee.com/heerkaisair/horse-race-ami");

            _ctx.GroupReply(sb.ToString());
        }

        private void TestCommand(string[] args)
        {
            switch (args.ElementAtOrDefault(0))
            {
                case "fr":
                {
                    var playerscount = 4;

                    var number = args.ElementAtOrDefault(1);
                    if (number != "")
                    {
                        playerscount = int.Parse(number);
                    }

                    var group = _ctx.GroupId.ToString();
                    if (HorseRace.Races.ContainsKey(group))
                    {
                        HorseRace.Races.Remove(group);
                    }

                    if (HorseRace.RaceTimeHistory.ContainsKey(_ctx.GroupId))
                    {
                        HorseRace.RaceTimeHistory.Remove(_ctx.GroupId);
                    }

                    RaceCreate.CreateRace(group);
                    for (int i = 0;
                         i < playerscount;
                         i++)
                    {
                        RaceJoin.JoinRace(group, "12345" + i, $"》{i}《");
                    }

                    RaceStart.StartRaceInGroup(_ctx, group);
                    _ctx.GroupReply("> 操作成功");
                }
                    break;
                case "db":
                {
                    try
                    {
                    }
                    catch (Exception e)
                    {
                        _ctx.GroupReply($"> 操作失败\n{e.ToString()}");
                    }
                }
                    break;
                case "m":
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"群号:{_ctx.GroupId}");
                    foreach (var module in ConfigUtil.GetConfig().GetEnableModulesInGroup(_ctx.GroupId))
                    {
                        sb.AppendLine($"{module.Name}:开启");
                    }

                    _ctx.GroupReply(sb.ToString());
                }
                    break;
            }
        }
    }
}