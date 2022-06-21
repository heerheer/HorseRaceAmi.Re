using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Buffs;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Events
{
    [HorseRaceEventGroup(GroupName = "开发者赫尔事件集v1")]
    public class DeveloperHeer
    {
        [HorseRaceEvent(EventName = "刷新场地数据")]
        public static void Heer_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.RefreshBuffs();
            msgBuilder.AppendLine(
                $"赫尔乱入比赛，使用神秘的力量刷新了场地的数据，场地中的延迟事件的时间-1");
        }

        [HorseRaceEvent(EventName = "刷新选手数据")]
        public static void Heer_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.RefreshBuffs();
            msgBuilder.AppendLine(
                $"赫尔随手一点，用神秘力量加快了{from.GetDisplay()}的Buffs速度，所有的效果持续时间-1");
        }

        [HorseRaceEvent(EventName = "更改选手眩晕Buff到无敌Buff")]
        public static void Heer_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var index = from.Buffs.FindIndex(x => (x.Buff == EssBuffs.Dizz || x.Buff == EssBuffs.Dizz_114514));
            if (index != -1)
            {
                var buff = from.Buffs[index];
                from.Buffs[index] = new(EssBuffs.InvincibleBuff, buff.LeftTime);
                msgBuilder.AppendLine(
                    $"赫尔看到了有趣的事情，用神秘力量改写了{from.GetDisplay()}的{buff.Buff.Prefix()}Buff，转换为了<无敌>Buff");
            }
        }


        [HorseRaceEvent(EventName = "赋予选手眩晕Buff")]
        public static void Heer_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(EssBuffs.Dizz, 3, true);
            msgBuilder.AppendLine(
                $"赫尔找不到男朋友也找不到女朋友，非常生气，随便给了{from.GetDisplay()}一个3回合的眩晕Buff(强制更改)");
        }

        [HorseRaceEvent(EventName = "赋予选手无敌Buff")]
        public static void Heer_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(EssBuffs.InvincibleBuff, 999, true);
            msgBuilder.AppendLine(
                $"赫尔找到了络腮胡白袜壮熊，非常开心，随便给了{from.GetDisplay()}一个999回合的无敌Buff(强制更改)");
        }

        [HorseRaceEvent(EventName = "赋予所有选手无敌Buff")]
        public static void Heer_6(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.Horses.ForEach(x => x.AddBuff(EssBuffs.InvincibleBuff, 1, true));
            msgBuilder.AppendLine(
                $"赫尔找到了眼镜中二异瞳腐女，非常开心，给了所有选手一回合的无敌Buff(强制更改)");
        }

        [HorseRaceEvent(EventName = "改变赛道")]
        public static void Heer_7(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var random = RandomUtil.Number(2, ground.Horses.Count);

            var race1 = ground.Horses[0].Clone();
            var race2 = ground.Horses[random - 1].Clone();

            ground.Horses[random - 1] = race1;
            ground.Horses[0] = race2;

            msgBuilder.AppendLine(
                $"赫尔想要调戏一下选手，强行改变了1号和 {random}号的赛道");
        }

        [HorseRaceEvent(EventName = "我将!扭转万象!")]
        public static bool Heer_8(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            if (RandomUtil.Number(0, 10) > 6)
                return false;
            ground.Horses.ForEach(x => x.SetStep(ground.AllLong - x.Step, true));
            msgBuilder.AppendLine(
                $"赫尔因为抽不出识宝，气愤的大喊，我将!!扭转万象!!(所有选手位置改变且直接进入下一回合)");
            return true;
        }

        [HorseRaceEvent(EventName = "我将!扭转一切!")]
        public static bool Heer_9(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            ground.Horses.ForEach(x => x.SetStatus(HorseStatus.None, true));
            ground.Horses.ForEach(x => x.AddBuff(EssBuffs.InvincibleBuff, 1));
            msgBuilder.AppendLine(
                $"赫尔抽出了识宝，开心的大喊，我将!!扭转一切!!(所有选手复活且赋予1回合无敌)");
            return true;
        }
    }
}