using System.Collections.Generic;
using System.Linq;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.SDK.Model
{
    public class Horse : BuffableObject
    {
        public List<IHorseItem> Items { get; set; } = new();

        /// <summary>
        /// 谁的马儿
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public string OwnerQQ { get; set; }

        /// <summary>
        /// 马尔的展示名
        /// </summary>
        public string Display { get; set; }

        /// <summary>
        /// 在事件执行前就会计算随机的步数并存放于此。
        /// 未执行事件的实体会根据事件执行后的这个值前进。
        /// </summary>
        public int TempStep;

        private int _step = 0;

        public int Step
        {
            get => _step;
            set => SetStep(value);
        }

        public int GetStep()
        {
            return _step;
        }

        public bool SetStep(int value, bool force = false)
        {
            if (force)
            {
                _step = value;
                return true;
            }
            else
            {
                var ori = _step;

                _step = CanMove() ? (value < 0) ? (CanBeInfluence() ? value : _step) : value : _step;

                return value == _step || _step != ori;
            }
        }

        /// <summary>
        /// 为选手添加一个Buff
        /// </summary>
        /// <param name="buff"></param>
        /// <param name="time">持续回合(不包括当前回合)</param>
        /// <param name="force"></param>
        /// <returns>是否添加成功</returns>
        public bool AddBuff(BuffBase buff, int time = 1, bool force = false)
        {
            if (force)
            {
                Buffs.Add((buff, time));
                return true;
            }

            if (!CanBeInfluence())
                return false;

            if (!OnBuffAdd(buff, time, this))
                return false;

            if (Buffs.Exists(x => x.Buff == buff))
            {
                var find = Buffs.Find(x => x.Buff == buff);
                find.LeftTime += time;
            }
            else
            {
                Buffs.Add((buff, time));
            }

            return true;
        }


        /// <summary>
        /// 获取选手的状态
        /// </summary>
        public HorseStatus Status { get; set; } = HorseStatus.None;

        public bool IsWaiting = false;

        /// <summary>
        /// 自己无论睡眠状态都可以影响自己，若显然自己也无法影响自己，请设置from=null
        /// </summary>
        /// <returns></returns>
        public bool SetStatus(HorseStatus status, bool force = false)
        {
            if (force)
            {
                Status = status;
                return true;
            }

            if (!CanBeInfluence())
                return false;
            if (!OnStatusSet(status, this))
                return false;
            Status = status;
            return true;
        }

        /// <summary>
        /// 是否能够被事件影响
        /// </summary>
        /// <returns></returns>
        public bool CanBeInfluence()
        {
            if (Status is HorseStatus.Died or HorseStatus.Left) //死亡或者离场，无敌无法被影响
                return false;
            if (!Buffs.TrueForAll(x => x.Buff.CanBeInfluence()))
                return false;
            return true;
        }


        /// <summary>
        /// 是否可以正常移动(无法被事件影响)
        /// </summary>
        /// <returns></returns>
        public bool CanMove()
        {
            if (Status is HorseStatus.Died or HorseStatus.Left) //死亡,离场,眩晕
                return false;

            if (!Buffs.TrueForAll(x => x.Buff.CanMove()))
                return false;

            return true;
        }

        /// <summary>
        /// 结合着临时的前缀获取显示用名称 
        /// </summary>
        /// <param name="nochange">不接受Buff的重写change</param>
        /// <returns></returns>
        public string GetDisplay(bool nochange = true)
        {
            //获取数据与buff的前缀
            var temps = GetPrefixes();


            //获取状态的前缀
            var status = TextUtil.GetEnumDesc(Status);

            var display = $"{(status != "" ? status : "")}" + string.Join("", temps) + Display;

            if (!nochange)
            {
                Buffs.ForEach(x => display = x.Buff.ChangeDisplay(display));
            }

            return display;
        }

        /// <summary>
        /// 获取数据中的临时前缀+Buffs中的前缀
        /// </summary>
        /// <returns></returns>
        public string[] GetPrefixes()
        {
            var list = new List<string>();
            list.AddRange(GetPrefixesInDatas());
            list.AddRange(GetPrefixesInBuffs());
            return list.ToArray();
        }

        /// <summary>
        /// 获取所有Prefix_前缀数据
        /// </summary>
        /// <returns></returns>
        private string[] GetPrefixesInDatas()
        {
            return Datas.Where(x => x.Key.StartsWith("Prefix"))
                .ToList()
                .Select(x => x.Value.ToString()).ToArray();
        }

        private string[] GetPrefixesInBuffs()
        {
            return Buffs
                .Select(x => x.Buff.Prefix()).ToArray();
        }

        /// <summary>
        /// 增加一个前缀(-1 time为永久)
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="time"></param>
        public void AddPrefix(string prefix)
        {
            SetData($"Prefix_{prefix}", prefix);
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        public Horse Clone()
        {
            return MemberwiseClone() as Horse;
        }
    }
}