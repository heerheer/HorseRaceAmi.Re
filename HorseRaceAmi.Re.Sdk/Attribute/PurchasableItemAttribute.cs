using System;

namespace HorseRaceAmi.SDK.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PurchasableItemAttribute:System.Attribute
    {
        public string ItemName { get; set; }
        public string ShopName { get; set; }
        public int ItemCost { get; set; }
    }
}