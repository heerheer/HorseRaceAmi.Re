using System.Text;
using AmiableNext.SDK;
using HorseRaceAmi.Re.Code.Module;
using HorseRaceAmi.Re.Code.Util;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.BotEvents
{
    public class UseItemCommand : IBotEvent
    {
        private AmiableEventContext _eventArgs;

        public void Process(AmiableEventContext ctx)
        {
            _eventArgs = ctx;
            if (!ConfigUtil.GetConfig().IsGroupEnable(ctx.GroupId))
                return;
            if (ctx.Content.StartsWith("使用小马物资"))
            {
                var sb = new StringBuilder();

                var item = ctx.Content.Substring(6).Trim();
                if (DataUtil.GetItemCount(ctx.AuthorId, item) >= 1)
                {
                    var items = HrAmiModuleLoader.GetAllItems(ctx.GroupId);
                    if (items.Exists(x => x.ItemName == item))
                    {
                        var find = items.Find(x => x.ItemName == item);

                        if (find.Use(ctx.GroupId, ctx.AuthorId, sb))
                        {
                            //使用成功

                            sb.AppendLine("使用成功~");
                            DataUtil.AddItem(ctx.AuthorId, item, -1);
                        }
                        else
                        {
                            sb.AppendLine("使用失败了!!!");
                        }
                    }
                    else
                    {
                        //sb.AppendLine("您似乎得到了平行世界的东西~");
                        sb.AppendLine("前面的区域，以后再来探索吧");
                    }
                }
                else
                {
                    sb.AppendLine("物资数量不足!");
                }

                ctx.GroupReply(sb.ToString());
            }
        }

        public CommonEventType EventType { get; set; } = CommonEventType.MessageGroup;
    }
}