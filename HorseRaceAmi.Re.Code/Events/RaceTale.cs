using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.Re.Code.Events
{
    [HorseRaceEventGroup(GroupName = "赛马场怪谈")]
    public class RaceTale
    {
        [HorseRaceEvent(EventName = "切勿携带食物")]
        public static void RaceTale_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"[赛马场怪谈]{from.Display}看到了赛马场的手册：2.请不要携带食物，尤其是人类的食物投喂休息区中的马儿。");
        }

        [HorseRaceEvent(EventName = "切勿对视")]
        public static void RaceTale_2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"[赛马场怪谈]{from.Display}看到了赛马场的手册：3.如果你看到马儿和你对视，请立刻转移视线");
        }

        [HorseRaceEvent(EventName = "小马积分是什么")]
        public static void RaceTale_3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"[赛马场怪谈]{from.Display}看到了赛马场的手册：");
            msgBuilder.AppendLine($"7.在赛马周边小铺中，我们不会提供且使用名为小马积分的小硬币，如果发现工作人员询问你是否要使用小马积分兑换物品，请立刻离开周边小铺，并寻找附近的工作人员说明情况。");
        }

        [HorseRaceEvent(EventName = "其他区域？")]
        public static void RaceTale_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"[赛马场怪谈]{from.Display}看到了赛马场的手册：");
            msgBuilder.AppendLine($"8.赛马场任何一个比赛区域只会在9:00-11:00和15:00-17:00开放，如果其他时间工作人员推荐你前往观看比赛，请不要犹豫直接拒绝并远离此工作人员。");
        }

        [HorseRaceEvent(EventName = "触发彩蛋!")]
        public static void RaceTale_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"[赛马场怪谈]{from.Display}捡到了一个赛马场手册书。");
            msgBuilder.AppendLine($"[@{from.OwnerQQ}]可以凭借这条消息找 赫尔 领取 >赛马场怪谈规则文案<");
        }

        [HorseRaceEvent(EventName = "雾霾!")]
        public static void RaceTale_6(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"[赛马场怪谈]{from.Display}正在仔细阅读赛马场手册");
            msgBuilder.AppendLine($"10.如果发现周边有雾霾，请按照地图指示的路径离开赛马场。");
        }

        [HorseRaceEvent(EventName = "动物园？")]
        public static void RaceTale_ex1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Display += "?";
            msgBuilder.AppendLine($"[动物园规则怪谈]{from.Display}在赛马场看到了白色的大象在游泳。开始怀疑自己是谁！");
        }
        [HorseRaceEvent(EventName = "动物园？2")]
        public static void RaceTale_ex2(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Display += "!!?!!";
            from.Step = 0;
            msgBuilder.AppendLine($"[动物园规则怪谈]{from.Display}在赛马场内发现了水族馆！San值疯狂下降！直接退回起点！");
        }
        [HorseRaceEvent(EventName = "动物园？3")]
        public static void RaceTale_ex3(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            from.Display += "<##？@@@？>";
            from.Step += 5;
            msgBuilder.AppendLine($"[动物园规则怪谈]{from.Display}在赛马场内喝了兔子血，吃了山羊肉。觉得自己突然充满了力量！步数+5");
        }
    }
}