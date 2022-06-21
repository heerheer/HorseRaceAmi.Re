using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.SDK.Interface
{
    public abstract class BuffBase
    {
        public virtual bool CanBeInfluence()
        {
            return true;
        } //确定是否能够被影响}

        
        public virtual bool CanMove() => true; //确定是否能够移动

        public abstract string Prefix();

        public virtual string ChangeDisplay(string display)
        {
            return display;
        }

        /// <summary>
        /// 在这场比赛开始前
        /// </summary>
        public virtual void OnBeforeRoundBegin(Horse horse, RaceGround ground)
        {
        }

        /// <summary>
        /// 在这场回合结束时
        /// </summary>
        public virtual void OnRoundEnded(Horse horse, RaceGround ground)
        {
        }

        /// <summary>
        /// 在这场回合结束时，且步数统计已完成但还未生效
        /// </summary>
        public virtual void OnAfterRoundEnded(Horse horse, RaceGround ground)
        {
        }

        /// <summary>
        /// 当选手受到状态设置(CanBeInfluence为真的情况下)
        /// </summary>
        /// <param name="horse"></param>
        /// <param name="ground"></param>
        /// <param name="status">被设置的钻台</param>
        /// <returns>返回否会取消设置</returns>
        public virtual bool OnStatusSet(Horse horse,  HorseStatus status)
        {
            return true;
        }

        /// <summary>
        /// 当选手受到Buff设置(CanBeInfluence为真的情况下)
        /// </summary>
        /// <param name="horse"></param>
        /// <param name="buff"></param>
        /// <param name="time"></param>
        /// <returns>返回否会取消Add</returns>
        public virtual bool OnBuffAdd(Horse horse,  BuffBase buff,int time)
        {
            return true;
        }
        
    }
}