using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Buffs;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Events
{
    [HorseRaceEventGroup(GroupName = "芜湖集合")]
    public class WuHuEvents
    {
        [HorseRaceEvent(EventName = "超稳定芜湖_前进4")]
        public static void WuHu_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStep(from.GetStep() + randomStep + 3);
            msgBuilder.AppendLine($"{from.GetDisplay()} 得到了超稳定芜湖，快速额外前进了3格");
        }

        [HorseRaceEvent(EventName = "不稳定芜湖_前进-4-4")]
        public static void WuHu_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var random = RandomUtil.Number(-4, 4);
            from.SetStep(from.GetStep() + random);
            msgBuilder.AppendLine(
                $"{from.GetDisplay()} 得到了不稳定芜湖力量,{((random >= 0) ? "前进" : "后退")}了{Math.Abs(random)}格");
        }

        [HorseRaceEvent(EventName = "黑暗芜湖力量")]
        public static void WuHu_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.Horses[0].SetStatus(HorseStatus.Died);
            msgBuilder.AppendLine($"{from.GetDisplay()} 得到了黑暗芜湖力量，杀死了赛道1的马");
        }

        [HorseRaceEvent(EventName = "错误的芜湖力量")]
        public static void WuHu_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStatus(HorseStatus.Died, false);
            msgBuilder.AppendLine($"{from.GetDisplay()} 得到了错误的芜湖力量，杀死了自己");
        }

        [HorseRaceEvent(EventName = "至尊芜湖力量")]
        public static void WuHu_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            if (RandomUtil.Number(1, 10) >= 5)
            {
                ground.Horses.ForEach(s => s.SetStep(ground.AllLong));
                msgBuilder.AppendLine($"{from.GetDisplay()} 得到了金光闪闪至尊芜湖力量，让所有选手都到达了终点");
            }
            else
            {
                msgBuilder.AppendLine($"{from.GetDisplay()} 得到了金光闪闪至尊芜湖力量，但好像没有啥用");
            }
        }

        [HorseRaceEvent(EventName = "轻量芜湖力量")]
        public static void WuHu_6(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStep(from.GetStep() + randomStep + 1);
            msgBuilder.AppendLine($"{from.GetDisplay()} 得到了轻量芜湖力量，让自己额外前进1格");
        }


        [HorseRaceEvent(EventName = "彻彻底底芜湖力量")]
        public static void WuHu_7(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            if (from.AddBuff(EssBuffs.InvincibleBuff))
                msgBuilder.AppendLine($"{from.GetDisplay()} 得到了彻彻底底芜湖力量，获得了一回合的<无敌>状态");
            else
                msgBuilder.AppendLine($"{from.GetDisplay()} 得到了彻彻底底芜湖力量，但是好像被奇怪的力量阻止了");
        }

        [HorseRaceEvent(EventName = "隐秘芜湖力量")]
        public static void WuHu_8(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            if (from.AddBuff(EssBuffs.HideSelf, 2))
                msgBuilder.AppendLine($"{from.GetDisplay()} 得到了隐秘芜湖力量，获得了二回合的<隐身>状态");
            else
                msgBuilder.AppendLine($"{from.GetDisplay()} 得到了隐秘芜湖力量，但是好像被奇怪的力量阻止了");
        }

        [HorseRaceEvent(EventName = "芜湖法杖")]
        public static void WuHu_9(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.Horses.ForEach(x => x.AddBuff(EssBuffs.HideSelf));
            msgBuilder.AppendLine($"{from.GetDisplay()} 得到了芜湖法杖，让所有选手隐身了");
        }

        [HorseRaceEvent(EventName = "芜湖法杖·天启1")]
        public static void WuHu_10(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStatus(HorseStatus.Died, false);
            msgBuilder.AppendLine($"{from.GetDisplay()} 得到了<芜湖法杖·天启>，操作失误，毁灭了自己");
        }

        [HorseRaceEvent(EventName = "芜湖法杖·天启2")]
        public static void WuHu_11(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            if (RandomUtil.Number(0, 10) >= 8)
            {
                ground.Horses.Where(x => x != from).ToList().ForEach(x => x.SetStatus(HorseStatus.Died));
                msgBuilder.AppendLine($"{from.GetDisplay()} 得到了<芜湖法杖·天启>，起了杀心，尝试毁灭了所有的选手");
            }
            else
            {
                msgBuilder.AppendLine($"{from.GetDisplay()} 得到了<芜湖法杖·天启>，起了杀心，但是并不会操作");
            }
        }

        [HorseRaceEvent(EventName = "芜湖法杖·未来")]
        public static void WuHu_12(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.Horses.Where(x => x != from).ToList().ForEach(x => x.AddBuff(EssBuffs.TimeFrozen));
            msgBuilder.AppendLine($"{from.GetDisplay()} 得到了<芜湖法杖·未来>，停止了所有选手的时间!");

            
        }
        
        
        [HorseRaceEvent(EventName ="芜湖法杖·复苏")]
        public static void WuHu_13(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            foreach (var horse in ground.Horses.Where(x=>x.Status==HorseStatus.Died))
            {
                horse.Status = HorseStatus.None;
            }

            msgBuilder.AppendLine($"{from.GetDisplay()} 得到了<芜湖法杖·复苏>，复苏了所有的选手!");
        }
    }
}