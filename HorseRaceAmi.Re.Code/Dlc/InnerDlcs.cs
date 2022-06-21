using HorseRaceAmi.Re.Code.Dlc.Dice;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.Re.Code.Dlc
{
    public class InnerDlcs
    {
        public static HrAmiModule GoldRiver = new()
        {
            Name = "黄金之河",
            Types = new()
            {
                typeof(GoldRiver.GoldRiver),
                typeof(GoldRiver.GoldRiver2),
                typeof(GoldRiver.GoldKey),
                typeof(GoldRiver.Items),
                typeof(GoldRiver.Util),
            }
        };

        public static HrAmiModule BugHeer = new()
        {
            Name = "Bug生产者赫尔",
            Types = new()
            {
                typeof(BugHeer.JustHeer),
                typeof(BugHeer.NotJustHeer),
                typeof(BugHeer.HeerBugBuff),
                typeof(BugHeer.BugHeer),
                typeof(BugHeer.Util),
            }
        };

        public static HrAmiModule DrawCard = new()
        {
            Name = "抽卡",
            Types = new()
            {
                typeof(DrawCard.ACardPack),
                typeof(DrawCard.NormalCardPack),
                typeof(DrawCard.AAAPack),
            }
        };

        public static HrAmiModule Dice = new()
        {
            Name = "骰娘事件薄",
            Types = new()
            {
                typeof(DiceLove),
            }
        };

        public static HrAmiModule Ye = new()
        {
            Name = "放课后！被赫尔雷普の叶子！",
            Types = new()
            {
                typeof(Ye.YeEvents),
                typeof(Ye.YeLeiPu),
                typeof(Ye.MeiMoYeZi),
                typeof(Ye.Items)
            }
        };
    }
}