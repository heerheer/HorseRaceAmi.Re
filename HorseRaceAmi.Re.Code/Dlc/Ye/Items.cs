using System.Text;
using HorseRaceAmi.Re.Code.dayz;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Dlc.Ye
{
    [PurchasableItem(ShopName = "雷普小店", ItemName = "魅魔叶子的赠礼", ItemCost = 99881)]
    public class Items
    {
    }

    public class YeLeiPu : IHorseItem
    {
        public string ItemName => "雷雷普";

        public RoundTime UseTime
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public bool CanUseInRace()
        {
            return false;
        }

        public bool Use(RaceGround ground, Horse horse, StringBuilder sb)
        {
            return false;
        }

        public bool Use(string @group, string userId, StringBuilder sb)
        {
            var util = new UserUtil();
            if (DataUtil.GetItemCount(userId, "赫赫尔") >= 1 && DataUtil.GetItemCount(userId, "叶叶子") >= 1)
            {
                DataUtil.AddItem(userId, "赫赫尔", -1);

                DataUtil.AddItem(userId, "叶叶子", -1);

                sb.AppendLine("消耗「赫赫尔 x1」和「叶叶子 x1」😜");

                var r = RandomUtil.Number(0, 100);
                if (r is 1 or 0)
                {
                    sb.AppendLine("叶子强势扭转！开始雷普赫尔！");
                    if (RandomUtil.Number(1, 5) is 2)
                    {
                        sb.AppendLine("因为赫尔忘记穿白袜了，导致赫尔被撅晕了！(超级奖励！欧皇时刻！获得1919810小马积分！)");
                        DataUtil.AddCoin(userId, 1919810);
                    }
                }

                if (r is > 1 and <= 80)
                {
                    var list = new[]
                    {
                        "赫尔疯狂雷普叶子。但叶子起码还没有被撅晕。",
                        "赫尔疯狂雷普叶子。叶子被雷普到肚子饿了。",
                        "赫尔疯狂雷普叶子。叶子大无语！",
                        "赫尔疯狂雷普叶子。只听到传来「救命，别雷普我了！」",
                        "赫尔疯狂雷普叶子。叶子想把自己的里人格召唤出来",
                        "赫尔疯狂雷普叶子。不许涩涩。",
                        "赫尔疯狂雷普叶子。赫尔都要雷普累了",
                    };
                    sb.AppendLine(list[RandomUtil.Number(0, list.Length - 1)]);
                }

                if (r is > 80 and <= 99)
                {
                    sb.AppendLine("赫尔没想到眼前的叶子是里人格「魅魔叶子」。");
                    sb.AppendLine("赫尔生命❤-1");
                    sb.AppendLine("获得了「叶叶子 x1」");
                }

                if (r is 100)
                {
                    sb.AppendLine("赫尔疯狂雷普叶子。然后叶子被撅晕了！");
                    sb.AppendLine("田所浩二时刻！获得114514小马积分！");
                    DataUtil.AddCoin(userId, 114514);
                }
            }

            else
            {
                sb.AppendLine("你的「赫赫尔」和「叶叶子」似乎不足");
                return false;
            }

            return true;
        }
    }

    public class MeiMoYeZi : IHorseItem
    {
        public string ItemName => "魅魔叶子的赠礼";

        public RoundTime UseTime
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public bool CanUseInRace()
        {
            return false;
        }

        public bool Use(RaceGround ground, Horse horse, StringBuilder sb)
        {
            return false;
        }

        public bool Use(string @group, string userId, StringBuilder sb)
        {
            sb.AppendLine("魅魔叶子:已经撅晕了...好累...");

            return true;
        }
    }
}