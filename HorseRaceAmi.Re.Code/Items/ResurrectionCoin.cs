using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.Re.Code.Items
{
    [PurchasableItem(ShopName = "道具商店", ItemName = "复活币", ItemCost = 500)]
    public class ResurrectionCoin : IHorseItem
    {
        public string ItemName => "复活币";

        public RoundTime UseTime
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public bool CanUseInRace() => true;

        public bool Use(RaceGround ground, Horse horse, StringBuilder sb)
        {
            if (horse.Status == HorseStatus.Died)
            {
                horse.Status = HorseStatus.None;
                sb.AppendLine($"{horse.Display}使用了一个复活币!复活了!");
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Use(string @group, string userId, StringBuilder sb)
        {
            sb.AppendLine("赫尔暂时无法影响表征世界...");
            return false;
        }
    }
}