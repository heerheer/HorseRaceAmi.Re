using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Buffs;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Events
{
    [HorseRaceEventGroup(GroupName = "昏睡红茶事件集v1")]
    public class Me114514
    {
        [HorseRaceEvent(EventName = "昏睡红茶_即时版")]
        public static void Me114514_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var random = RandomUtil.Number(1, 3);
            from.SetStep(from.GetStep() - random);
            msgBuilder.AppendLine($"{from.GetDisplay()} 被喝了一口昏睡红茶，觉得精神百倍，后退了{random}步");
        }

        [HorseRaceEvent(EventName = "昏睡红茶_立刻昏睡")]
        public static void Me114514_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(EssBuffs.Dizz);
            msgBuilder.AppendLine($"{from.GetDisplay()} 被喝了一口昏睡红茶,晕了过去");
        }
        

        [HorseRaceEvent(EventName = "先辈的追忆1")]
        public static void Me114514_m1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Step += 2;
            msgBuilder.AppendLine($"无数的记忆让{from.GetDisplay()}回忆起了先辈，于是加快了脚步");
        }

        [HorseRaceEvent(EventName = "先辈的追忆2")]
        public static void Me114514_m2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.GetDisplay()}触发了隐藏剧情:你知道吗，凭借这个消息，你可以找赫尔领取一个野兽先辈");
        }

        [HorseRaceEvent(EventName = "先辈的追忆3")]
        public static void Me114514_m3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.Horses.Where(x => x != from).ToList().ForEach(x => x.AddBuff(EssBuffs.Dizz_114514));
            msgBuilder.AppendLine($"{from.GetDisplay()}制作了一杯昏睡红茶，迷晕了在场所有的小伙伴");
        }

        [HorseRaceEvent(EventName = "先辈的追忆4")]
        public static void Me114514_m4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            if (from.Buffs.Exists(x => x.Buff == EssBuffs.Dizz_114514 && x.LeftTime >= 1))
            {
                msgBuilder.AppendLine($"{from.GetDisplay()}制作了昏睡红茶解药，解除了昏睡红茶状态");
            }
            else
            {
                msgBuilder.AppendLine($"{from.GetDisplay()}制作了昏睡红茶解药，然后倒在了河里也不给需要的人。");
            }
        }
    }
}