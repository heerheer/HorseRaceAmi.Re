using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Dlc.BugHeer
{
    [HorseRaceEventGroup(GroupName = "Bug生产者赫尔v1")]
    public class BugHeer
    {
        [HorseRaceEvent(EventName = "生产Bug_1")]
        public static void BugHeer_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            Util.AddBug(ground, 10);
            msgBuilder.AppendLine($"{from.GetDisplay()}触发了♬赫尔♬留下的Bug陷阱，场地Bug+10");
        }

        [HorseRaceEvent(EventName = "生产Bug_2")]
        public static void BugHeer_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(new HeerBugBuff(), 2);
            msgBuilder.AppendLine($"{from.GetDisplay()}不小心开启了♬赫尔♬忘记删掉的机关,获得了2回合<赫尔的Bug>状态");
            msgBuilder.AppendLine($"<赫尔的Bug>:每回合场地Bug数随机增加5-10");
        }

        [HorseRaceEvent(EventName = "清除Bug_1")]
        public static void BugHeer_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var bug = Util.GetBug(ground);
            msgBuilder.AppendLine($"{from.GetDisplay()}不小心按下了♬赫尔♬的召唤器,召唤出了赫尔。");
            if (bug >= 30)
            {
                msgBuilder.AppendLine($"♬赫尔♬觉得场地上{bug}个Bug太多了,清理掉了Bug");
                msgBuilder.AppendLine($"♬赫尔♬同时给了在场所有人Bug补偿，{bug}x{50}={bug * 10}小马积分");
                ground.Horses.ForEach(x => DataUtil.AddCoin(x.OwnerQQ, bug * 10));
            }
            else
            {
                msgBuilder.AppendLine($"觉得场地上{bug}个Bug还不算多,直接溜了。");
            }
        }

        [HorseRaceEvent(EventName = "清除Bug_2")]
        public static void BugHeer_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            Util.AddBug(ground, 20);
            msgBuilder.AppendLine($"{from.GetDisplay()}不小心按下了♬赫尔♬的召唤器,召唤出了赫尔。");

            msgBuilder.AppendLine($"♬赫尔♬觉得场地上个Bug还不够多了,赐予了场地20个Bug");
        }

        [HorseRaceEvent(EventName = "清除Bug_3")]
        public static void BugHeer_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            Util.AddBug(ground, 20);
            msgBuilder.AppendLine($"{from.GetDisplay()}不小心按下了♬赫尔♬的召唤器,召唤出了赫尔。");

            msgBuilder.AppendLine($"♬赫尔♬觉得场地上个Bug还不够多了,赐予了场地20个Bug");
        }

        [HorseRaceEvent(EventName = "不小心的Bug")]
        public static void BugHeer_6(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(new JustHeer(), 2);
            msgBuilder.AppendLine($"{from.GetDisplay()}不小心开启了♬赫尔♬忘记删掉的机关,获得了2回合<只是赫尔>状态");
            msgBuilder.AppendLine($"<只是赫尔>:每回合有概率无法移动XD");
        }

        [HorseRaceEvent(EventName = "不小心的Bug_2")]
        public static void BugHeer_7(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(new NotJustHeer(), 2);
            msgBuilder.AppendLine($"{from.GetDisplay()}不小心开启了♬赫尔♬忘记删掉的机关,获得了2回合<不只是赫尔>状态");
            msgBuilder.AppendLine($"<不只是赫尔>:每回合步数锁定为前进2");
        }

        [HorseRaceEvent(EventName = "最后的Bug")]
        public static void BugHeer_8(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.Horses.ForEach(x => x.AddBuff(new JustHeer(), 1));
            msgBuilder.AppendLine($"{from.GetDisplay()}使用了X<超级赫尔召唤器·最后的离别之歌>X，召唤了♬赫尔♬");
            msgBuilder.AppendLine($"♬赫尔♬赋予了所有人1回合的<只是赫尔>状态");
            msgBuilder.AppendLine($"<只是赫尔>:每回合有概率无法移动XD");
        }
    }
}