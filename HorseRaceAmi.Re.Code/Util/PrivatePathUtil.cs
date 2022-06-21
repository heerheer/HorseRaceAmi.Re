using System.Reflection;

namespace HorseRaceAmi.Re.Code.Util
{
    public class PrivatePathUtil
    {
        public static void Set()
        {
            AppDomain.CurrentDomain.SetData("PRIVATE_BINPATH", "plugin;bin;");
            AppDomain.CurrentDomain.SetData("BINPATH_PROBE_ONLY", "plugin;bin;");
            var funsion = typeof(AppDomain).GetMethod("GetFusionContext",
                BindingFlags.NonPublic | BindingFlags.Instance);

            var m = typeof(AppDomainSetup).GetMethod("UpdateContextProperty",
                BindingFlags.NonPublic | BindingFlags.Static);
            m.Invoke(null,
                new object[]
                    { funsion.Invoke(AppDomain.CurrentDomain, null), "PRIVATE_BINPATH", "plugin;bin;" });

        }
    }
}