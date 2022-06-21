using HorseRaceAmi.SDK.Interface;

namespace HorseRaceAmi.Re.Code.Dlc.BugHeer
{
    public static class Util
    {
        public static void AddBug(DatableObject from, int gold)
        {
            from.SetData("bug", (int)(from.GetData("bug") ?? 0) + gold);
        }

        public static int GetBug(DatableObject from)
        {
            return (int)(from.GetData("bug")?? 0);
        }
    }
}