using System;
using System.ComponentModel;

namespace HorseRaceAmi.SDK.Util
{
    public class TextUtil
    {
        public static string GetEnumDesc(System.Enum @enum)
            => ((DescriptionAttribute)(System.Attribute.GetCustomAttribute(@enum.GetType().GetField(System.Enum.GetName(@enum.GetType(), @enum)), typeof(DescriptionAttribute)))).Description;


    }
}