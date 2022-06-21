using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Buffs;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Events
{
    [HorseRaceEventGroup(GroupName = "Phigors单方联动集")]
    public class PhigrosEvents
    {
        [HorseRaceEvent(EventName = "dB doll")]
        public static void Phigros_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(EssBuffs.Dizz, 2);
            msgBuilder.AppendLine($"{from.GetDisplay()} 尝试了IN的dB doll，在最后出现了一个蓝!!心脏骤停!!!");
        }
        [HorseRaceEvent(EventName = "云女孩")]
        public static void Phigros_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Step -= 17;
            msgBuilder.AppendLine($"[云女孩]{from.GetDisplay()} 激 情 慢 速 死 亡 停 顿 A T 1 7 (后退17格)");
        }
        [HorseRaceEvent(EventName = "云女孩2")]
        public static void Phigros_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Step -= 1;
            msgBuilder.AppendLine($"[云女孩]音符停顿的那一刻{from.GetDisplay()}的心脏也停了(后退1格)");
        }
        [HorseRaceEvent(EventName = "云女孩3")]
        public static void Phigros_14(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.AddBuff(new PhiBuffs.CloudGirl(),3);
            msgBuilder.AppendLine($"[云女孩]{from.GetDisplay()}三回合的~云女孩~效果");
            msgBuilder.AppendLine($"[云女孩]~云女孩~:每回合有可能突然停顿无法移动。");
        }
        [HorseRaceEvent(EventName = "垃圾分类")]
        public static void Phigros_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.GetDisplay()}:你是什么垃圾");
        }
        [HorseRaceEvent(EventName = "萤火虫の怨")]
        public static void Phigros_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Step += randomStep + 2;
            msgBuilder.AppendLine($"{from.GetDisplay()}觉得萤火虫已经是怨火虫了(步数额外+2)");
        }
        [HorseRaceEvent(EventName = "萤火虫の怨2")]
        public static void Phigros_6(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Step += randomStep + 3;
            msgBuilder.AppendLine($"[萤火虫の怨]{from.GetDisplay()}十 字 交 叉 满 屏 黄 线(步数额外+3)");
        }
        
        [HorseRaceEvent(EventName = "LeaF迷宫")]
        public static void Phigros_7(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Step += randomStep + 1;
            msgBuilder.AppendLine($"{from.GetDisplay()}Lyrith-迷宫 和我LeaF有什么关系(步数额外+1)");
        }
        
        [HorseRaceEvent(EventName = "樱树街道")]
        public static void Phigros_8(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Step += randomStep + 1;
            msgBuilder.AppendLine($"[樱树街道]{from.GetDisplay()}觉得中间飘得几篇雪花是mope花(步数额外+1)");
        }
        [HorseRaceEvent(EventName = "尊师1")]
        public static void Phigros_9(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Step += randomStep + 1;
            msgBuilder.AppendLine($"[尊师The Guru]{from.GetDisplay()}打了一15次尊师，终于到了90w(步数+1)");
        }
        [HorseRaceEvent(EventName = "尊师2")]
        public static void Phigros_10(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Step += randomStep -2;
            msgBuilder.AppendLine($"[尊师The Guru]{from.GetDisplay()}:读谱不能(后退2格)");
        }
        [HorseRaceEvent(EventName = "尊师3")]
        public static void Phigros_11(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Step += randomStep + 15;
            msgBuilder.AppendLine($"[尊师The Guru]{from.GetDisplay()}:15之耻(前进15格)");
        }
        [HorseRaceEvent(EventName = "尊师4")]
        public static void Phigros_12(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStatus(HorseStatus.Died);
            msgBuilder.AppendLine($"[尊师The Guru]{from.GetDisplay()}觉得赫尔才91w，太菜了。于是赫尔就把Ta鲨了。");
        }
        [HorseRaceEvent(EventName = "尊师5")]
        public static void Phigros_13(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.SetStatus(HorseStatus.Died);
            msgBuilder.AppendLine($"[尊师The Guru]{from.GetDisplay()}让大家一起打尊师。所有人获得<尊师>Buff");
            msgBuilder.AppendLine($"[尊师The Guru]<尊师>:每个回合随机获得-1~1的步数加成");
        }
    }

    public class PhiBuffs
    {
        public class Guru : BuffBase
        {
            public override string Prefix()
                => "<尊师>";

            public override void OnRoundEnded(Horse horse, RaceGround ground)
            {
                horse.Step += RandomUtil.Number(-1, 1);
            }
        }
        public class CloudGirl : BuffBase
        {
            public override string Prefix()
                => "~云女孩~";

            public override bool CanMove()
            {
                if (RandomUtil.Number(1, 2) == 1)
                    return true;
                else
                    return false;
            }
        }
    }
}
