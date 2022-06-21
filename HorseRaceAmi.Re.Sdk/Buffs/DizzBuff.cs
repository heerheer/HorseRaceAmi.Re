using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.SDK.Buffs
{
    public class DizzBuff:BuffBase
    {
        
        public override bool CanBeInfluence()
        {
            return true;
        }

        public override bool CanMove()
        {
            return false;
        }

        public override string Prefix()
        {
            return "<眩晕>";
        }
    }
}