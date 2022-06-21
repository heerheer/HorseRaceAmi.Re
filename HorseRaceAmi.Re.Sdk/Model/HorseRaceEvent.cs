using System;
using System.Reflection;
using System.Text;

namespace HorseRaceAmi.SDK.Model
{
    public class HorseRaceEvent
    {
        public string EventName { get; set; }
        public string Tag { get; set; }
        public string Group { get; set; }

        public MethodInfo method;
        public Action<RaceGround,Horse,StringBuilder,int> EventAction;
        public Type type;

        /// <summary>
        /// 返回是否直接跳过后续
        /// </summary>
        /// <param name="ground"></param>
        /// <param name="from"></param>
        /// <param name="msgBuilder"></param>
        /// <param name="randomStep"></param>
        /// <returns></returns>
        public bool Invoke(RaceGround ground, Horse from, StringBuilder msgBuilder, int randomStep)
        {
            if (method == null)
            {
                EventAction.Invoke(ground,from,msgBuilder,randomStep);
                return true; 
            }
            if (method.ReturnType == typeof(bool))
            {
                return (bool)method.Invoke(null, new object[]
                {
                    ground, from, msgBuilder, randomStep
                });
            }
            else
            {
                method.Invoke(null, new object[]
                {
                    ground, from, msgBuilder, randomStep
                });
                return false;
            }
        }
    }
}