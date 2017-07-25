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
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Okay, here is our menu. Let me know when you ready.");

            Activity replyMenu = (Activity)context.MakeMessage();
            replyMenu.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            replyMenu.Attachments = GetCards.GetCardsAttachments();

            await context.PostAsync(replyMenu);

            context.Wait(this.MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            //todo: after user press button, get the order count of each item
            int cnt = -1;
            var message = await result;
            cnt = (int)message.Value;
            await context.PostAsync("ok {cnt}");
            //var activity = await result as Activity;
            //var reply = context.MakeMessage();
            //reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            //reply.Attachments = GetCards.GetCardsAttachments();

            //await context.PostAsync(reply);
        }
    }
}
