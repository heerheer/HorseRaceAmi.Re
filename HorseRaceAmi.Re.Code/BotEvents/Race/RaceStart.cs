using System.Security.Cryptography;
using System.Text;
using AmiableNext.SDK;
using HorseRaceAmi.Re.Code.dayz;
using HorseRaceAmi.Re.Code.Module;
using HorseRaceAmi.SDK.Util;
using ConfigUtil = HorseRaceAmi.Re.Code.Util.ConfigUtil;

namespace HorseRaceAmi.Re.Code.BotEvents.Race
{
    public class RaceStart : IBotEvent
    {
        public CommonEventType EventType { get; set; } = CommonEventType.MessageGroup;

        public void Process(AmiableEventContext ctx)
        {
            if (!ConfigUtil.GetConfig().IsGroupEnable(ctx.GroupId))
                return;

            var message = ctx.Content;
            //加入赛马 [🐎]


            if (message == "开始赛马")
            {
                try
                {
                    StartRaceInGroup(ctx, ctx.GroupId.ToString());
                }
                catch (Exception exception)
                {
                    var sb = new StringBuilder();
                    sb.AppendLine($"> 比赛开始失败");
                    sb.AppendLine($"> 原因:{exception.Message}");
                    ctx.GroupReply(sb.ToString());
                }
            }
        }

