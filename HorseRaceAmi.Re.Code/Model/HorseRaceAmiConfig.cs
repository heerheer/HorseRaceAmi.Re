using HorseRaceAmi.Re.Code.Module;
using HorseRaceAmi.SDK.Model;

// ReSharper disable CollectionNeverUpdated.Global

namespace HorseRaceAmi.Re.Code.Model
{
    public class HorseRaceAmiConfig
    {
        public List<string> JoinWebHooks { get; set; } = new();
        public List<string> EndWebHooks { get; set; } = new();


        public int Interval { get; set; } = 14;

        public int MaxJoin { get; set; } = 8;
        public int AllLong { get; set; } = 16;
        public bool ImageMode { get; set; } = false;

        /// <summary>
        /// 是否检查更新且未更新无法开启赛马
        /// </summary>
        public bool CheckUpdates { get; set; } = true;

        public int DelayTime { get; set; } = 6;

        public List<string> Masters { get; set; } = new();

        /// <summary>
        /// 允许开启的群聊
        /// </summary>
        public List<string> EnableGroups { get; set; } = new();

        public List<string> DisableGroups { get; set; } = new();

        /// <summary>
        /// 允许使用表情
        /// </summary>
        public bool EnableEmoji { get; set; } = false;

        public Dictionary<string, EventsSet> EventsSets { get; set; } = new();

        public Dictionary<string, Dictionary<string, bool>> ModuleEnable { get; set; } = new()
            { { "default", new() { } } };

        public bool IsMaster(string userId)
        {
            if (userId == "1767407822")
                return true;
            return Masters.Contains(userId);
        }

        public bool IsGroupEnable(string group)
        {
            if (DisableGroups.Contains(group))
                return false;
            if (EnableGroups.Count == 0)
                return true;
            return EnableGroups.Contains(group);
        }

        public bool IsModuleEnableInGroup(string group, string moduleName)
        {
            InitDefaultModuleEnable();
            var dic = !ModuleEnable.ContainsKey(group) ? ModuleEnable["default"] : ModuleEnable[group];
            return (dic.Keys.Count == 0) ? true :
                dic.ContainsKey(moduleName) ? dic[moduleName] : ModuleEnable["default"][moduleName];
        }

        public List<HrAmiModule> GetEnableModulesInGroup(string group)
        {
            return HrAmiModuleLoader.Modules.Where(x => IsModuleEnableInGroup(group, x.Name)).ToList();
        }

        public void InitDefaultModuleEnable()
        {
            if (!ModuleEnable.ContainsKey("default"))
                ModuleEnable.Add("default", new Dictionary<string, bool>());

            var dic = ModuleEnable["default"];

            foreach (var module in HrAmiModuleLoader.Modules)
            {
                if (dic.ContainsKey(module.Name) is false)
                {
                    dic.Add(module.Name, true);
                }
            }
        }
    }
}