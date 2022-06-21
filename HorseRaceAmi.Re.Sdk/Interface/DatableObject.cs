using System.Collections.Generic;

namespace HorseRaceAmi.SDK.Interface
{
    public class DatableObject
    {
        public Dictionary<string, object> Datas = new(); //存放数据

        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetData(string key, object value)
        {
            if (!Datas.ContainsKey(key))
            {
                Datas.Add(key, value);
            }
            else
            {
                Datas[key] = value;
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetData(string key)
            => !Datas.ContainsKey(key) ? null : Datas[key];

        /// <summary>
        /// 如果没有就会自动初始化一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetDataValue<T>(string key) where T : new()
            => (T)GetData(key) ?? new T();

        
    }
}