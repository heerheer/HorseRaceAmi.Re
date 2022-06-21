using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.SDK.Buffs
{
    public class HideSelf : BuffBase
    {
        public override bool CanBeInfluence() => true;

        public override bool CanMove() => true;

        public override string Prefix() => "隐身";

        public override string ChangeDisplay(string display)
        {
            return "";
        }
    }
}