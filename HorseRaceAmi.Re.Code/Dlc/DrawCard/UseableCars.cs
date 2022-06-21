using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Dlc.DrawCard
{
    [PurchasableItem(ShopName = "道具商店", ItemName = "抽卡器", ItemCost = 200)]
    public class AAAPack : IHorseItem
    {
        public string ItemName => "抽卡器";

        public RoundTime UseTime
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public bool CanUseInRace()
        {
            return true;
        }

        public bool Use(RaceGround ground, Horse horse, StringBuilder sb)
        {
            var c = RandomUtil.Number(1, 10);
            sb.AppendLine($"{horse.Display}使用了<抽卡器>道具");
            switch (c)
            {
                case 1:
                    horse.Step += 5;
                    sb.AppendLine($"{horse.Display}抽到了一个<超级赫尔加速器>,直接前进5格");
                    break;
                case 2:
                    DataUtil.AddItem(horse.OwnerQQ, "复活币", 1);
                    sb.AppendLine($"{horse.Display}抽到了一个<复活币>");
                    break;
                case 3:
                    horse.Step += 1;

                    sb.AppendLine($"{horse.Display}抽到了一个猪头，前进+1");
                    break;
                default:
                    sb.AppendLine($"{horse.Display}什么都没抽到");
                    break;
                    
            }

            return true;
        }

        public bool Use(string @group, string userId, StringBuilder sb)
        {
            return false;
        }
    }
}