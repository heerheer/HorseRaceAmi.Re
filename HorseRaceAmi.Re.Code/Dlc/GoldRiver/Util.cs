using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.Re.Code.Dlc.GoldRiver
{
    public static class Util
    {
        public static void AddGold(Horse from, int gold)
        {
            from.SetData("gold", (int)(from.GetData("gold") ?? 0) + gold);
        }

        public static int GetGold(Horse from)
        {
            return (int)(from.GetData("gold")?? 0);
        }
    }
}