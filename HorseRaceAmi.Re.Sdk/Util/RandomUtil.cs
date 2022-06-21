using System;

namespace HorseRaceAmi.SDK.Util
{
    public class RandomUtil
    {
        public static int Number(int min,int max)
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next(min, max + 1);
        }
    }
}
