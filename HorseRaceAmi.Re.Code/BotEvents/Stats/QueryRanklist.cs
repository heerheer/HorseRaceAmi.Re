using System.Text;
using AmiableNext.SDK;
using HorseRaceAmi.Re.Code.Util;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.BotEvents.Stats
{
    public class QueryRankList : IBotEvent
    {
        private CodeProvider _codeProvider;

        public void Process(AmiableEventArgs _e)
        {
            _codeProvider = AppService.Instance.DefaultCodeProvider;
            AmiableMessageEventArgs e = (AmiableMessageEventArgs)_e;

            if (!ConfigUtil.GetConfig().IsGroupEnable(e.GroupId))
                return;

            if (e.RawMessage != "小马排行榜")
                return;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"> 参赛次数最多的是...{_codeProvider.At(SqlUtil.GetMostPlay().ToString())}");
            sb.AppendLine($"> 被骗积分最多次数的是...{_codeProvider.At(SqlUtil.GetMost("LostMoney")?.QQ ?? "没有人!")}");
            sb.AppendLine($"> 捡到/赛场内获得积分最多的是...{_codeProvider.At(SqlUtil.GetMost("EarnMoney")?.QQ ?? "没有人!")}");
            sb.AppendLine($"> 前面的区域...请以后再来..探索!!");

            e.SendMessage(sb.ToString());
        }

        public CommonEventType EventType => CommonEventType.MessageGroup;
    }
}