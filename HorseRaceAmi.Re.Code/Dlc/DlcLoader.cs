using System.Diagnostics;
using System.Reflection;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Dlc
{
    public static class DlcLoader
    {
        public static List<DlcInfo> DlcInfos = new();

        public static void LoadDlcs()
        {
            DlcInfos.Clear();

            var dir = PathUtil.DlcDirectory;

            var files = Directory.GetFiles(dir, "*.dll");

            foreach (var file in files)
            {
                try
                {
                    DlcInfos.Add(Load(file));
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }

        /// <summary>
        /// 识别DLC，并不是尝试加载内部实现
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DlcInfo Load(string path)
        {
            var fileInfo = new FileInfo(path);

            var fvi = FileVersionInfo.GetVersionInfo(path);
            var version = fvi.FileVersion;
            var apiVersion = fvi.FileVersion.Split('.')[1];
            var dlcName = fvi.FileDescription;

            return new DlcInfo
            {
                Version = version,
                ApiVersion = int.Parse(apiVersion),
                DlcName = dlcName,
                FileInfo = fileInfo
            };
        }

        /// <summary>
        /// 将DLC尝试加载并返回模块声明
        /// </summary>
        public static List<HrAmiModule> LoadModules()
        {
            LoadDlcs();

            List<HrAmiModule> modules = new();

            foreach (var dlcInfo in DlcInfos)
            {
                try
                {
                    var ass = Assembly.LoadFrom(dlcInfo.FileInfo.FullName);

                    var attrType = typeof(HrAmiModuleAttribute);
                    foreach (var type in ass.GetTypes().Where(x =>
                        Attribute.IsDefined(x, attrType) && typeof(HrAmiModule).IsAssignableFrom(x)))
                    {
                        var methodInfo = type.GetMethod("Main");
                        List<HrAmiModule> returnModules = methodInfo.Invoke(null, null) as List<HrAmiModule>;
                        if (returnModules != null) modules.AddRange(returnModules);
                    }
                }
                catch (ReflectionTypeLoadException e)
                {
                    foreach (var eLoaderException in e.LoaderExceptions)
                    {
                        //AppService.Instance.Log($"[加载异常]{eLoaderException}");
                    }
                }
                catch (Exception e)
                {
                    //AppService.Instance.Log($"[加载DLC程序集异常]{e}");
                }
            }

            return modules;
        }
    }
}