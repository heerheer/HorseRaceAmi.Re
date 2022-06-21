namespace HorseRaceAmi.SDK.Enum
{
    public enum RoundTime
    {
        /// <summary>
        /// 比赛开始前
        /// </summary>
        BeforeRaceStart,
        /// <summary>
        /// 回合开始前
        /// </summary>
        BeforeRoundStart,
        /// <summary>
        /// 回合开始后
        /// </summary>
        RoundStarted,
        /// <summary>
        /// 统计步数前
        /// </summary>
        BeforeRoundCount,
        /// <summary>
        /// 统计步数数
        /// </summary>
        AfterRoundCount,
        /// <summary>
        /// 回合结束
        /// </summary>
        RoundFinished,
        /// <summary>
        /// 比赛结束
        /// </summary>
        RaceFinished,
        
        /// <summary>
        /// 被改变状态
        /// </summary>
        HorseStatus,
        
        /// <summary>
        /// 被赋予Buff
        /// </summary>
        HorseBuff
    }
}