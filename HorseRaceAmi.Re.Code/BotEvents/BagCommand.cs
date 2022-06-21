using System.Text;
using AmiableNext.SDK;
using HorseRaceAmi.Re.Code.Util;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.BotEvents
{
    public class BagCommand : IBotEvent
    {
        private AmiableEventContext _eventArgs;

        public void Process(AmiableEventContext ctx)
        {
            _eventArgs = ctx;
            if (!ConfigUtil.GetConfig().IsGroupEnable(ctx.GroupId))
                return;
            if (ctx.Content.StartsWith("小马背包"))
            {
                var sb = new StringBuilder();
                sb.AppendLine($"> 赫尔正在检索你的背包");
                DataUtil.GetItems(ctx.AuthorId).ForEach(x => sb.AppendLine($"> {x.Key} > {x.Value}"));
                ctx.GroupReply(sb.ToString());
            }
        }

        public CommonEventType EventType { get; set; } = CommonEventType.MessageGroup;
    }
}