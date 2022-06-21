using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.SDK.Buffs
{
    public class InvincibleBuff:BuffBase
    {
        public override bool CanBeInfluence() => false;

        public override bool CanMove() => true;

        public override string Prefix() => "<无敌>";
    }
}