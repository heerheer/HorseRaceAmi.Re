using System.Text;
using AmiableNext.SDK;
using HorseRaceAmi.Re.Code.Util;

namespace HorseRaceAmi.Re.Code.BotEvents
{
    public class HelpCommand : IBotEvent
    {
        private AmiableEventContext _ctx;

        public void Process(AmiableEventContext ctx)
        {
            if (!ConfigUtil.GetConfig().IsGroupEnable(ctx.GroupId))
                return;


            if (ctx.Content == "#赫尔是谁")
            {
                var sb = new StringBuilder();
                sb.AppendLine($"> 你是在问赫尔是谁吗？");
                sb.AppendLine($"> 在这里你就可以找到赫尔啦:1767407822");
                sb.AppendLine($"> 你也可以加入赫尔的赛马用户群哦~230657692");
                ctx.GroupReply(sb.ToString());
                return;
            }


            if (ctx.Content.StartsWith("小马帮助") || ctx.Content.TrimStart('/', '.').StartsWith("help"))
            {
                var sb = new StringBuilder();
                sb.AppendLine($"> 赫尔正在奋笔疾书...");
                sb.AppendLine($"> .hrami 查看核心/Master相关命令");
                sb.AppendLine($"> 加入赛马 | 开始赛马");
                sb.AppendLine($"> 创建赛马 | 小马统计");
                sb.AppendLine($"> 小马商店 | 购买小马物资");
                sb.AppendLine($"> 小马背包 | 小马积分");
                ctx.GroupReply(sb.ToString());
            }
        }

        public CommonEventType EventType { get; set; } = CommonEventType.MessageGroup;
    }
}