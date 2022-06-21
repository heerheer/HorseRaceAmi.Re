using System.Text;
using AmiableNext.SDK;
using HorseRaceAmi.Re.Code.dayz;
using HorseRaceAmi.Re.Code.Util;
using HorseRaceAmi.SDK.Model;
using ConfigUtil = HorseRaceAmi.Re.Code.Util.ConfigUtil;

namespace HorseRaceAmi.Re.Code.BotEvents.Race
{
    public class RaceJoin : IBotEvent
    {
        /// <summary>
        /// 以默认的🐎加入赛马场
        /// </summary>
        /// <param name="group"></param>
        /// <param name="qq"></param>
        /// <param name="display"></param>
        /// <returns></returns>
        public static bool JoinRace(string group, string qq, string display = "")
        {
            display = display.Trim(); //处理空白字符

            if (!HorseRace.Enable)
            {
                throw new Exception("赛马场正在停业整顿中!");
            }

            if (string.IsNullOrEmpty(display))
            {
                display = "(选手)";
            }

            if (!HorseRace.Races.ContainsKey(group))
                throw new Exception("本群还未开启比赛!");

            if (HorseRace.Races[group].Horses.Exists(h => h.Display == display))
                throw new Exception("名字重复啦!!!");
            if (HorseRace.Races[group].Horses.Find(h => h.OwnerQQ == qq) != null)
                throw new Exception("您已经加入了赛马场!");

            //已经加入了赛马场

            if (HorseRace.Races[group].IsOn)
                throw new Exception("您不能在已经开启的场地中散步!");

            if (ConfigUtil.GetConfig().EnableEmoji)
            {
                if (display.Contains("[pic=") || display.Contains("[CQ:image"))
                {
                    throw new Exception("起码别用图片哦~");
                }

                if (display.Length >= 20)
                {
                    throw new Exception("表情太长啦");
                }
            }
            else
            {
                //赛马已开始
                if (display.Contains("["))
                {
                    throw new Exception("咱不能用任何的Code哦嗷呜");
                }
            }

            if (display.ToCharArray().All(s => s == '.'))
            {
                throw new Exception("我们不允许隐身人参赛");
            }

            if (!(display.StartsWith("[") && display.EndsWith("]")) && display.Length >= 14)
            {
                throw new Exception("您名字这么长可真厉害");
            }

            //dayzserver 联动
            var dayzUtil = new UserUtil();
            var info = dayz.ConfigUtil.GetHorseRaceInfo();
            if (info.Enable)
            {
                var race = HorseRace.Races[group];
                var get = dayzUtil.GetInfo(qq);
                if (get.result == true)
                {
                    if (get.data.Value.currency >= info.JoinCoin)
                    {
                        dayzUtil.AddCoin(qq, -info.JoinCoin);
                    }
                    else
                    {
                        throw new Exception("DAYZ金币不足");
                    }
                }
                else
                {
                    throw new Exception("DAYZ数据异常");
                }
            }
            //联动结束

            HorseRace.Races[group].Horses.Add(new Horse() { Display = display, OwnerQQ = qq });
            return true;
        }

        public void Process(AmiableEventContext ctx)
        {
            var e = ctx;

            if (!ConfigUtil.GetConfig().IsGroupEnable(e.GroupId))
                return;

            var message = e.Content;
            try
            {
                if (message.StartsWith("加入赛马"))
                {
                    var horse = message.Replace("加入赛马", "").Trim();
                    if (string.IsNullOrEmpty(horse))
                    {
                        e.GroupReply(
                            $"[@{ctx.AuthorId}]\n" +
                            $"> 加入赛马失败!!!\n" +
                            $"> 请确定选手名称!\n" +
                            $"> 格式为 加入赛马+名称");
                        return;
                    }

                    try
                    {
                        JoinRace(e.GroupId, e.AuthorId, horse);
                    }
                    catch (Exception exception)
                    {
                        var sb = new StringBuilder();
                        sb.AppendLine($"> 加入失败");
                        sb.AppendLine($"> 原因:{exception.Message}");
                        sb.AppendLine($"> 你知道吗:{YouKnow.Get()}");
                        e.GroupReply(sb.ToString());
                        return;
                    }


                    var maxJoin = ConfigUtil.GetConfig().MaxJoin;

                    var nowJoin = HorseRace.Races[e.GroupId].Horses.Count;


                    e.GroupReply(
                        $"[@{e.AuthorId}]\n" +
                        $"> 加入赛马成功\n" +
                        $"> 赌上马儿性命的一战即将开始!\n" +
                        $"> 赛马场位置:{nowJoin}/{maxJoin}\n" +
                        $"> 你知道吗:{YouKnow.Get()}");
                    if (nowJoin >= maxJoin)
                    {
                        RaceStart.StartRaceInGroup(ctx, e.GroupId.ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                e.GroupReply(exception.Message);
            }
        }

        public CommonEventType EventType { get; set; } = CommonEventType.MessageGroup;
    }
}