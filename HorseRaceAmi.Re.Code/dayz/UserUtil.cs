using System.Text.Json;
using System.Text.Json.Nodes;
using HorseRaceAmi.Re.Sdk.Tool.HIni;

namespace HorseRaceAmi.Re.Code.dayz
{
    public class UserUtil
    {
        public static string JsonDir;

        static UserUtil()
        {
            JsonDir = ConfigUtil.GetJsonDir();


            if (!Directory.Exists(JsonDir))
            {
                Directory.CreateDirectory(JsonDir);
            }
        }
        
        
        public int GetHorseRaceInfo()
        {
            var ini = new IniObject(Path.Combine(AppContext.BaseDirectory, "heer", "dayz_server", "binds.ini"));

            ini.Load();

            return ini["horse-race"]["win-award"].ToInt32();
        }

        public string GetId(string qq)
        {
            var ini = new IniObject(Path.Combine(AppContext.BaseDirectory, "heer", "dayz_server", "binds.ini"));

            ini.Load();

            return ini[qq]["steam_id"];
        }


        /// <summary>
        /// 获取游戏信息,会主动检测是否绑定后返回
        /// </summary>
        /// <param name="qq"></param>
        /// <returns></returns>
        public (bool result, string reason, (string name, long currency)? data) GetInfo(string qq)
        {
            var id = GetId(qq);
            if (string.IsNullOrEmpty(id))
            {
                return (false, "steam_id未绑定 使用 绑定+steamid 来绑定", null);
            }
            else
            {
                var path = Path.Combine(JsonDir, id + ".json");
                if (!File.Exists(path))
                {
                    return (false, "用户文件不存在，可能还没登入过服务器!", null);
                }

                //var reader = new Utf8JsonReader(File.ReadAllBytes(path).AsSpan());

                var jsonObj = JsonSerializer.Deserialize<JsonNode>(File.ReadAllText(path));
                if (jsonObj != null)
                {
                    return (true, "OK", (jsonObj["m_Username"]?.ToString(),
                        (long)jsonObj["m_OwnedCurrency"]));
                }
                else
                {
                    return (false, "JSON解析失败!", null);
                }
            }
        }


        public void AddCoin(string qq, long count)
        {
            var id = GetId(qq);
            var path = Path.Combine(JsonDir, id + ".json");
            var jsonObj = JsonSerializer.Deserialize<JsonNode>(File.ReadAllText(path));

            var coin = (long)(jsonObj["m_OwnedCurrency"] ?? 0);

            jsonObj["m_OwnedCurrency"] = coin + count;

            File.WriteAllText(path, JsonSerializer.Serialize(jsonObj));
        }

        public IEnumerable<(string qq, string steamId)> GetAllValidUser()
        {
            var ini = new IniObject(Path.Combine(AppContext.BaseDirectory, "heer", "dayz_server", "binds.ini"));
            ini.Load();
            if (ini.Sections is not null)
            {
                foreach (var iniSection in ini.Sections)
                {
                    var qq = iniSection.SectionName;
                    var steamId = iniSection["steam_id"];
                    var path = Path.Combine(JsonDir, steamId + ".json");

                    if (!File.Exists(path))
                    {
                        continue;
                    }

                    yield return (qq, steamId);
                }
            }
        }
    }
}