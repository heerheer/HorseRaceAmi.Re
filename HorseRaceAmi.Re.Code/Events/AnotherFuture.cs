using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Events
{
    [HorseRaceEventGroup(GroupName = "另一个未来事件集")]
    public class AnotherFuture
    {
        //TODO 花火花火丶
        //一个马死另一个马也死
        [HorseRaceEvent(EventName = "另一个未来_绑定")]
        public static void Af_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var list = ground.Horses.Where(x => x != from).ToList();
            var newh = list[RandomUtil.Number(0, list.Count - 1)];
            from.AddBuff(new HuaHuo(newh));
            newh.AddBuff(new HuaHuo(from));
            msgBuilder.AppendLine(
                $"花火花火丶觉得{from.GetDisplay()}和{newh.GetDisplay()}很般配，赋予了他们丶花火丶状态");
            msgBuilder.AppendLine(
                $"丶花火丶: 死亡/复活/离开 会绑定两人的状态。");
        }
        //一个马带着另一个马离开了赛场

        //一个马受伤另一个马也受伤

        //一个马赢另一个也赢

        public static void Af_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            //彩蛋:767445721
            if (from.OwnerQQ == "767445721")
            {
                msgBuilder.AppendLine(
                    $"[专属彩蛋]{from.GetDisplay()}听说自己的主人要透赫尔，非常开心，激动地透死了两边赛道的选手!");
                msgBuilder.AppendLine(
                    $"[专属彩蛋]{from.GetDisplay()}但是还没透完，就发现自己的钱不翼而飞了。");
                var index = ground.Horses.IndexOf(from);
                if (ground.Horses.Count > index + 1)
                {
                    ground.Horses[index + 1].SetStatus(HorseStatus.Died);
                }

                if (index != 0)
                {
                    ground.Horses[index - 1].SetStatus(HorseStatus.Died);
                }
            }
        }

        [HorseRaceEvent(EventName = "另一个未来_喷射")]
        public static void Af_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(new LanYu());
            msgBuilder.AppendLine(
                $"{from.GetDisplay()}肚子疼，得到了<兰羽·喷射>状态");
            msgBuilder.AppendLine(
                $"<兰羽·喷射>:每回合有概率忍住喷射，额外加速前进2格。也有概率直接忍不住退场。");
        }

        [HorseRaceEvent(EventName = "另一个未来_我们终将免费")]
        public static void Af_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Status = HorseStatus.Left;
            msgBuilder.AppendLine(
                $"{from.Display}大喊:我们免费了!! 离开了赛场");
        }

        [HorseRaceEvent(EventName = "另一个未来_陨石")]
        public static void Af_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.Buffs.Add(new(new BoomBoom(), 3));
            msgBuilder.AppendLine("场地遭受了3回合陨石袭击，每个回合都有赛道会被陨石撞击导致马儿死亡。");
        }
    }

    public class HuaHuo : BuffBase
    {
        private Horse _horse;

        public HuaHuo(Horse horse)
        {
            _horse = horse;
        }

        public override string Prefix()
            =>
                "丶花火丶";

        public override bool OnStatusSet(Horse horse, HorseStatus status)
        {
            _horse.SetStatus(status, true);
            return true;
        }
    }

    public class LanYu : BuffBase
    {
        public override string Prefix()
        {
            return "<兰羽·喷射>";
        }

        public override void OnRoundEnded(Horse horse, RaceGround ground)
        {
            if (RandomUtil.Number(1, 2) == 1)
            {
                //忍住了
                horse.Step += 2;
            }
            else
            {
                horse.Status = HorseStatus.Left;
            }
        }
    }

    public class BoomBoom : BuffBase
    {
        public override string Prefix()
        {
            return "";
        }

        public override void OnBeforeRoundBegin(Horse horse, RaceGround ground)
        {
            ground.Horses[RandomUtil.Number(0, ground.Horses.Count - 1)].Status = HorseStatus.Died;
        }
    }
}