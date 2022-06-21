using System.Reflection;
using AmiableNext.SDK;
using HorseRaceAmi.Re.Code.Module;
using HorseRaceAmi.Re.Code.Util;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Model;

namespace HorseRaceAmi.Re.Code
{
    public class HorseRace
    {
        /// <summary>
        /// 是否允许创建,加入，开始赛马
        /// </summary>
        public static bool Enable { get; set; } = true;

        public static int MaxStep = 3;
        public static int MinStep = 1;

        //public static char StepDisplay = '=';

        public static double RandomEvent = 1;

        public static RaceMode RaceMode = RaceMode.OneWin;


        /// <summary>
        /// Group-RaceGround
        /// </summary>
        public static Dictionary<string, RaceGround> Races { get; set; } = new();

        public static Dictionary<string, DateTime> RaceTimeHistory = new();


        public static bool CanCreateRace(string group, out TimeSpan left)
        {
            left = TimeSpan.Zero;
            if (!RaceTimeHistory.ContainsKey(group))
            {
                return true;
            }


            left = TimeSpan.FromMinutes(ConfigUtil.GetConfig().DelayTime) -
                   (DateTime.Now - RaceTimeHistory[group]); //已经过去的时间
            if (left >= TimeSpan.Zero)
            {
                return false;
            }

            RaceTimeHistory.Remove(group);
            return true;
        }

        public static void RemoveRace(string group)
        {
            if (Races.ContainsKey(group))
            {
                Races.Remove(group);
            }
        }

        public static void Init()
        {
            if (HrAmiModuleLoader.IsInitialized is false)
            {
                HrAmiModuleLoader.LoadModules();
            }
        }

        public static List<Type> GetPluginEvents()
        {
            return Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.Namespace != null && x.Namespace.Contains("HorseRaceAmi.Re.Code.BotEvents"))
                .Where(x => typeof(IBotEvent).IsAssignableFrom(x)).ToList();
        }
    }
}