using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Buffs;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Dlc.GoldRiver
{
    [HorseRaceEventGroup(GroupName = "黄金之河事件集v1")]
    public class GoldRiver
    {
        [HorseRaceEvent(EventName = "黄金之河_收集1")]
        public static void GoldRiver_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var number = RandomUtil.Number(5, 20);
            Util.AddGold(from, number);
            msgBuilder.AppendLine($"{from.GetDisplay()}于黄金的颂歌中寻得了{number}点闪烁");
        }

        [HorseRaceEvent(EventName = "黄金之河_收集2")]
        public static void GoldRiver_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var number = RandomUtil.Number(5, 15);
            Util.AddGold(from, number);
            msgBuilder.AppendLine($"{from.GetDisplay()}于醉意的音符中窥见了{number}点耀歌");
        }

        [HorseRaceEvent(EventName = "黄金之河_收集3")]
        public static void GoldRiver_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var number = RandomUtil.Number(10, 30);
            Util.AddGold(from, number);
            msgBuilder.AppendLine($"{from.GetDisplay()}于热情的歌喉中收藏了{number}点辉光");
        }

        [HorseRaceEvent(EventName = "黄金之河_收集4")]
        public static void GoldRiver_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var number = RandomUtil.Number(10, 20);
            Util.AddGold(from, number);
            msgBuilder.AppendLine($"{from.GetDisplay()}于欢快的舞步中留住了{number}点曙光");
        }

        [HorseRaceEvent(EventName = "黄金之河_收集5")]
        public static void GoldRiver_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var number = RandomUtil.Number(20, 50);
            Util.AddGold(from, number);
            msgBuilder.AppendLine($"{from.GetDisplay()}于黄金的河流上寻觅了{number}点星光");
        }

        [HorseRaceEvent(EventName = "黄金之河_兑换为步数")]
        public static void GoldRiver_6(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var ex = (int)Math.Floor(Util.GetGold(from) / 4.0);
            from.Step += randomStep + ex;
            Util.AddGold(from, -Util.GetGold(from));
            msgBuilder.AppendLine($"{from.GetDisplay()}向黄金之河贡献了所有的财富，获得了{ex}额外步数");
        }

        [HorseRaceEvent(EventName = "黄金之河_兑换为无敌")]
        public static void GoldRiver_7(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var ex = (int)Math.Floor(Util.GetGold(from) / 15.0);
            if (ex <= 0)
            {
                from.Step += randomStep;
                msgBuilder.AppendLine($"{from.GetDisplay()}在黄金之河洗澡，黄金之河什么都没有发生");
            }
            else
            {
                from.AddBuff(EssBuffs.InvincibleBuff, ex, true);
                Util.AddGold(from, -Util.GetGold(from));
                msgBuilder.AppendLine($"{from.GetDisplay()}向黄金之河贡献了所有的财富，获得了{ex}回合的<无敌>Buff");
            }
        }

        [HorseRaceEvent(EventName = "黄金之河_复活")]
        public static void GoldRiver_8(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            if (ground.Horses.Exists(x => x.Status == HorseStatus.Died))
            {
                var horse = ground.Horses.First(x => x.Status == HorseStatus.Died);
                horse.SetStatus(HorseStatus.None, true);
                Util.AddGold(from, -Util.GetGold(from));
                msgBuilder.AppendLine($"{from.GetDisplay()}在星空中探秘流淌的黄金之河，意外复活了{horse.GetDisplay()}");
            }
        }
    }
}