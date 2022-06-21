using System.Text;
using HorseRaceAmi.Re.Code.Events.Creative;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Dlc.Dice
{
    [HorseRaceEventGroup(GroupName = "恋爱骰娘")]
    public class DiceLove
    {
        [HorseRaceEvent(EventName = "恋爱骰娘_1")]
        public static void DiceLove_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var number = RandomUtil.Number(1, 100);
            msgBuilder.AppendLine($"{from.GetDisplay()}进行了.r!");
            if (number <= 10)
            {
                msgBuilder.AppendLine($"1D100={number}!{from.GetDisplay()}和绿箭恋骰去了~muamua");
            }

            msgBuilder.AppendLine($"1D100={number}!{from.GetDisplay()}暗恋绿箭中。");
        }

        [HorseRaceEvent(EventName = "恋爱骰娘_2")]
        public static void DiceLove_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var number = RandomUtil.Number(1, 100);
            msgBuilder.AppendLine($"{from.GetDisplay()}进行了.r!");
            if (number <= 10)
            {
                msgBuilder.AppendLine($"1D100={number}!{from.GetDisplay()}和辞镜恋骰去了~muamua");
            }

            msgBuilder.AppendLine($"1D100={number}!{from.GetDisplay()}暗恋辞镜中。");
        }

        [HorseRaceEvent(EventName = "恋爱骰娘_3")]
        public static void DiceLove_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var number = RandomUtil.Number(1, 44);
            msgBuilder.AppendLine($"{from.GetDisplay()}进行了.rd44!");
            if (number <= 8)
            {
                msgBuilder.AppendLine($"1D44={number}!{from.GetDisplay()}发现自己暗恋的对象竟然是ZhaoDice搭建的骰娘!非常生气");
            }
            else
            {
                msgBuilder.AppendLine($"1D44={number}!{from.GetDisplay()}发现自己暗恋的对象竟然是OlivsDice搭建的骰娘!非常生气");
            }
        }

        [HorseRaceEvent(EventName = "恋爱骰娘_4")]
        public static void DiceLove_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var number = RandomUtil.Number(1, 55);
            msgBuilder.AppendLine($"{from.GetDisplay()}进行了.rd55!");
            if (number <= 9)
            {
                msgBuilder.AppendLine($"1D55={number}!{from.GetDisplay()}加速追赶前面的爱宕！前进了6步");
            }
            else
            {
                msgBuilder.AppendLine($"1D55={number}!{from.GetDisplay()}大失败。");
            }
        }

        [HorseRaceEvent(EventName = "恋爱骰娘_5")]
        public static void DiceLove_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var number = RandomUtil.Number(1, 999);
            msgBuilder.AppendLine($"{from.GetDisplay()}进行了.rd999!\n1D999={number}!");
            if (number <= 9)
            {
                from.Step += 6;
                msgBuilder.AppendLine($"{from.GetDisplay()}加速追赶前面的爱宕！前进了6步");
            }
            else
            {
                msgBuilder.AppendLine($"{from.GetDisplay()}大失败。");
            }
        }

        [HorseRaceEvent(EventName = "恋爱骰娘_6")]
        public static void DiceLove_6(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(new ActionBuff((x, y) =>
            {
                var number = RandomUtil.Number(1, 10);
                if (number <= 5)
                {
                    from.Step -= 1;
                }
                else if (number <= 9)
                {
                    from.Step += 1;
                }
                else
                {
                    from.Step += 5;
                }
            }, "恋骰"), 5);

            msgBuilder.AppendLine($"{from.GetDisplay()}玩骰娘太多了，魔怔了，进入了5回合的<恋骰>状态。");
            msgBuilder.AppendLine($"<恋骰>：持续中每回合进行.rd10,如果<=5，则后退一步，若>=5&<=9则前进一步，若为10，则前进5步。");
        }

        [HorseRaceEvent(EventName = "恋爱骰娘_7")]
        public static void DiceLove_7(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(new ActionBuff((x, y) =>
            {
                var number = RandomUtil.Number(1, 100);

                if (number != 99)
                {
                    from.Step = 0;
                }
                else
                {
                    from.Step += 999;
                }
            }, "骰娘化"), 5);

            msgBuilder.AppendLine($"{from.GetDisplay()}玩骰娘太多了，认为自己也是骰娘，进入了5回合的<骰娘化>状态。");
            msgBuilder.AppendLine($"<骰娘化>：持续中每回合进行.rd100,如果不等于99，则会退回起点。若等于100，则直接前进999步。");
        }
    }
}