        public static bool StartRaceInGroup(AmiableEventContext ctx, string group)
        {
            var config = ConfigUtil.GetConfig();

            if (!HorseRace.Enable)
            {
                throw new Exception("赛马场正在停业整顿中!");
            }

            if (!HorseRace.Races.ContainsKey(group))
                throw new Exception("本群还未开启比赛");
            if (HorseRace.Races[group].IsOn)
                throw new Exception("本群比赛已经开始了");
            if (HorseRace.Races[group].Horses.Count < 2)
                throw new Exception("人数小于2人");

            // ReSharper disable once AsyncVoidLambda
            Task.Factory.StartNew(async () =>
            {
                HorseRace.Races[group].IsOn = true; //开启赛马


                var ground = HorseRace.Races[group]; //引用这个群的ground

                //自动装备可用的道具
                foreach (var horse in ground.Horses)
                {
                    foreach (var item in HrAmiModuleLoader.GetAllItems(group).Where(x => x.CanUseInRace()))
                    {
                        if (DataUtil.GetItemCount(horse.OwnerQQ, item.ItemName) >= 1)
                        {
                            horse.Items.Add(item);
                        }
                    }
                }

                StringBuilder sb = new StringBuilder();

                //开始之前的委托
                HrAmiModuleLoader.GetAllBeforeActions(group).ForEach(x => x.Invoke(ground, sb));


                ground.Horses.ForEach(s =>
                {
                    var mestr = ">>>>>" + s.Display +
                                $"\n携带道具:{string.Join("|", s.Items.Select(item => item.ItemName))}";
                    sb.AppendLine(mestr);
                });


                ctx.GroupReply(sb.ToString());
                sb.Clear();


                //初始化赛马场结束


                bool needContinue = true;


                while (needContinue && HorseRace.Races[group].IsOn)
                {
                    await Task.Delay(config.Interval * 1000);

                    ground.Next(sb);

                    //发送
                    if (ConfigUtil.GetConfig().ImageMode)
                    {
                        /*ctx.GroupReply(group,
                            AppService.Instance.DefaultCodeProvider.Image(RaceDrawer.Draw(sb))
                        );*/

                        await Task.Delay(2000);
                    }
                    else
                    {
                        ctx.GroupReply(sb.ToString());
                    }

                    //清理sb
                    sb.Clear();

                    //再次判断是否需要继续。
                    needContinue = ground.ShouldContinue();
                }


                //模块内的事件
                HrAmiModuleLoader.GetAllEndActions(group).ForEach(x => x.Invoke(ground, sb));

                await Task.Delay(2000);

                sb.Clear();
                sb.AppendLine("> 比赛结束");
                sb.AppendLine("> 赫尔正在为您生成战报...");
                ctx.GroupReply(sb.ToString());


                var memberCount = ground.Horses.Count;
                await Task.Delay(2000);
                //延迟2s后发出比赛结果

                sb.Clear();

                //积分操作
                var firstCoin = RandomUtil.Number(2000, 4000);
                var otherCoin = RandomUtil.Number(100, 500);

                // ground.Horses.Sort((x, y) => -x.Step.CompareTo(y.Step));

                var maxStep = ground.Horses.Max(x => x.Step);


                var r = RandomUtil.Number(1, 5);
                switch (r)
                {
                    case 1:
                        sb.AppendLine($"> 大家都获得了「叶叶子 x1」和「赫赫尔 x0」");
                        break;
                    case 2:
                        sb.AppendLine($"> 大家都获得了「叶叶子 x0」和「赫赫尔 x2」");
                        break;
                    case 3:
                        sb.AppendLine($"> 大家都获得了「叶叶子 x1」和「赫赫尔 x2」");
                        break;
                    case 4:
                        sb.AppendLine($"> 大家都获得了「叶叶子 x2」和「赫赫尔 x1」");
                        break;
                    case 5:
                        sb.AppendLine($"> 大家都获得了关键道具「雷雷普 x1」");
                        break;
                }

                ground.Horses.ForEach(x =>
                {
                    switch (r)
                    {
                        case 1:
                            DataUtil.AddItem(x.OwnerQQ, "叶叶子", 2);

                            break;
                        case 2:
                            DataUtil.AddItem(x.OwnerQQ, "赫赫尔", 2);

                            break;
                        case 3:
                            DataUtil.AddItem(x.OwnerQQ, "叶叶子", 1);
                            DataUtil.AddItem(x.OwnerQQ, "赫赫尔", 2);

                            break;
                        case 4:
                            DataUtil.AddItem(x.OwnerQQ, "叶叶子", 2);
                            DataUtil.AddItem(x.OwnerQQ, "赫赫尔", 1);

                            break;
                        case 5:
                            DataUtil.AddItem(x.OwnerQQ, "雷雷普", 1);
                            break;
                    }
                });

                var wins = ground.Horses.Where(x => x.Step == maxStep);

                sb.AppendLine($"> 恭喜获胜的朋友们:");
                wins.ToList().ForEach(x =>
                {
                    //records.Find(f => f.QQ == x.OwnerQQ).Ranking = 1;
                    sb.AppendLine($"[{x.OwnerQQ}]");

                    DataUtil.AddCoin(x.OwnerQQ, firstCoin);


                    //dayzserver 联动
                    var info = dayz.ConfigUtil.GetHorseRaceInfo();
                    if (info.Enable)
                    {
                        new UserUtil().AddCoin(x.OwnerQQ,
                            (memberCount * info.JoinCoin) * info.WinPer + info.WinExReward);
                        Console.WriteLine($"{x.OwnerQQ}获得了奖励:" +
                                          ((memberCount * info.JoinCoin) * info.WinPer + info.WinExReward));
                    }
                    //联动结束

                    ground.Horses.Remove(x);
                });
                sb.AppendLine($"> 已为获胜者添加了{firstCoin}小马积分");


                ground.Horses.ForEach(x =>
                {
                    //records.Find(f => f.QQ == x.OwnerQQ).Ranking = ground.Horses.IndexOf(x) + 2;


                    DataUtil.AddCoin(x.OwnerQQ, otherCoin);
                });

                sb.AppendLine($"> 也感谢其他奋战的{ground.Horses.Count}位选手");
                sb.AppendLine($"> 每一位选手都获得了{otherCoin}小马积分");
                ctx.GroupReply(sb.ToString());


                HorseRace.Races.Remove(group);

                //尝试写入数据库
                try
                {
                    //records.ForEach(x => { SqlUtil.AddRecord(x); });
                }
                catch (Exception)
                {
                    //ctx.GroupReply("尝试写入数据库失败，请Master检查下是否按照赫尔的指引操作了。");
                }
            }).ContinueWith(t =>
            {
                //if (t.Exception != null) AppService.Instance.Log(t.Exception.InnerException);
                ctx.GroupReply(t.Exception.Message + "\n" + "请拿着消息前往赛马官方频道/群/联系赫尔" + "\n更多请发送:#赫尔是谁");
                HorseRace.Races.Remove(group);
            }, TaskContinuationOptions.OnlyOnFaulted);

            return true;
        }
    }
}