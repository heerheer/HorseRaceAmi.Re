using HorseRaceAmi.Re.Sdk.Tool.HIni;

namespace HorseRaceAmi.Re.Code.dayz
{
    public static class ConfigUtil
    {
        public static string GetJsonDir()
        {
            var ini = new IniObject(Path.Combine(AppContext.BaseDirectory, "heer", "dayz_server", "config.ini"));
            ini.Load();
            if (ini["config"]["json_dir"] == "")
            {
                ini["config"]["json_dir"] = Path.Combine(AppContext.BaseDirectory, "heer", "dayz_server", "datas");
                ini.Save();
            }

            return ini["config"]["json_dir"];
        }

        public static (int min, int max) GetCheckinConfig()
        {
            var ini = new IniObject(Path.Combine(AppContext.BaseDirectory, "heer", "dayz_server", "config.ini"));
            ini.Load();
            var min = ini["checkin"]["min"];
            var max = ini["checkin"]["max"];
            if (min == "" || max == "")
            {
                if (min == "") ini["checkin"]["min"] = "-50";
                if (max == "") ini["checkin"]["max"] = "900";
                ini.Save();
            }

            return (ini["checkin"]["min"].ToInt32(), ini["checkin"]["max"].ToInt32());
        }


        public static (int JoinCoin, int WinPer, int WinExReward, bool Enable) GetHorseRaceInfo()
        {
            var ini = new IniObject(Path.Combine(AppContext.BaseDirectory, "heer", "dayz_server", "config.ini"));
            ini.Load();
            var enable = ini["horse-race"]["enable"] == "true";

            var joinCoin = ini["horse-race"]["join"].ToInt32();
            var winPer = ini["horse-race"]["win-percentage"].ToInt32();
            var winExReward = ini["horse-race"]["win-extra-reward"].ToInt32();

            return (joinCoin, winPer, winExReward, enable);
        }
    }
}
/*
 * [horse-race]
 * enable=true
 * join=500
 * win-percentage=0.8
 * win-extra-reward=400
 */