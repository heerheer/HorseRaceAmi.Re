using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.Re.Code.Events
{
    [HorseRaceEventGroup(GroupName = "复刻经典事件集合v1")]
    public class OldEvents
    {
        [HorseRaceEvent(EventName = "空间跳跃_前进3")]
        public static void Old_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStep(from.GetStep() + randomStep + 3);
            msgBuilder.AppendLine($"{from.GetDisplay()} 获得了白井黑子的力量，往前空间跳跃了3格!");
        }
        [HorseRaceEvent(EventName = "曲率加速_前进3")]
        public static void Old_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStep(from.GetStep() + randomStep + 3);
            msgBuilder.AppendLine($"{from.GetDisplay()} 找到了一个曲率加速器，加速前进了3格");
        }
        [HorseRaceEvent(EventName = "火箭推进器_前进3")]
        public static void Old_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStep(from.GetStep() + randomStep + 3);
            msgBuilder.AppendLine($"{from.GetDisplay()} 找到了一个火箭推进器，加速前进了3格");
        }

        [HorseRaceEvent(EventName = "沉迷PHub_后退2")]
        public static void Old_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStep(from.GetStep() - 2);
            msgBuilder.AppendLine($"{from.GetDisplay()} 沉迷PHub，后退了2格");
        }

        [HorseRaceEvent(EventName = "飞机击中")]
        public static void Old_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            if (from.SetStatus(HorseStatus.Died))
                msgBuilder.AppendLine($"{from.GetDisplay()} 被天上的飞机击中了，不幸离开了战场");
            else
                msgBuilder.AppendLine($"{from.GetDisplay()} 被天上的飞机击中了，当并没有发生什么事");

        }

        [HorseRaceEvent(EventName = "心脏病")]
        public static void Old_6(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            if (from.SetStatus(HorseStatus.Died))
                msgBuilder.AppendLine($"{from.GetDisplay()}突发心脏病，不幸离开了战场");
            else
                msgBuilder.AppendLine($"{from.GetDisplay()}突发心脏病，但是找到了药物，活了过来。");

        }
        [HorseRaceEvent(EventName = "掉入二次元_后退2")]
        public static void Old_7(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStep(from.GetStep() - 2);
            msgBuilder.AppendLine($"{from.GetDisplay()} 掉入了二次元，沉迷其中，无法自拔，后退了2格");

        }
        [HorseRaceEvent(EventName = "脚艺马_前进2")]
        public static void Old_8(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStep(from.GetStep() + randomStep + 2);
            msgBuilder.AppendLine($"{from.GetDisplay()} 成为了脚艺马，额外前进了2格");
        }
        [HorseRaceEvent(EventName = "我命有我不由天")]
        public static void Old_9(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            if (from.SetStep(0))
                msgBuilder.AppendLine($"{from.GetDisplay()} 觉得我命有我不由天，快速冲到了起点");
            else
                msgBuilder.AppendLine($"{from.GetDisplay()} 觉得我命有我不由天，但是并没人理他");
        }
        [HorseRaceEvent(EventName = "诡计")]
        public static void Old_10(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.Horses.Where(x=>x!=from).ToList().ForEach(x => x.Step = 0);
            msgBuilder.AppendLine($"{from.GetDisplay()} 智商增加了，施了诡计使得除了他之外的选手退回了起点!");
        }
    }
}
