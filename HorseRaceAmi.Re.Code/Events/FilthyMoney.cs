using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Events
{
    [HorseRaceEventGroup(GroupName = "不义之财事件集")]
    public class FilthyMoney
    {
        [HorseRaceEvent(EventName = "不义之财_牙签")]
        public static void Fm_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var random = RandomUtil.Number(1, 3);
            from.SetStep(from.GetStep() - random);
            msgBuilder.AppendLine($"{from.GetDisplay()} 捡到了一盒牙签，本来想拿走，结果发现是吴签，后退了{random}步");
        }

        [HorseRaceEvent(EventName = "不义之财_钱包")]
        public static void Fm_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var random = RandomUtil.Number(1000, 4000);
            msgBuilder.AppendLine($"{from.GetDisplay()} 捡到了一个钱包，获得了{random}小马积分");
            DataUtil.AddCoin(from.OwnerQQ, random);
        }

        [HorseRaceEvent(EventName = "不义之财_大钱包")]
        public static void Fm_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var random = RandomUtil.Number(20000, 40000);
            msgBuilder.AppendLine($"{from.GetDisplay()} 捡到了一个大钱包，获得了{random}小马积分");

            DataUtil.AddCoin(from.OwnerQQ, random);
        }

        [HorseRaceEvent(EventName = "不义之财_被抓")]
        public static void Fm_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var random = RandomUtil.Number(1000, 2000);
            msgBuilder.AppendLine($"{from.GetDisplay()} 捡到了一个钱包，获得了{random}小马积分");
            msgBuilder.AppendLine($"{from.GetDisplay()} 还没来得及溜走，就被抓住了!被遣送回起点!");
            from.SetStep(0, true);
            DataUtil.AddCoin(from.OwnerQQ, random);
        }


        [HorseRaceEvent(EventName = "不义之财_诈骗")]
        public static void Fm_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var list = ground.Horses.Where(x => x != from).ToList();
            var newh = list[RandomUtil.Number(0, list.Count - 1)];
            msgBuilder.AppendLine($"{from.GetDisplay()} 通过诈骗获得了{newh.GetDisplay()}的5000小马积分!!");
            from.SetStep(0, true);
            DataUtil.AddCoin(from.OwnerQQ, 5000);
            DataUtil.AddCoin(newh.OwnerQQ, -5000);
        }
    }
}