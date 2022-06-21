using System.Text;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Dlc.DrawCard
{
    [PurchasableItem(ShopName = "赫尔小铺", ItemName = "钻石卡包", ItemCost = 5000)]
    public class ACardPack : IHorseItem
    {
        public string ItemName => "钻石卡包";

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

        public bool Use(string @group, string userId, StringBuilder sb)
        {
            List<string> items = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                items.Add("白袜赫尔");
                items.Add("黑丝赫尔");
                items.Add("夹心面包赫尔");
                items.Add("不吐葡萄皮赫尔");
                items.Add("不吃赫尔的赫尔");
                items.Add("车车赫尔");
                items.Add("不写代码的赫尔");
            }

            items.Add("六色条纹白袜络腮胡肌肉眼镜寸头赫尔");

            var item = items[RandomUtil.Number(0, items.Count - 1)];

            sb.AppendLine($"你拆开卡包，抽到了一张:{item}");
            sb.AppendLine($"不过似乎因为赫尔的咕咕咕，背包系统并没有很完善XD，所以卡片转瞬即逝了");
            return true;
        }
    }

    [PurchasableItem(ShopName = "赫尔小铺", ItemName = "普通卡包", ItemCost = 100)]
    public class NormalCardPack : IHorseItem
    {
        public string ItemName => "普通卡包";

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

        public bool Use(string @group, string userId, StringBuilder sb)
        {
            List<string> items = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                items.Add("复活币");
                items.Add("不是复活币");
                items.Add("保证不吭你");
                items.Add("买不了上当");
                items.Add("买不了吃亏");
                items.Add("一个大恩的签名海报·赫尔仿制版");
                items.Add("赫尔·你知道吗");
                items.Add("赫尔·蓝色条纹の白袜");
                items.Add("哎呀，你的大奖被老鼠偷走了");
                items.Add("赫尔·冰箱的主人");
                items.Add("赫尔·吃吃吃·但是吃不胖");
            }

            items.Add("五颜六色花言巧语骗你钱财顺便图你美色的赫尔");

            var item = items[RandomUtil.Number(0, items.Count - 1)];

            sb.AppendLine($"你拆开卡包，抽到了一张:{item}");
            if (item == "复活币")
            {
                DataUtil.AddItem(userId,"复活币",1);
                sb.AppendLine($"恭喜获得一枚复活币");

            }
            else
            {
                sb.AppendLine($"不过似乎因为赫尔的咕咕咕，背包系统并没有很完善XD，所以卡片转瞬即逝了");
            }

            return true;
        }
    }
}