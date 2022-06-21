using System.Text;
using HorseRaceAmi.Re.Code.BotEvents;
using HorseRaceAmi.Re.Code.Dlc;
using HorseRaceAmi.Re.Code.Events;
using HorseRaceAmi.Re.Code.Items;
using HorseRaceAmi.Re.Code.Model;
using HorseRaceAmi.Re.Code.Util;
using HorseRaceAmi.SDK.Attribute;
using HorseRaceAmi.SDK.Interface;
using HorseRaceAmi.SDK.Model;
using HorseRaceAmi.SDK.Util;

namespace HorseRaceAmi.Re.Code.Module
{
    public class HrAmiModuleLoader
    {
        public static bool IsInitialized = false;
        public static List<HrAmiModule> Modules = new();


        /// <summary>
        /// 读取HrAmi模块
        /// </summary>
        public static void LoadModules()
        {
            if (IsInitialized)
                return;
            IsInitialized = true;
            Modules.Add(new HrAmiModule()
            {
                Name = "内置模块",
                Types = new()
                {
                    typeof(WuHuEvents),
                    typeof(HonkiEvents),
                    typeof(OldEvents),
                    typeof(Me114514),
                    typeof(PhigrosEvents),
                    typeof(DeveloperHeer),
                    typeof(AnotherFuture),
                    typeof(FilthyMoney),
                    typeof(ResurrectionCoin),
                    typeof(RaceTale),
                    typeof(NoneEffectItems),
                }
            }); //添加内部模块
            Modules.Add(new HrAmiModule()
            {
                Name = "创意之箱",
                Types = new()
                {
                    typeof(Events.Creative.CreativeThis),
                    typeof(Events.Creative.Creative2),
                    typeof(Events.Creative.WalkIn),
                    typeof(Events.Creative.Baby),
                }
            }); //添加内部模块
            Modules.Add(InnerDlcs.BugHeer);
            Modules.Add(InnerDlcs.DrawCard);
            Modules.Add(InnerDlcs.GoldRiver);
            Modules.Add(InnerDlcs.Dice);
            Modules.Add(InnerDlcs.Ye);

            //加载DLC中声明的模块
            Modules.AddRange(DlcLoader.LoadModules());


            Modules.ForEach(x =>
            {
                try
                {
                    LoadModule(x);
                }
                catch (Exception e)
                {
                    //AppService.Instance.Log($"[Module加载异常]{e}");
                }
            });
        }

        public static void LoadModule(HrAmiModule module)
        {
            Console.WriteLine($"加载模块{module.Name}");
            foreach (var type in module.Types)
            {
                if (Attribute.IsDefined(type, typeof(HorseRaceEventGroupAttribute)))
                {
                    //AppService.Instance.Log("Event Group Added ", type.Name);

                    var methods = type.GetMethods();

                    var groupName =
                        ((HorseRaceEventGroupAttribute)Attribute.GetCustomAttribute(type,
                            typeof(HorseRaceEventGroupAttribute))).GroupName;
                    foreach (var method in methods)
                    {
                        if (Attribute.IsDefined(method, typeof(HorseRaceEventAttribute)))
                        {
                            //载入方法
                            var attr = (HorseRaceEventAttribute)Attribute.GetCustomAttribute(method,
                                typeof(HorseRaceEventAttribute));

                            module.Events.Add(new HorseRaceEvent
                            {
                                EventName = attr.EventName,
                                Tag = attr.Tag,
                                method = method,
                                type = type,
                                Group = groupName
                            });
                            //AppService.Instance.Log($"[ModuleLoader]事件{attr.EventName}已加载");
                        }
                    }
                }

                //将可购买特征的类的所有特征加入商店系统
                if (Attribute.IsDefined(type, typeof(PurchasableItemAttribute)))
                {
                    var attrs = Attribute.GetCustomAttributes(type, typeof(PurchasableItemAttribute));
                    foreach (var item in attrs)
                    {
                        var attr = (PurchasableItemAttribute)item;
                        if (!ShopCommand.Shops.ContainsKey(attr.ShopName))
                        {
                            Console.WriteLine($"初始化商店:{attr.ShopName}");
                            ShopCommand.Shops.Add(attr.ShopName, new Dictionary<string, int>());
                        }

                        ShopCommand.Shops[attr.ShopName].Add(attr.ItemName, attr.ItemCost);
                    }
                }

                if (typeof(IHorseItem).IsAssignableFrom(type))
                {
                    module.Items.Add((IHorseItem)Activator.CreateInstance(type));
                }
            }
        }

        public static List<HorseRaceEvent> GetAllEvents(string group)
        {
            List<HorseRaceEvent> events = new();
            ConfigUtil.GetConfig().GetEnableModulesInGroup(group).ForEach(x => { events.AddRange(x.Events); });
            foreach (var ce in PathUtil.GetJsonObject<List<CustomEvent>>(PathUtil.CustomEventPath))
            {
                events.Add(new HorseRaceEvent()
                {
                    EventName = ce.Name,
                    EventAction = (ground, horse, msg, rs) =>
                    {
                        horse.Step += ce.Step;
                        msg.AppendLine(horse.Display + ":" + ce.Message);
                    }
                });
            }

            return events;
        }

        public static List<IHorseItem> GetAllItems(string group)
        {
            List<IHorseItem> items = new();
            ConfigUtil.GetConfig().GetEnableModulesInGroup(group).ForEach(x => { items.AddRange(x.Items); });
            return items;
        }

        public static List<Action<RaceGround, StringBuilder>> GetAllBeforeActions(string group)
        {
            List<Action<RaceGround, StringBuilder>> actions = new();
            ConfigUtil.GetConfig().GetEnableModulesInGroup(group)
                .ForEach(x => { actions.AddRange(x.BeforeRaceStartActions); });
            return actions;
        }

        public static List<Action<RaceGround, StringBuilder>> GetAllEndActions(string group)
        {
            List<Action<RaceGround, StringBuilder>> actions = new();
            ConfigUtil.GetConfig().GetEnableModulesInGroup(group).ForEach(x => { actions.AddRange(x.RaceEndActions); });
            return actions;
        }
    }
}