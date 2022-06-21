using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Dlc.GoldRiver
{
    [HorseRaceEventGroup(GroupName = "黄金之河事件集v2")]
    public class GoldRiver2
    {
        [HorseRaceEvent(EventName = "黄金之河2_最后的黄金")]
        public static void GoldRiver2_1(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            var number = RandomUtil.Number(5, 20);
            Util.AddGold(from, number);
            msgBuilder.AppendLine($"{from.GetDisplay()}发现了深藏于星河中的{number}颗黄金");
        }
        
        
        [HorseRaceEvent(EventName = "黄金之河2_最后的黄金4")]
        public static void GoldRiver2_4(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.GetDisplay()}来到了黄金之河，但是发现河流快枯竭了");
        }
        [HorseRaceEvent(EventName = "黄金之河2_最后的黄金5")]
        public static void GoldRiver2_5(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.GetDisplay()}来到了黄金之河，但是似乎无法再次探寻黄金了");
        }
        [HorseRaceEvent(EventName = "黄金之河2_最后的黄金6")]
        public static void GoldRiver2_6(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            msgBuilder.AppendLine($"{from.GetDisplay()}为黄金之河投入了一些黄金与祝福");
        }
    }
}