using System.Text;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.SDK.Interface
{
    /// <summary>
    /// 比赛时候的道具
    /// </summary>
    public interface IHorseItem
    {
        string ItemName { get; }
        
        /// <summary>
        /// 定义何时使用
        /// </summary>
        RoundTime UseTime { get; set; }
        
        bool CanUseInRace();

        /// <summary>
        /// 若使用了返回true
        /// </summary>
        /// <param name="ground"></param>
        /// <param name="horse"></param>
        /// <returns></returns>
        bool Use(RaceGround ground,Horse horse, StringBuilder sb);
        bool Use(string group,string userId,StringBuilder sb);
    }
}