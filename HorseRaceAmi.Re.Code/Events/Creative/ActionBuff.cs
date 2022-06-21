using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.Re.Code.Events.Creative
{
    public class ActionBuff : BuffBase
    {
        private Action<Horse, RaceGround> _onRoundEndedAction;
        private string _buff;

        public ActionBuff(Action<Horse, RaceGround> action,string buff)
        {
            _onRoundEndedAction = action;
            _buff = buff;
        }

        public override string Prefix()
        {
            return _buff;
        }

        public override void OnRoundEnded(Horse horse, RaceGround ground)
        {
            _onRoundEndedAction(horse, ground);
        }
    }
}