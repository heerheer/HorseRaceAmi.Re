using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Buffs;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Events.Creative
{
    [HorseRaceEventGroup(GroupName = "创意工坊2")]
    public class Creative2
    {
        [HorseRaceEvent(EventName = "熬夜加班")]
        public static void Creative2_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}熬夜加班，太累了，可能要睡一会。(By 辞镜)");
            from.AddBuff(EssBuffs.Dizz, 1);
        }

        [HorseRaceEvent(EventName = "屁股喷火")]
        public static void Creative2_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}xxx屁股后面喷出了火,加速了3格。(By ？？？)");
            from.Step += randomStep + 3;
        }

        [HorseRaceEvent(EventName = "二次元老婆")]
        public static void Creative2_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display} 途中看到二次元老婆，接下来3轮速度降低 (By 云帆·星辰)");
            from.Step += randomStep - 1;
        }

        [HorseRaceEvent(EventName = "核辐射")]
        public static void Creative2_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}参加东京奥运会，不小心受到核辐射，长出八条腿，接下来三轮比赛步数x2 (By 梦呓)");
            from.AddBuff(new ActionBuff((horse, raceGround) => { horse.Step += horse.TempStep; }, "<东京核辐射>"), 2);
        }

        [HorseRaceEvent(EventName = "摔倒在了地上")]
        public static void Creative2_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}不小心踩到了石头,摔倒在了地上，眩晕1回合 (By ？？？)");
            from.AddBuff(EssBuffs.Dizz, 1);
        }

        [HorseRaceEvent(EventName = "跑团")]
        public static void Creative2_6(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"kp喊{from.Display} 跑团，然而根本没有团，{from.Display}去把kp锤了一顿（随机进入一格，有小概率离开）(By 云帆·星辰)");
            if (RandomUtil.Number(1, 10) >= 7)
            {
                from.Step = RandomUtil.Number(0, ground.AllLong - 1);
            }
            else
            {
                from.SetStatus(HorseStatus.Left);
            }
        }

        [HorseRaceEvent(EventName = "化身莉莉")]
        public static void Creative2_7(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}化身莉莉，附近的队友害怕被误伤而远离（左右赛道的马在前方的加速，在后方的减速，同一位置被误杀）(By 云帆·星辰)");
            var indexOf = ground.Horses.IndexOf(@from);
            if (indexOf != 0)
            {
                var h = ground.Horses[indexOf - 1];
                if (h.Step == from.Step)
                {
                    h.SetStatus(HorseStatus.Died);
                }
                else
                {
                    h.Step += (h.Step >= from.Step) ? 1 : -1;
                }
            }

            if (indexOf != ground.Horses.Count)
            {
                var h = ground.Horses[indexOf + 1];
                if (h.Step == from.Step)
                {
                    h.SetStatus(HorseStatus.Died);
                }
                else
                {
                    h.Step += (h.Step >= from.Step) ? 1 : -1;
                }
            }
        }

        [HorseRaceEvent(EventName = "xx喊xxx去跑团")]
        public static void Creative2_8(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var list = ground.Horses.Where(x => x != from).ToList();
            var newh = list[RandomUtil.Number(0, list.Count - 1)];
            msgBuilder.AppendLine($"{from.Display}喊{newh.Display}去跑团,于是他们去跑团了。(By 吃贞子的栗子)");
            from.SetStatus(HorseStatus.Left);
            newh.SetStatus(HorseStatus.Left);
        }

        [HorseRaceEvent(EventName = "影阅响读")]
        public static void Creative2_9(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}发现事实证明汉字序顺并不影阅响读，所以随机进入一个位置(By 辞镜)");
            from.Step = RandomUtil.Number(0, ground.AllLong - 1);
        }

        [HorseRaceEvent(EventName = "虾政委")]
        public static void Creative2_10(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}你看到了远处的虾政委，收到coc的感召冲向前方（3回合加速）(By 云帆·星辰)");
            from.AddBuff(new ActionBuff((horse, raceGround) => { horse.Step += 1; }, "<coc的感召>"), 3);
        }

        [HorseRaceEvent(EventName = "兰羽·眼镜")]
        public static void Creative2_11(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}获得了兰羽特别赠送的一副眼镜，带上去之后，你的眼睛突然瞎了（3回合加速）(By 兰羽)");
            from.AddBuff(new ActionBuff((horse, raceGround) => { horse.Step -= 4; }, "<兰羽·眼镜>"), 3);
        }

        [HorseRaceEvent(EventName = "平行空间")]
        public static void Creative2_12(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}传送到了平行空间，现在有两个你(By 辞镜)");
            ground.Horses.Add(from.Clone());
        }

        [HorseRaceEvent(EventName = "大卡车")]
        public static void Creative2_13(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}被突然开进赛马场的一个大卡车撞飞了，虽然没受到伤害，但是你去法院控告了大卡车的司机，司机赔偿了你5000积分(By 兰羽)");
            DataUtil.AddCoin(from.OwnerQQ, 5000);
        }

        [HorseRaceEvent(EventName = "赛道1的诅咒")]
        public static void Creative2_14(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            if (ground.Horses.IndexOf(from) == 0)
            {
                msgBuilder.AppendLine($"{from.Display}:我将把这诅咒化为力量！你与随机一马交换赛道并直接使用暗黑芜湖之力(By 辞镜)");
                var list = ground.Horses.Where(x => x != from).ToList();
                var newh = RandomUtil.Number(0, list.Count - 1);

                var race1 = ground.Horses[0].Clone();
                var race2 = ground.Horses[newh].Clone();

                ground.Horses[newh] = race1;
                ground.Horses[0] = race2;

                ground.Horses[0].SetStatus(HorseStatus.Died);
                msgBuilder.AppendLine($"{from.GetDisplay()}将诅咒化为力量，杀死了新的赛道1的马({race2.Display})");
            }
        }

        [HorseRaceEvent(EventName = "恋骰")]
        public static void Creative2_15(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display} 你看见了一旁的辞镜，她很可爱所以你去恋骰了（离开）(By 云帆·星辰)");
            from.SetStatus(HorseStatus.Left);
        }

        [HorseRaceEvent(EventName = "量子之马")]
        public static void Creative2_16(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}被薛定谔选中了(By 云帆·星辰)");
            from.AddBuff(new ActionBuff((horse, raceGround) => { }, "<量子之马><死亡><未死亡><叠加>"), 10);
        }

        [HorseRaceEvent(EventName = "败者食尘")]
        public static void Creative2_17(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}发动了技能:败者食尘！无需多讲 (By 辞镜)");
        }

        [HorseRaceEvent(EventName = "Creeper")]
        public static void Creative2_18(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display} 一只Creeper在追你，你获得加速，但概率死亡(By 云帆·星辰)");
            from.AddBuff(new ActionBuff((horse, raceGround) =>
            {
                if (RandomUtil.Number(1, 10) >= 6)
                {
                    horse.SetStatus(HorseStatus.Died);
                }
                else
                {
                    horse.Step += 1;
                }
            }, "<恐惧奔跑"), 10);
        }

        [HorseRaceEvent(EventName = "误入Bug")]
        public static void Creative2_19(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}碰到了赫尔的bug区块，掉进了不存在的bug领域 (By 吃贞子的栗子)");
            from.SetStatus(HorseStatus.Died);
        }

        [HorseRaceEvent(EventName = "黑星照常升起")]
        public static void Creative2_20(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}愿黑星照常升起：复活所有玩家并将不具有<无敌>的人送回起点 (By 云帆·星辰)");
            ground.Horses.ForEach(x => x.SetStatus(HorseStatus.None));
            ground.Horses.ForEach(x =>
            {
                if (x.Buffs.Exists(x => x.Buff == EssBuffs.InvincibleBuff) is false)
                {
                    x.Step = 0;
                }
            });
        }

        [HorseRaceEvent(EventName = "换个视角")]
        public static void Creative2_21(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}换个视角？赫尔歪了歪头从另一个角度观察赛场发现这样看就只有一个赛道了！（所有玩家统一变成一个位置）(By 辞镜)");
            ground.Horses.ForEach(x => x.SetStep(RandomUtil.Number(0, ground.AllLong - 1), true));
        }
        
        [HorseRaceEvent(EventName = "这是一场交易")]
        public static void Creative2_22(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}:这是一场交易？你把你的马卖了换取了5000积分 (By 辞镜)");
            from.SetStatus(HorseStatus.Left);
            DataUtil.AddCoin(from.OwnerQQ, 5000);
        }
        [HorseRaceEvent(EventName = "这把不算")]
        public static void Creative2_23(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}:嗯..我是说，这把不算。（强制返回起点洗掉buff开始比赛） (By 辞镜)");
            from.SetStep(0,false);
            from.Buffs.Clear();
        }
        [HorseRaceEvent(EventName = "信标")]
        public static void Creative2_24(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.Display}:在原地留下一个信标，若死亡则从新标处免费复活,但是信标这个版本还没出。(By 辞镜)");
            
        }
    }
}