using System.ComponentModel;

namespace HorseRaceAmi.SDK.Enum
{
    public enum HorseStatus
    {
        [Description("")]
        None,
        [Description("<死亡>")]
        Died,
        [Description("<离开>")]
        Left,
        [Description("<律者化>")]
        Herrscher,

    }
}