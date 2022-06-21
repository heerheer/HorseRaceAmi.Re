using System.Text;
using HorseRaceAmi.Re.Code.Util;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.BotEvents.Stats
{
    public class QueryStats : IPluginEvent
    {
        public AmiableEventType EventType => AmiableEventType.Group;

        // ReSharper disable once InconsistentNaming
        public void Process(AmiableEventArgs _e)
        {
            AmiableMessageEventArgs e = (AmiableMessageEventArgs)_e;

            if (!ConfigUtil.GetConfig().IsGroupEnable(e.GroupId))
                return;
            if (!e.RawMessage.StartsWith("小马统计"))
                return;


            var sb = new StringBuilder();
            sb.AppendLine("> 赫尔正在为你统计数据...");
            e.SendMessage(sb.ToString());
            sb.Clear();

            Task.Factory.StartNew(() =>
            {
                var list = SqlUtil.GetRecord(e.UserId.ToString());
                sb.AppendLine($"> 您一共参加了{list.Count}次比赛");
                sb.AppendLine($"> 成功抵达终点{list.Count(x => x.IsFinished)}次");
                sb.AppendLine($"> 在场上损失了马儿{list.Count(x => x.FinishedStatus == (int)HorseStatus.Died)}只");
                sb.AppendLine($"> 有{list.Count(x => x.FinishedStatus == (int)HorseStatus.Left)}马儿离开了赛场");
                sb.AppendLine($"> 总计有{list.Count(x => x.Ranking == 1)}次获得了第一名的佳绩");
                sb.AppendLine($"> 累计有{list.GroupBy(x => x.Time.Day).Count()}天参与比赛");
                sb.AppendLine($"> 累计失去了{SqlUtil.GetCustomRecord(e.UserId.ToString(),"LostMoney")}积分");
                sb.AppendLine($"> 累计偷取/捡到了{SqlUtil.GetCustomRecord(e.UserId.ToString(),"EarnMoney")}积分");
                sb.AppendLine($"> 更多数据请期待...");
                e.SendMessage(sb.ToString());
            });
        }
    }
}