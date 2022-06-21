namespace HorseRaceAmi.SDK.Model
{
    public class TempData
    {
        public TempData(string value, int lastTime = -1)
        {
            Value = value;
            LeftTime = lastTime;
        }

        public int LeftTime { get; set; } //-1永久

        public object Value { get; set; } //数据的值

        public T GetValue<T>() where T : new()
        {
            return (T)Value ?? new T();
        }

        /// <summary>
        /// 消耗一次持续时间
        /// </summary>
        /// <returns></returns>
        public void Consume()
        {
            LeftTime--;
        }

        /// <summary>
        /// 判断是否需要移除这个数据
        /// </summary>
        public bool ShouldRemove => (LeftTime < 0) ? true : false;
    }
}