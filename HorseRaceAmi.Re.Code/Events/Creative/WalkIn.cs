using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Buffs;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Events.Creative
{
    [HorseRaceEventGroup(GroupName = "夺舍")]
    public class WalkIn
    {
        [HorseRaceEvent(EventName = "夺舍")]
        public static void WalkIn_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var list = ground.Horses.Where(x => x != from).ToList();
            var h = list[RandomUtil.Number(0, list.Count - 1)];
            h.OwnerQQ = from.OwnerQQ;
            msgBuilder.AppendLine($"{from.Display}夺舍了{h.Display}。");
        }

        [HorseRaceEvent(EventName = "夺舍2")]
        public static void WalkIn_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var list = ground.Horses.Where(x => x != from).ToList();
            var h = list[RandomUtil.Number(0, list.Count - 1)];
            from.AddBuff(EssBuffs.Dizz, 1);
            msgBuilder.AppendLine($"{from.Display}尝试夺舍{h.Display}。但是夺舍失败，被打晕了");
        }
        [HorseRaceEvent(EventName = "夺舍3")]
        public static void WalkIn_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var list = ground.Horses.Where(x => x != from).ToList();
            var h = list[RandomUtil.Number(0, list.Count - 1)];
            from.SetStatus(HorseStatus.Died);
            msgBuilder.AppendLine($"{from.Display}尝试夺舍{h.Display}。但是夺舍失败，魂飞魄散");
        }
        [HorseRaceEvent(EventName = "夺舍4")]
        public static void WalkIn_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var list = ground.Horses.Where(x => x != from).ToList();
            var h = list[RandomUtil.Number(0, list.Count - 1)];
            from.Step += h.Step;
            h.Step = 0;
            msgBuilder.AppendLine($"{from.Display}尝试夺舍{h.Display}。不仅夺舍成功，甚至直接将Ta的步数都贡献给了本体。");
        }
    }
}