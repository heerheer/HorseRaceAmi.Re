using System.IO;
using System.Reflection;

namespace HorseRaceAmi.SDK.Util
{
    public class PathUtil
    {
        public static string AppDirectory =>
            InitDir(Path.Combine(Directory.GetCurrentDirectory(), "config", "赛马Ami"));


        public static string DbPath =>
            Path.Combine(AppDirectory, "data.db");

        public static string CustomEventPath =>
            Path.Combine(AppDirectory, "自定义事件.json");

        public static string DlcDirectory =>
            InitDir(Path.Combine(AppDirectory, "Dlc"));

        public static string SelfPath =>
            Path.Combine(Assembly.GetExecutingAssembly().Location);

        public static string BeSdkPath =>
            Path.Combine(Directory.GetCurrentDirectory(), "HorseRaceAmi.dll");

        public static string ConfigPath =>
            InitFile(Path.Combine(AppDirectory, "config.json"));

        public static string UserDataPath(string userid) =>
            InitFile(Path.Combine(AppDirectory, "datas", $"{userid}.ini"));

        public static string InitDir(string dir)
        {
            if (Directory.Exists(dir) is false)
            {
                Directory.CreateDirectory(dir);
            }

            return dir;
        }

        public static string InitFile(string path)
        {
            InitDir(Path.GetDirectoryName(path));

            if (File.Exists(path) is false)
            {
                File.Create(path).Close();
            }

            return path;
        }

        public static T GetJsonObject<T>(string path) where T : new()
        {
            InitFile(path);
            if (File.ReadAllText(path) == "")
            {
                return new T();
            }
            else
            {
                try
                {
                    return System.Text.Json.JsonSerializer.Deserialize<T>(File.ReadAllText(path));
                }
                catch
                {
                    return new T();
                }
            }
        }
    }
}