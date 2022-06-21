using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Dlc.BugHeer
{
    public class HeerBugBuff : BuffBase
    {
        public override string Prefix()
            => "<赫尔的Bug>";

        public override void OnRoundEnded(Horse horse, RaceGround ground)
        {
            base.OnRoundEnded(horse, ground);
            Util.AddBug(ground, RandomUtil.Number(5, 10));
        }
    }

    public class JustHeer : BuffBase
    {
        public override string Prefix()
            => "<只是赫尔>";

        public override bool CanMove()
        {
            if (RandomUtil.Number(10, 20) >= 11)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class NotJustHeer : BuffBase
    {
        public override string Prefix()
            => "<不只是赫尔>";

        public override void OnRoundEnded(Horse horse, RaceGround ground)
        {
            horse.TempStep = 2;
        }
    }
}