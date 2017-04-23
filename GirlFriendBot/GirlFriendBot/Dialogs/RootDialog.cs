using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace GirlFriendBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            // calculate something for us to return
            int length = (activity.Text ?? string.Empty).Length;

            // return our reply to the user
            // await context.PostAsync($"You sent {activity.Text} which was {length} characters");

            if (activity.Text.Contains("名字") || activity.Text.Contains("name"))
            {
                await context.PostAsync($"我叫高圆圆~ 我是你的女朋友，请多多关照！");
                context.Wait(MessageReceivedAsync);
            }
            else if (activity.Text == "a") 
            {
                PromptDialog.Confirm(context, AfterConfirm, "我是不是世界上最美的人？");
            }  
            else if (activity.Text.Contains("饿"))
            {
                List<string> food = new List<string>
                {
                    "鸡蛋灌饼",
                    "糯米包油条",
                    "双喜铁板烧"
                };
                PromptOptions<string> options = new PromptOptions<string>("今天想吃什么","","",food);
                PromptDialog.Choice<string>(context, Eat, options);
            }
            else if (activity.Text.Contains("表情"))
            {
                var reply = activity.CreateReply();
                reply.Attachments = new List<Attachment>();

                reply.Attachments.Add(new Attachment()
                {
                    ContentUrl = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1492924950681&di=a9acc842e9db15e66a60d346d2f9e5e5&imgtype=0&src=http%3A%2F%2Fimg.tcgame.com.cn%2Fsapk%2F78913.jpg",
                    ContentType = "image/jpg",
                    Name = "bq.jpg"
                });
                await context.PostAsync(reply);
                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task Eat(IDialogContext context, IAwaitable<string> result)
        {
            string food = await result;
            switch (food)
            {
                case "鸡蛋灌饼": await context.PostAsync($"好吃，我们走吧"); break;
                case "糯米包油条": await context.PostAsync($"工菜里我最爱吃这个"); break;
                case "双喜铁板烧": await context.PostAsync($"太远了，别去了吧"); break;
                default:
                    break;
            }
        }

        public async Task AfterConfirm(IDialogContext context, IAwaitable<bool> result)
        {
            var confirm = await result;
            if (confirm)
            {
                await context.PostAsync("你最帅~");
            }
            else
            {
                await context.PostAsync("你是不是欠抽");
            }
        }
    }
}