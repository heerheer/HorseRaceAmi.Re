using System.Collections.Generic;
using System.Linq;
using HorseRaceAmi.Re.Sdk.Tool.HIni;

namespace HorseRaceAmi.SDK.Util
{
    public class DataUtil
    {
        public static void AddCoin(string userid, int coin)
        {
            AddItem(userid, "coin", coin);
        }

        public static int GetCoin(string userid)
        {
            return GetItemCount(userid, "coin");
        }

        public static void AddItem(string userid, string item, int count = 1)
        {
            AddCustomItem(userid, "背包", item, count);
        }

        public static int GetItemCount(string userid, string item)
        {
            return GetCustomItemCount(userid, "背包", item);
        }

        public static List<KeyValuePair<string, string>> GetItems(string userid)
        {
            var ini = new IniObject(PathUtil.UserDataPath(userid));
            ini.Load();
            return ini["背包"].Values.Where(x => x.Key != "coin").ToList();
        }

        public static void AddCustomItem(string userid, string bag, string item, int count)
        {
            var ini = new IniObject(PathUtil.UserDataPath(userid));
            ini.Load();
            var str = ini[bag][item];
            ini[bag][item] = ((int)decimal.Parse(str == "" ? "0" : str) + count).ToString();
            ini.Save();
        }

        public static void SetCustomItem(string userid, string bag, string item, int value)
        {
            var ini = new IniObject(PathUtil.UserDataPath(userid));
            ini.Load();
            var str = ini[bag][item];
            ini[bag][item] = (value).ToString();
            ini.Save();
        }

        public static int GetCustomItemCount(string userid, string bag, string item)
        {
            var ini = new IniObject(PathUtil.UserDataPath(userid));
            ini.Load();
            var str = ini[bag][item];
            return (int)decimal.Parse(str == "" ? "0" : str);
        }
    }
}