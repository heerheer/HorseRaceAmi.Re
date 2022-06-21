using System.Text.Encodings.Web;
using System.Text.Json;
using HorseRaceAmi.Re.Code.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Util
{
    public class ConfigUtil
    {
        public static HorseRaceAmiConfig GetConfig()
        {
            try
            {
                var path = PathUtil.ConfigPath;
                var text = File.ReadAllText(path);
                if (text == string.Empty)
                {
                    File.WriteAllText(path, JsonSerializer.Serialize(new HorseRaceAmiConfig()));
                    return new();
                }

                return JsonSerializer.Deserialize<HorseRaceAmiConfig>(text);
            }
            catch (Exception)
            {
                return new();
            }
        }

        public static void SetConfig(HorseRaceAmiConfig cfg)
        {
            var path = PathUtil.ConfigPath;
            File.WriteAllText(path,
                JsonSerializer.Serialize(cfg,
                    new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }));
        }
    }
}