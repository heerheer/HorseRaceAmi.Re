using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Events.Creative
{
    [HorseRaceEventGroup(GroupName = "创意工坊")]
    public class CreativeThis
    {
        [HorseRaceEvent(EventName = "天灾马")]
        public static void Creative_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}变成了天灾之马，杀死了一个跑的比他慢的");
            from.Display = "~天灾~";
            ground.Horses.Find(x => x.Step < from.Step)?.SetStatus(HorseStatus.Died);
        }

        [HorseRaceEvent(EventName = "回家吃饭")]
        public static void Creative_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Step = 0;
            msgBuilder.AppendLine($"{from.Display}的主人喊他回家吃饭~");
        }

        //轮椅战胜
        public class Wheelchair : BuffBase
        {
            public override string Prefix()
            {
                return "轮椅战神";
            }

            public override void OnAfterRoundEnded(Horse horse, RaceGround ground)
            {
                horse.Step = 1;
            }

            public override bool CanBeInfluence()
            {
                return false;
            }
        }

        [HorseRaceEvent(EventName = "轮椅战神")]
        public static void Creative_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(new Wheelchair(), 2);
            msgBuilder.AppendLine($"{from.Display}获得了两回合的<轮椅战神>");
            msgBuilder.AppendLine($"轮椅战神，无敌但是每回合固定走一格");
        }

        [HorseRaceEvent(EventName = "核弹来袭")]
        public static void Creative_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.Horses.ForEach(x =>
            {
                if (RandomUtil.Number(1, 10) <= 3)
                {
                    from.Display = @"@s#+\/";
                    msgBuilder.AppendLine($"{x.Display}在核弹的威力中发生了变异!");
                }
                else
                {
                    x.SetStatus(HorseStatus.Died);
                    msgBuilder.AppendLine($"{x.Display}在核弹的威力中变成了烤马肉");
                }
            });
        }

        [HorseRaceEvent(EventName = "赫尔不死于bug")]
        public static void Creative_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}获得了三回合的<赫尔不死于bug>");
            msgBuilder.AppendLine($"赫尔不死于bug:没有任何效果，因为赫尔懒得写新Buff");
        }

        [HorseRaceEvent(EventName = "爆炸0")]
        public static void Creative_6(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Step += 1;
            msgBuilder.AppendLine($"{from.Display}变成了爆炸0!");
        }
    }
}