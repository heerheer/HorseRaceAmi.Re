using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.Re.Code.Dlc.GoldRiver
{
    [PurchasableItem(ShopName = "黄金的庭院",ItemName = "黄金的枯竭记忆",ItemCost = 30000)]
    [PurchasableItem(ShopName = "黄金的庭院",ItemName = "黄金的涌动记忆",ItemCost = 30000)]
    [PurchasableItem(ShopName = "黄金的庭院",ItemName = "最后的黄金之钥",ItemCost = 40000)]
    public class Items
    {
        
    }

    public class GoldKey : IHorseItem
    {
        public string ItemName =>"最后的黄金之钥";

        public RoundTime UseTime
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public bool CanUseInRace()
        {
            return false;
        }

        public bool Use(RaceGround ground, Horse horse,StringBuilder sb)
        {
            return false;
        }

        public bool Use(string @group, string userId,StringBuilder sb)
        {
            sb.AppendLine($"流淌的河水终将枯竭...");
            return true;
        }
    }
    
}