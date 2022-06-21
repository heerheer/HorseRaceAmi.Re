using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using HorseRaceAmi.SDK.Enum;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.SDK.Model
{
    public class RaceGround : BuffableObject
    {
        public int AllLong;
        public bool IsOn { get; set; }

        public List<Horse> Horses { get; set; } = new();


        /// <summary>
        /// 赛马场中的事件列表
        /// </summary>
        public List<HorseRaceEvent> Events;


        public List<string> EventHistory = new();


        public delegate void RaceHandler(StringBuilder msgBuilder);


        public RaceGround(int allLong, List<HorseRaceEvent> events)
        {
            AllLong = allLong;
            Events = events;
        }

        public bool ShouldContinue()
        {
            if (Horses == null)
            {
                return false;
            }

            if (Horses is { Count: 0 })
            {
                return false;
            }

            //判断场地是否还要继续
            //1.看看是不是都死了或者都跑了
            if (Horses.All(x => x.Status is HorseStatus.Died or HorseStatus.Left))
            {
                return false;
            }

            //1.看看是不是有的到终点了
            if (Horses.Exists(x => x.GetStep() >= AllLong))
            {
                return false;
            }


            return true;
        }


        public void Next(StringBuilder sb)
        {
            List<int> oldSteps = new List<int>();

            foreach (var horse in Horses.ToArray())
            {
                oldSteps.Add(horse.Step);
            }

            var longstr = new string('.', AllLong);

            OnBeforeRoundBegin(this, null);
            foreach (var horse in Horses)
            {
                horse.OnBeforeRoundBegin(this, horse);
            }

            //处理每只马
            foreach (var horse in Horses.Where(x => x.CanMove()).ToArray())
            {
                var jump = false; //跳过

                var randomStep = RandomUtil.Number(1, 3);

                horse.TempStep = randomStep;

                var maxNum = 3;
                if (Events == null)
                    maxNum = 2;
                if (Events.Count == 0)
                    maxNum = 2;

                if (RandomUtil.Number(1, maxNum) == 3)
                {
                    //事件中的马尔会被事件完全接管
                    var randomEvent =
                        Events[RandomUtil.Number(0, Events.Count - 1)];
                    try
                    {
                        jump = randomEvent.Invoke(this, horse, sb, randomStep);
                        EventHistory.Add(randomEvent.EventName);
                    }
                    catch (TargetInvocationException ex)
                    {
                        if (ex.InnerException != null)
                            sb.AppendLine($"[{randomEvent.Group}->{randomEvent.EventName}]{ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        sb.AppendLine($"[{randomEvent.Group}->{randomEvent.EventName}]{ex.Message}");
                    }
                }
                else
                {
                    //不使用事件的马儿
                    horse.SetStep(horse.GetStep() + horse.TempStep);
                }
            }


            //处理每一个选手的回合结束事件
            OnRoundEnded(this, null);
            Horses.ForEach(x => x.OnRoundEnded(this, x));

            //计算相对于上一次的移动步数
            Horses.ForEach(s =>
            {
                try
                {
                    var oldStep = oldSteps.ElementAtOrDefault(0);

                    var moved = s.Step - oldStep;

                    s.TempStep = moved;

                    oldSteps.RemoveAt(0);

                    s.GetDataValue<List<int>>("StepHistory").Add(s.TempStep); //存入历史记录
                }
                catch (Exception)
                {
                }
            });


            //处理每一个选手的步数统计已完成事件
            OnAfterRoundEnded(this, null);
            Horses.ForEach(x => x.OnAfterRoundEnded(this, x));


            //处理步数异常
            foreach (var item in Horses)
            {
                if (item.GetStep() > AllLong)
                {
                    item.SetStep(AllLong, true);
                }

                if (item.GetStep() < 0)
                {
                    item.SetStep(0, true);
                }
            }

            //生成显示参赛选手步数的语句

            Horses.ForEach(s =>
            {
                var _mestr = longstr;

                var moved = s.TempStep;

                sb.AppendLine($"[{(moved >= 0 ? "+" : "-")}{Math.Abs(moved)}]" +
                              _mestr.Insert(AllLong - s.GetStep(), s.GetDisplay(false)));
            });

            //回合结束使用道具
            Horses.ForEach(x => x.Items.ToArray().ToList().ForEach(item =>
            {
                //sb.AppendLine($"{x.Display}尝试使用道具<{item.ItemName}>");
                if (item.Use(this, x, sb))
                {
                    DataUtil.AddItem(x.OwnerQQ, item.ItemName, -1);
                    x.Items.Remove(item);
                    //sb.AppendLine($"{x.GetDisplay()}使用了一个<{item.ItemName}>");
                }
            }));

            //刷新Buffs
            RefreshBuffs();
            Horses.ForEach(x => { x.RefreshBuffs(); });
        }
    }
}