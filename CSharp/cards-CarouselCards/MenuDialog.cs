namespace CarouselCardsBot
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class MenuDialog : IDialog<object>
    {
        public List<string> order;
        public async Task StartAsync(IDialogContext context)
        {
            order = new List<string>();

            await context.PostAsync("Okay, here is our menu. Let me know when you ready.");

            Activity replyMenu = (Activity)context.MakeMessage();
            replyMenu.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            replyMenu.Attachments = GetCards.GetCardsAttachments();

            await context.PostAsync(replyMenu);

            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var newOrder = await result;
            order.Add(newOrder.Text);
            await context.PostAsync($"Ordering {newOrder.Text} each! Do you want anything else?");

            context.Wait(this.TestFunction);
        }

        public async Task TestFunction(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var response = await result;
            if(response.Text.ToLower().Contains("$"))
            {
                var newOrder = await result;
                order.Add(newOrder.Text);
                await context.PostAsync($"Ordering {newOrder.Text} each! Do you want anything else?");

                context.Wait(this.TestFunction);
            }
            else if(response.Text.ToLower().Contains("y"))
            {
                await context.PostAsync("Here is our menu again. Let me know what you want.");

                Activity replyMenu = (Activity)context.MakeMessage();
                replyMenu.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                replyMenu.Attachments = GetCards.GetCardsAttachments();
                await context.PostAsync(replyMenu);

                context.Wait(this.MessageReceivedAsync);
            }
            else
            {
                await context.PostAsync("Thanks for ordering! Here's your order summary. See you next time!");

                int total = 0;
                foreach(var item in order)
                {
                    int indexDollarSign = item.IndexOf("$");
                    int indexFirstSpace = item.IndexOf(" ");
                    int quantity = Convert.ToInt32(item.Substring(0, indexFirstSpace));
                    total += quantity * Convert.ToInt32(item.Substring(indexDollarSign + 1));
                    await context.PostAsync($"{item}");
                }
                await context.PostAsync($"Total: ${total}");

                // This will return us to RootDialog
                context.Done(true);
            }
        }
    }
}
