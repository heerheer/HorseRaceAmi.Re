using System.Text;
using AmiableNext.SDK;
using HorseRaceAmi.Re.Code.Module;
using HorseRaceAmi.Re.Code.Util;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.Re.Code.BotEvents.Race
{
    public class RaceCreate : IBotEvent
    {
        /// <summary>
        /// 为某个群打开一个赛马场!
        /// </summary>
        /// <param name="group"></param>
        /// <param name="setName">设置组的名字</param>
        /// <returns></returns>
        public static bool CreateRace(string group, string setName = "")
        {
            if (!HorseRace.Enable)
            {
                throw new Exception("赛马场正在停业整顿中!");
            }

            if (HorseRace.Races.ContainsKey(group))
            {
                throw new Exception("本群已存在一个赛马场!");
            }

            List<HorseRaceEvent> list;
            //否则添加一个赛马场
            // if (string.IsNullOrEmpty(setName))
            //{
            list = HrAmiModuleLoader.GetAllEvents(group);
            //}
            //else
            //{
            //list = GetEventSetAllowEvents(setName);
            // }

            HorseRace.Races.Add(group, new RaceGround(ConfigUtil.GetConfig().AllLong, list));
            HorseRace.RaceTimeHistory.Add(group, DateTime.Now);
            return true; // 创建成功
        }

        private static List<HorseRaceEvent> GetEventSetAllowEvents(string name)
        {
            List<HorseRaceEvent> list = new(); //= HrAmiModuleLoader.GetAllEvents(long.Parse(group));

            var config = ConfigUtil.GetConfig();

            if (!config.EventsSets.ContainsKey(name))
            {
                return list;
            }

            foreach (var str in config.EventsSets[name].EventGroups)
            {
                if (str.StartsWith("-"))
                {
                    var eventGroup = str.TrimStart('-').Trim();
                    list.RemoveAll(x => x.Group == eventGroup);
                }
            }

            foreach (var str in config.EventsSets[name].Events)
            {
                if (str.StartsWith("-"))
                {
                    var eventName = str.TrimStart('-').Trim();
                    list.RemoveAll(x => x.EventName == eventName);
                }
                else
                {
                    if (list.Exists(x => x.EventName == str.Trim()) is false)
                    {
                        //list.Add(HrAmiModuleLoader.GetAllEvents().Find(x => x.EventName == str.Trim()));
                    }
                }
            }

            return list;
        }

        // ReSharper disable once InconsistentNaming
        public void Process(AmiableEventContext ctx)
        {
            var e = ctx;

            if (e != null && !ConfigUtil.GetConfig().IsGroupEnable(e.GroupId))
                return;

            var message = e.Content;
            //创建赛马
            if (message.StartsWith("创建赛马"))
            {
                //检查防沉迷
                if (HorseRace.CanCreateRace(e.GroupId, out var left) is false)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"> 创建赛马比赛失败!");
                    sb.AppendLine($"> 原因:赫尔正在打扫赛马场...");
                    sb.AppendLine($"> 解决方案:等赫尔打扫完...");
                    sb.AppendLine($"> 打扫时间还剩:{left:g}秒");
                    sb.AppendLine($"> 你知道吗:{YouKnow.Get()}");
                    e.GroupReply(sb.ToString());
                    return;
                }


                var setname = e.Content.Substring(4).Trim();

                try
                {
                    CreateRace(e.GroupId, setname);
                    var sb = new StringBuilder();
                    sb.AppendLine($"> 创建赛马比赛成功!");
                    sb.AppendLine($"> 输入 加入赛马+名字 即可加入赛马");
                    //sb.AppendLine($"> 你知道吗:{YouKnow.Get()}");
                    e.GroupReply(sb.ToString());
                }
                catch (Exception exception)
                {
                    var sb = new StringBuilder();
                    if (HorseRace.Races.ContainsKey(e.GroupId))
                    {
                        HorseRace.Races.Remove(e.GroupId);
                    }

                    sb.AppendLine($"> 创建赛马比赛失败!");
                    //sb.AppendLine($"> 原因:{exception}");
                    e.GroupReply(sb.ToString());
                }
            }
        }

        public CommonEventType EventType { get; set; } = CommonEventType.MessageGroup;
    }
}