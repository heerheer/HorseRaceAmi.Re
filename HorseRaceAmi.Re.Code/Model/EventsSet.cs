namespace HorseRaceAmi.Re.Code.Model
{
    public class EventsSet
    {
        /// <summary>
        /// 事件组
        /// </summary>
        public IEnumerable<string> EventGroups { get; set; } = new string[0];
        /// <summary>
        /// 事件
        /// </summary>
        public IEnumerable<string> Events { get; set; } = new string[0];
    }
}