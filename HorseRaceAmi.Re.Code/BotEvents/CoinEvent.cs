using System.Text;
using AmiableNext.SDK;
using HorseRaceAmi.Re.Code.Util;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.BotEvents
{
    public class CoinEvent : IBotEvent
    {
        public void Process(AmiableEventContext ctx)
        {
            if (!ConfigUtil.GetConfig().IsGroupEnable(ctx.GroupId))
                return;

            if (ctx.Content != "小马积分")
                return;

            var sb = new StringBuilder();
            sb.AppendLine($"[@{ctx.AuthorId}]");
            sb.AppendLine($"> {DataUtil.GetCoin(ctx.AuthorId)}");
            sb.AppendLine($"> 以上就是你的小马积分~");
            //sb.AppendLine($"> 您可以随时在 Ami交换中心 换取其他货币~");

            ctx.GroupReply(sb.ToString());
        }

        public CommonEventType EventType { get; set; } = CommonEventType.MessageGroup;
    }
    
}