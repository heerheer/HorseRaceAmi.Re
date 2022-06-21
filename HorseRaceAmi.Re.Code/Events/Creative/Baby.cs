using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Events.Creative
{
    [HorseRaceEventGroup(GroupName = "繁殖-黎鸣")]
    public class Baby
    {
        private static ActionBuff _actionBuff = new ActionBuff(
            action: (h, g) => { h.Step = 0; },
            "<繁衍中>"
        );

        [HorseRaceEvent(EventName = "繁殖1")]
        public static void Baby_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(_actionBuff, 3);
            msgBuilder.AppendLine($"{from.Display} 正在生孩子!(持续3回合) 繁衍DLC-By黎老板");
        }

        [HorseRaceEvent(EventName = "繁殖2")]
        public static void Baby_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            randomStep = 0;
            msgBuilder.AppendLine($"{from.Display} 正在努力把孩子生出来!! 繁衍DLC-By黎老板");
        }

        [HorseRaceEvent(EventName = "繁殖3")]
        public static void Baby_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.Horses.Add(new Horse() { Display = "天灾马", Step = from.Step, OwnerQQ = "114514" });
            from.SetStatus(HorseStatus.Died);
            msgBuilder.AppendLine($"{from.Display}生了个天灾马，还没来得及反应就被自己的孩子吃掉了 繁衍DLC-By黎老板");
        }

        [HorseRaceEvent(EventName = "繁殖4")]
        public static void Baby_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var list = ground.Horses.Where(x => x != from).ToList();
            var h = list[RandomUtil.Number(0, list.Count - 1)];
            from.Step = 0;
            h.Step = 0;
            msgBuilder.AppendLine($"{from.Display}和{h.Display}一起开始生孩子!!!约定好一起回到了起点!! 繁衍DLC-By黎老板");
        }
        [HorseRaceEvent(EventName = "繁殖5")]
        public static void Baby_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var list = ground.Horses.Where(x => x != from).ToList();
            var h = list[RandomUtil.Number(0, list.Count - 1)];
            from.Step = 0;
            h.Step = 0;
            msgBuilder.AppendLine($"{from.Display}和{h.Display}一起开始生孩子!!!约定好一起回到了起点!! 繁衍DLC-By黎老板");
        }
        [HorseRaceEvent(EventName = "繁殖6")]
        public static void Baby_6(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStatus(HorseStatus.Left);
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(20 * 1000);
                from.SetStatus(HorseStatus.None);

            });
            msgBuilder.AppendLine($"{from.Display}先去生孩子了，20秒后回归赛场。");
        }
    }
}