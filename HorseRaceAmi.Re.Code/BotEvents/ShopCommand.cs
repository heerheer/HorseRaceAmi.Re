using System.Text;
using AmiableNext.SDK;
using HorseRaceAmi.Re.Code.Util;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.BotEvents
{
    public class ShopCommand : IBotEvent
    {
        public static Dictionary<string, Dictionary<string, int>> Shops = new();

        private AmiableEventContext _eventArgs;

        public void Process(AmiableEventContext ctx)
        {
            _eventArgs = ctx;

            if (!ConfigUtil.GetConfig().IsGroupEnable(ctx.GroupId))
                return;
            if (ctx.Content.StartsWith("小马商店"))
            {
                var shop = ctx.Content.Substring(4).Trim();
                Process_Shop(shop);
            }

            if (ctx.Content.StartsWith("购买小马物资"))
            {
                var item = ctx.Content.Substring(6).Trim();
                if (item.Contains("*"))
                {
                    var _ = item.Split('*');

                    var count = decimal.Parse(_[1]);
                    if (count <= 0 || count >= int.MaxValue)
                    {
                        return;
                    }

                    Process_Bug(_[0], (int)count);
                    return;
                }

                Process_Bug(item);
            }

            if (ctx.Content.StartsWith("抢劫小马商店"))
            {
                if (ctx.AuthorId == "1767407822")
                {
                    DataUtil.AddCoin(ctx.AuthorId, 2000000);
                    ctx.GroupReply("哟，这不是赫尔吗，快去复习考研。\n> 抢劫成功，2000000小马积分已添加进您的账户");
                }
            }
        }

        private void Process_Bug(string item, int count = 1)
        {
            if (count <= 0)
                return;

            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(item))
            {
                sb.AppendLine("您要买啥???");
                _eventArgs.GroupReply(sb.ToString());
                return;
            }

            var find = Shops.ToList().Find(x => x.Value.ContainsKey(item));

            if (find.Value == null)
            {
                sb.AppendLine("您要买啥???");
                _eventArgs.GroupReply(sb.ToString());
                return;
            }

            var needMoney = find.Value[item] * count;
            if (needMoney <= 0)
                return;
            if (DataUtil.GetCoin(_eventArgs.AuthorId) >= needMoney)
            {
                //满足
                DataUtil.AddCoin(_eventArgs.AuthorId, -needMoney);
                sb.AppendLine($"> 购买成功~");
                sb.AppendLine($"> 背包中已添加{count}个{item}");
                sb.AppendLine($"> 道具商品会在开始的时候自动装备!");
                sb.AppendLine($"> 非道具商品可以使用 使用小马物资xxx 来使用哦~");
                DataUtil.AddItem(_eventArgs.AuthorId, item, count);

                _eventArgs.GroupReply(sb.ToString());
            }
            else
            {
                sb.AppendLine($"> 小马商店不做亏本生意~");
                if (RandomUtil.Number(1, 10) == 10)
                {
                    sb.AppendLine($"> 彩蛋:背包中已添加1个<亏本生意>");
                    DataUtil.AddItem(_eventArgs.AuthorId, "亏本生意", 1);
                }

                _eventArgs.GroupReply(sb.ToString());
            }
        }

        public CommonEventType EventType { get; set; } = CommonEventType.MessageGroup;

        private void Process_Shop(string shop)
        {
            var sb = new StringBuilder();
            if (string.IsNullOrEmpty(shop))
            {
                sb.AppendLine("> 小马商店列表:");
                Shops.ToList().ForEach(x => sb.AppendLine($"> {x.Key}"));
                sb.AppendLine("> 输入 小马商店 <商店名> 来查看物品列表");

                _eventArgs.GroupReply(sb.ToString());
                return;
            }

            if (Shops.ContainsKey(shop) is false)
                return;

            var shopContent = Shops[shop];
            sb.AppendLine($"{shop}-商品列表");

            shopContent.ToList().ForEach(x => sb.AppendLine($"{x.Key} > {x.Value}小马积分"));
            sb.AppendLine($"> 输入 购买小马物资 xxx 即可购买物品");
            sb.AppendLine($"> 购买小马物资 物品*数量 可以购买多个");

            _eventArgs.GroupReply(sb.ToString());
        }
    }
}