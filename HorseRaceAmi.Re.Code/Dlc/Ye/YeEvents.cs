using System.Security.Cryptography;
using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Buffs;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Dlc.Ye
{
    [HorseRaceEventGroup(GroupName = "放课后！被赫尔雷普の叶子！")]
    public class YeEvents
    {
        [HorseRaceEvent(EventName = "叶子_1")]
        public static void Ye_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.GetDisplay()}被魅魔叶子控制住了!");
            msgBuilder.AppendLine($"{from.GetDisplay()}被撅晕过去了，直接退出比赛😫");
            from.SetStatus(HorseStatus.Left);
        }

        [HorseRaceEvent(EventName = "叶子_2")]
        public static void Ye_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.GetDisplay()}被魅魔赫尔控制住了!");
            msgBuilder.AppendLine($"赫尔逼问{from.GetDisplay()}叶子的位置，但是没有回答。");
            msgBuilder.AppendLine($"赫尔很生气，但是，于是，把{from.GetDisplay()}撅晕了(离开比赛)😅");
            from.SetStatus(HorseStatus.Left);
        }


        [HorseRaceEvent(EventName = "叶子_3")]
        public static void Ye_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var list = ground.Horses.ToArray().ToList();
            list.Sort((x, y) => -x.Step.CompareTo(y.Step));
            ground.Horses.Find(x => x == list.First()).Step = 0;
            msgBuilder.AppendLine($"{list.First()}不小心遇到了☹魅魔赫尔和😨魅魔叶子，两个恶魔不怀好意，把第一名送到了开始的地方。");
        }

        [HorseRaceEvent(EventName = "叶子de放课必杀")]
        public static void Ye_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.Horses.ForEach(x => x.SetStep(0, true));

            msgBuilder.AppendLine($"叶子被撅晕之后，穿越回到了过去！全场强制回归起点！");
        }

        [HorseRaceEvent(EventName = "叶子_跑团")]
        public static void Ye_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var r = RandomUtil.Number(0, 30);
            ground.Horses.ForEach(x => x.SetStep(r, true));
            msgBuilder.AppendLine($"叶子带大家跑团！全场强制穿梭到{r}格");
        }
    }
}