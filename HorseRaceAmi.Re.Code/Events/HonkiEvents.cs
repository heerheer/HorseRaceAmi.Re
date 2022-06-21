using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Buffs;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Events
{
    [HorseRaceEventGroup(GroupName = "崩坏集合v1")]
    public class HonkiEvents
    {
        [HorseRaceEvent(EventName = "崩坏能侵蚀_退1-3")]
        public static void Honki_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var random = RandomUtil.Number(1, 3);
            from.SetStep(from.GetStep() - random);
            msgBuilder.AppendLine($"{from.GetDisplay()} 被崩坏能侵蚀，丧失理智，后退了{random}步");
        }

        [HorseRaceEvent(EventName = "崩坏能侵蚀2_退4")]
        public static void Honki_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStep(from.GetStep() - 4);
            msgBuilder.AppendLine($"{from.GetDisplay()} 被崩坏能侵蚀，丧失自我，后退了{4}步");
        }

        [HorseRaceEvent(EventName = "崩坏能侵蚀3_眩晕")]
        public static void Honki_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(EssBuffs.Dizz);
            msgBuilder.AppendLine($"{from.GetDisplay()} 触碰到了大量的崩坏能，晕了过去");
        }

        [HorseRaceEvent(EventName = "崩坏能侵蚀4_退回起点")]
        public static void Honki_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStep(0);
            msgBuilder.AppendLine($"{from.GetDisplay()} 看了一眼西琳，被崩坏能送回了起点");
        }

        [HorseRaceEvent(EventName = "崩坏能侵蚀5_场地受损")]
        public static void Honki_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.SetData("Honki", (int)(ground.GetData("Honki")??0) + 1);
            msgBuilder.AppendLine($"{from.GetDisplay()} 引起了崩坏能泄露，场地现在存在{ground.GetData("Honki")}点散溢崩坏能");
        }
    }
}