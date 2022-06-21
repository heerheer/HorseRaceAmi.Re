using System.Collections.Generic;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Interface;

namespace HorseRaceAmi.SDK.Model
{
    public class BuffableObject:DatableObject
    {
        public List<(BuffBase Buff, int LeftTime)> Buffs = new();
        
        public void OnBeforeRoundBegin(RaceGround ground,Horse horse)
        {
            Buffs.ForEach(x => x.Buff.OnBeforeRoundBegin(horse, ground));
        }

        public void OnRoundEnded(RaceGround ground,Horse horse)
        {
            Buffs.ForEach(x => x.Buff.OnRoundEnded(horse, ground));
        }

        public void OnAfterRoundEnded(RaceGround ground,Horse horse)
        {
            Buffs.ForEach(x => x.Buff.OnAfterRoundEnded(horse, ground));
        }
        
        /// <summary>
        /// 只有选手可以被触发
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool OnBuffAdd(BuffBase buff,int time,Horse horse)
        {
            return Buffs.TrueForAll(x => x.Buff.OnBuffAdd(horse, buff,time));
        }

        /// <summary>
        /// 只有选手可以被触发
        /// </summary>
        /// <param name="status"></param>
        /// <param name="horse"></param>
        /// <returns></returns>
        public bool OnStatusSet(HorseStatus status,Horse horse)
        {
            return Buffs.TrueForAll(x => x.Buff.OnStatusSet(horse, status));
        }
        
        public void RefreshBuffs()
        {
            //refresh Buffs
            Buffs.ForEach(x => x.LeftTime -= 1);
            for (int i = 0; i < Buffs.Count; i++)
            {
                Buffs[i] = new(Buffs[i].Buff, Buffs[i].LeftTime - 1);
            }

            Buffs.RemoveAll(x => x.LeftTime < 0);
        }
    }
    
    
}