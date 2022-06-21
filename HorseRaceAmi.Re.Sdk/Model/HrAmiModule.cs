using System;
using System.Collections.Generic;
using System.Text;
using HorseRaceAmi.SDK.Interface;

namespace HorseRaceAmi.SDK.Model
{
    public class HrAmiModule
    {
        public bool IsEnable { get; set; } = true;
        public string Name { get; set; }

        public List<Type> Types { get; set; } 

        /// <summary>
        /// 由Loader加载
        /// </summary>
        public List<HorseRaceEvent> Events = new();

        /// <summary>
        /// 由Loader加载
        /// </summary>
        public List<IHorseItem> Items = new();

        /// <summary>
        /// 定义比赛开始前的委托
        /// </summary>
        public List<Action<RaceGround, StringBuilder>> BeforeRaceStartActions { get; set; } = new();

        /// <summary>
        /// 结束比赛时的动作
        /// </summary>
        public List<Action<RaceGround, StringBuilder>> RaceEndActions { get; set; } = new();
        

        public Dictionary<string, object> ModuleConfig;
    }
}